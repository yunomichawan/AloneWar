using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
//using System.Threading.Tasks;
using System.Reflection;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.Common;
using AloneWar.Common.Extensions;

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
        private void DbSetUp(Action<Type, DataAccessAttribute, List<TableNameInfo>> callback)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            List<Type> typeList = types.Where(t =>
            {
                return t.GetAttribute<DataAccessAttribute>() != null;
            }).ToList();

            List<TableNameInfo> masterTableNameInfoList = SqliteHelper.Instance.GetTableNameList(typeof(MasterCodeData));
            List<TableNameInfo> tranTableNameInfoList = SqliteHelper.Instance.GetTableNameList(typeof(SaveData));

            typeList.ForEach(t =>
            {
                DataAccessAttribute dataAccess = t.GetAttribute<DataAccessAttribute>();
                switch (dataAccess.DbName)
                {
                    case AloneWarConst.SqliteDataBaseName.Master:
                        callback(t, dataAccess, masterTableNameInfoList);
                        break;
                    case AloneWarConst.SqliteDataBaseName.Transaction:
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

    }
}
