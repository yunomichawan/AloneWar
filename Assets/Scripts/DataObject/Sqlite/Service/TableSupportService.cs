using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AloneWar.DataObject.Sqlite.Service
{
    /// <summary>
    /// エディターコードの予定
    /// </summary>
    public class TableSupportService
    {

        /// <summary>
        /// 全てのテーブルを再作成
        /// ・必要性が無いような...
        /// </summary>
        public void AllRemakeDb()
        {
            // 再生成
            this.DbSetUp((t, d, list) =>
            {
                bool istable = this.CheckTable(t, d, list);
                if (istable)
                {
                    SqliteHelper.Instance.DropSqliteTable(t);
                }
                SqliteHelper.Instance.CreateSqliteTable(t);
            });
        }

        /// <summary>
        /// 存在しないテーブルのみを作成
        /// </summary>
        public void MakeNoneDb()
        {
            this.DbSetUp((t, d, list) =>
            {
                bool isTable = this.CheckTable(t, d, list);
                if (!isTable)
                {
                    SqliteHelper.Instance.CreateSqliteTable(t);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        private void DbSetUp(Action<Type, DataAccessAttribute, List<TableNameInfo>> callback)
        {
            List<Type> typeList = this.GetSqliteDataObjectTypeList("SqliteObject");

            List<TableNameInfo> masterTableNameInfoList = SqliteHelper.Instance.GetTableNameList(typeof(MasterCodeData));
            List<TableNameInfo> tranTableNameInfoList = SqliteHelper.Instance.GetTableNameList(typeof(SaveData));

            typeList.ForEach(t =>
            {
                DataAccessAttribute dataAccess = t.GetAttribute<DataAccessAttribute>();
                switch (dataAccess.DbName)
                {
                    case AloneWarConst.SqliteDataBaseName.MasterDb:
                        callback(t, dataAccess, masterTableNameInfoList);
                        break;
                    case AloneWarConst.SqliteDataBaseName.TransactionDb:
                        callback(t, dataAccess, tranTableNameInfoList);
                        break;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private bool CheckTable(Type type, DataAccessAttribute dataAccess, List<TableNameInfo> tableNameInfoList)
        {
            return tableNameInfoList.Find(t => t.TableName.Equals(dataAccess.TableName)) == null;
        }

        /// <summary>
        /// 引数からSqliteObjectのタイプリストを取得
        /// </summary>
        /// <param name="dbNamespace"></param>
        /// <returns></returns>
        public List<Type> GetSqliteDataObjectTypeList(string dbNamespace)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            return types.Where(t =>
            {
                if (string.IsNullOrEmpty(t.Namespace))
                {
                    return t.Namespace.Contains(dbNamespace);
                }

                return false;
            }).ToList();
        }
    }
}
