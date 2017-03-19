using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AloneWar.Common;
using AloneWar.Common.Bind;
using AloneWar.Common.Component;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.Service;
using AloneWar.DataObject.Sqlite.Helper;

namespace AloneWar.DataObject
{
    /// <summary>
    /// 保存データを取り扱うクラス
    /// </summary>
    public class SaveDataManager
    {
        /// <summary>
        /// 初期化(新規開始)
        /// </summary>
        public void Init()
        {

        }

        /// <summary>
        /// トランザクションデータの保存
        /// </summary>
        public void Save()
        {
            Dictionary<Type, IList> transactionData = TransactionComponent.Instance.TransactionData;
            foreach (Type type in transactionData.Keys)
            {
                IList list = transactionData[type];
                for (int i = 0; i < list.Count; i++)
                {
                    SqliteBaseData sqliteBaseData = (SqliteBaseData)list[i];
                    switch (sqliteBaseData.dbObjectStatus)
                    {
                        case DbObjectStatus.Insert:
                            SqliteHelper.Instance.InsertSingle(sqliteBaseData);
                            break;
                        case DbObjectStatus.Update:
                            SqliteHelper.Instance.UpdateSingle(sqliteBaseData);
                            break;
                        case DbObjectStatus.Delete:
                            SqliteHelper.Instance.DeleteSingle(sqliteBaseData);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 読込
        /// </summary>
        public void Load(int saveId)
        {
            // トランザクションデータを全て読込
            TableSupportService tableSupportService = new TableSupportService();
            List<Type> typeList = tableSupportService.GetSqliteDataObjectTypeList(AloneWarConst.SqliteDataBaseName.Transaction);
            typeList.ForEach(t =>
            {
                SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder(t);
                DataTable dataTable = SqliteHelper.Instance.GetSqliteObjectTable(sqliteQueryBuilder);
                IList list = DataAnonymousBinding.DataTableToObjectList(dataTable, t);
                TransactionComponent.Set(t, list);
            });
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="saveId"></param>
        public void Delete(int saveId)
        {

        }
    }
}
