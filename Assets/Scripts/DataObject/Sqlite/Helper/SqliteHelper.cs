using AloneWar.Common;
using AloneWar.Common.Bind;
using AloneWar.Common.Component;
using AloneWar.Common.Extensions;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace AloneWar.DataObject.Sqlite.Helper
{
    /// <summary>
    /// Sqlite実行クラス
    /// </summary>
    public class SqliteHelper : SingletonComponent<SqliteHelper>
    {

        #region セレクトSQL実行

        public T GetSqliteObjectFirst<T>(SqliteQueryBuilder sqliteQueryBuilder)
        {
            List<T> objectList = this.GetSqliteObjectList<T>(sqliteQueryBuilder);
            return objectList.FirstOrDefault();
        }

        public List<T> GetSqliteObjectList<T>(SqliteQueryBuilder sqliteQueryBuilder)
        {
            // sql生成
            sqliteQueryBuilder.CreateSelectSql();
            List<T> objectList = ExecuteQuery<T>(sqliteQueryBuilder);
            return objectList;
        }

        public DataTable GetSqliteObjectTable(SqliteQueryBuilder sqliteQueryBuilder)
        {
            sqliteQueryBuilder.CreateSelectSql();
            DataTable dataTable = ExecuteQuery(sqliteQueryBuilder);
            return dataTable;
        }

        #endregion

        #region データ操作SQL実行

        public void InsertSingle(SqliteBaseData component)
        {
            SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder();
            sqliteQueryBuilder.CreateInsertSql(component);
            this.ExecuteNonQuery(sqliteQueryBuilder);
        }

        public void InsertMulti<T>(List<T> componetList) where T : SqliteBaseData
        {
            componetList.ForEach(c => this.InsertSingle(c));
        }

        public void UpdateSingle(SqliteBaseData component)
        {
            SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder();
            sqliteQueryBuilder.CreateUpdateSql(component);
            this.ExecuteNonQuery(sqliteQueryBuilder);
        }

        public void UpdateMulti<T>(List<T> componetList) where T : SqliteBaseData
        {
            componetList.ForEach(c => this.UpdateSingle(c));
        }

        public void UpdateCustomDetail(SqliteQueryBuilder sqliteQueryBuilder)
        {
            this.ExecuteNonQuery(sqliteQueryBuilder);
        }

        public void DeleteSingle(object component)
        {
            SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder();
            sqliteQueryBuilder.CreateDeleteSql(component);
            this.ExecuteNonQuery(sqliteQueryBuilder);
        }

        public void DeleteMulti<T>(List<T> componetList) where T : SqliteBaseData
        {
            componetList.ForEach(c => this.DeleteSingle(c));
        }

        public void DeleteCustomDetail(SqliteQueryBuilder sqliteQueryBuilder)
        {
            this.ExecuteNonQuery(sqliteQueryBuilder);
        }

        #endregion

        #region テーブル操作SQL実行

        public bool SqliteTableChecker(Type tableType)
        {
            SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder();
            sqliteQueryBuilder.CreateTableCheckSql(tableType);
            SqliteCounter sqliteCounter = this.ExecuteQuery<SqliteCounter>(sqliteQueryBuilder).First();
            return !sqliteCounter.Count.Equals(0);
        }

        public void CreateSqliteTable(Type tableType)
        {
            SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder();
            sqliteQueryBuilder.CreateTableSql(tableType);
            this.ExecuteNonQuery(sqliteQueryBuilder);
        }

        public void DropSqliteTable(Type tableType)
        {
            SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder();
            sqliteQueryBuilder.DropTableSql(tableType);
            this.ExecuteNonQuery(sqliteQueryBuilder);
        }

        public List<TableNameInfo> GetTableNameList(Type tableType)
        {
            SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder();
            sqliteQueryBuilder.CreateTableListSql(tableType);
            return this.ExecuteQuery<TableNameInfo>(sqliteQueryBuilder);
        }

        #endregion

        #region SQL実行

        private List<T> ExecuteQuery<T>(SqliteQueryBuilder sqliteQueryBuilder)
        {
            try
            {
                SqliteDatabase sqliteDatabase = this.CreateConenctInstance(sqliteQueryBuilder);
                // sql実行
                List<T> dataList = sqliteDatabase.ExecuteQueryToObject<T>(sqliteQueryBuilder.ExecutetSql.ToString());
                Debug.Log(sqliteQueryBuilder.ExecutetSql.ToString());
                return dataList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private DataTable ExecuteQuery(SqliteQueryBuilder sqliteQueryBuilder)
        {
            try
            {
                SqliteDatabase sqliteDatabase = this.CreateConenctInstance(sqliteQueryBuilder);
                // sql実行
                DataTable dataTable = sqliteDatabase.ExecuteQueryToDataTable(sqliteQueryBuilder.ToString());
                Debug.Log(sqliteQueryBuilder.ExecutetSql.ToString());
                return dataTable;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private void ExecuteNonQuery(SqliteQueryBuilder sqliteQueryBuilder)
        {
            try
            {
                // db接続(共通化すること)
                SqliteDatabase sqliteDatabase = this.CreateConenctInstance(sqliteQueryBuilder);
                // sql実行
                sqliteDatabase.ExecuteNonQuery(sqliteQueryBuilder.ExecutetSql.ToString());
                Debug.Log(sqliteQueryBuilder.ExecutetSql.ToString());
            }
            catch (Exception e)
            {

            }
        }

        private SqliteDatabase CreateConenctInstance(SqliteQueryBuilder sqliteQueryBuilder)
        {
            return new SqliteDatabase(sqliteQueryBuilder.DbName);
        }

        #endregion
    }

    #region 特定のSQLでしか使用しないクラス

    public class SqliteCounter
    {
        [SqliteProperty]
        public int Count { get; set; }
    }

    public class TableNameInfo
    {
        [SqliteProperty]
        public string TableName { get; set; }
    }

    #endregion
}
