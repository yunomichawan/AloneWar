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
    /// SQL文自動生成
    /// </summary>
    public class SqliteQueryBuilder
    {
        private const string SELECT_SQL = "select {0} from {1}";
        private const string INSERT_SQL = "insert into {0} {1}";
        private const string UPDATE_SQL = "update {0} set";
        private const string DELETE_SQL = "delete from {0}";
        private const string CREATE_TABLE_SQL = "create table {0}({1})";
        private const string ORDER_BY_SQL = "order by {0}";

        public string DbName { get; set; }
        public StringBuilder ExecutetSql { get; set; }
        private StringBuilder WhereSql { get; set; }
        private StringBuilder JoinSql { get; set; }
        private string[] OrderByColumns { get; set; }
        private Dictionary<Type, string> SelectType { get; set; }
        private Dictionary<string, string> PrimarySql { get; set; }

        #region コンストラクタ

        /// <summary>
        /// セレクタ文実行用コンストラクタ
        /// </summary>
        /// <param name="type"></param>
        public SqliteQueryBuilder(Type type, bool isDefaultSort = false)
        {
            this.ExecutetSql = new StringBuilder();
            this.SelectType = new Dictionary<Type, string>();
            this.WhereSql = new StringBuilder();
            this.JoinSql = new StringBuilder();
            this.PrimarySql = new Dictionary<string, string>();
            this.OrderByColumns = new string[] { };
            this.AddSelectType(type);

            if (isDefaultSort)
            {
                // typeof(IDefaultSort).Name
                if (type.GetInterface("IDefaultSort") != null)
                {
                    this.AddOrderByColumns("SortNo", type, true);
                }
            }

            this.SetConnectDbName(type);
        }

        /// <summary>
        /// テーブル操作文実行用コンストラクタ
        /// </summary>
        /// <param name="type"></param>
        public SqliteQueryBuilder()
        {
            ExecutetSql = new StringBuilder();
            SelectType = new Dictionary<Type, string>();
            WhereSql = new StringBuilder();
            JoinSql = new StringBuilder();
            PrimarySql = new Dictionary<string, string>();
            OrderByColumns = new string[] { };
        }

        #endregion

        #region 条件設定 Condition setting

        /// <summary>
        /// Setting conditional statement
        /// 条件文設定
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="equal"></param>
        public void AddCondition(string column, object value, Type type, bool equal)
        {
            if (value == null) return;
            this.AddOperator(true);
            DataAccessAttribute attribute = type.GetAttribute<DataAccessAttribute>();
            AddConditionValue(column, attribute, value.ToString(), equal);
        }

        /// <summary>
        /// Conditional statement (in) setting
        /// 条件文(in)設定
        /// </summary>
        /// <param name="column"></param>
        /// <param name="values"></param>
        /// <param name="type"></param>
        /// <param name="equal"></param>
        public void AddInCondition(string column, string[] values, Type type, bool equal)
        {
            if (values == null) return;
            this.AddOperator(true);
            DataAccessAttribute attribute = type.GetAttribute<DataAccessAttribute>();
            AddInConditionValue(column, attribute, values, equal);
        }

        /// <summary>
        /// Additional configuration conditional statement
        /// 条件文追加設定
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="isAnd"></param>
        /// <param name="equal"></param>
        public void AddCondition(string column, object value, Type type, bool isAnd, bool equal)
        {
            if (value == null) return;
            this.AddOperator(isAnd);
            DataAccessAttribute attribute = type.GetAttribute<DataAccessAttribute>();
            AddConditionValue(column, attribute, value.ToString(), equal);
        }

        /// <summary>
        /// Conditional statement (in) additional configuration
        /// 条件文(in)追加設定
        /// </summary>
        /// <param name="column"></param>
        /// <param name="values"></param>
        /// <param name="type"></param>
        /// <param name="isAnd"></param>
        /// <param name="equal"></param>
        public void AddInCondition(string column, string[] values, Type type, bool isAnd, bool equal)
        {
            if (values == null) return;
            this.AddOperator(isAnd);
            DataAccessAttribute attribute = type.GetAttribute<DataAccessAttribute>();
            AddInConditionValue(column, attribute, values, equal);
        }

        /// <summary>
        /// Condition value setting
        /// 条件値設定
        /// </summary>
        /// <param name="column"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <param name="equal"></param>
        private void AddConditionValue(string column, DataAccessAttribute attribute, string value, bool equal)
        {
            if (equal)
            {
                WhereSql.AppendLine(string.Format("{0}.{1} = '{2}'", attribute.TableName, column, value.ToString()));
            }
            else
            {
                WhereSql.AppendLine(string.Format("{0}.{1} != '{2}'", attribute.TableName, column, value.ToString()));
            }
        }

        /// <summary>
        /// Condition value (in) setting
        /// 条件値(in)設定
        /// </summary>
        /// <param name="column"></param>
        /// <param name="attribute"></param>
        /// <param name="values"></param>
        /// <param name="equal"></param>
        private void AddInConditionValue(string column, DataAccessAttribute attribute, string[] values, bool equal)
        {
            if (equal)
            {
                WhereSql.AppendLine(string.Format("{0}.{1} in ({2})", attribute.TableName, column, string.Format("'{0}'", string.Join("','", values))));
            }
            else
            {
                WhereSql.AppendLine(string.Format("{0}.{1} not in ({2})", attribute.TableName, column, string.Format("'{0}'", string.Join("','", values))));
            }
        }

        /// <summary>
        /// And Or statement setting
        /// And Or 文設定
        /// </summary>
        /// <param name="andOr"></param>
        private void AddOperator(bool isAnd)
        {
            if (string.IsNullOrEmpty(WhereSql.ToString()))
            {
                WhereSql.AppendLine("where");
            }
            else
            {
                if (isAnd)
                {
                    WhereSql.AppendLine("and");
                }
                else
                {
                    WhereSql.AppendLine("or");
                }
            }
        }

        #endregion

        #region テーブル結合 Add join table

        /// <summary>
        /// Add join table (And join only )
        /// 結合テーブル追加 (And結合のみ)
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="joinColumns">The same name row ,同名列</param>
        /// <param name="isInner"></param>
        public void AddJoinTable(Type baseType, Type joinType, string[] joinColumns, bool isInner = true)
        {
            DataAccessAttribute attribite = joinType.GetAttribute<DataAccessAttribute>();
            string tableName = baseType.GetAttribute<DataAccessAttribute>().TableName;
            string joinTableType = (isInner) ? "inner" : "left";
            StringBuilder joinConditions = new StringBuilder();

            for (int i = 0; i < joinColumns.Length; i++)
            {
                if (!i.Equals(0)) joinConditions.Append(" and ");
                joinConditions.Append(string.Format("{0}.{1} = {2}.{3}", tableName, joinColumns[i], attribite.TableName, joinColumns[i]));
            }

            JoinSql.AppendLine(string.Format("{0} join {1} on {2}", joinTableType, attribite.TableName, joinConditions.ToString()));

            this.AddSelectType(joinType);
        }

        /// <summary>
        /// Add join table
        /// 結合テーブル追加
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="joinColumns">同名列</param>
        /// <param name="isInner"></param>
        public void AddJoinTable(Type baseType, Type joinType, string joinColumn, bool isInner = true)
        {
            this.AddJoin(baseType, joinType, joinColumn, joinColumn, isInner);
        }

        /// <summary>
        /// Add join table
        /// 結合テーブル追加
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="joinType"></param>
        /// <param name="baseColumn"></param>
        /// <param name="joinColumn"></param>
        /// <param name="isInner"></param>
        public void AddJoinTable(Type baseType, Type joinType, string baseColumn, string joinColumn, bool isInner = true)
        {
            this.AddJoin(baseType, joinType, baseColumn, joinColumn, isInner);
        }

        /// <summary>
        /// Add join table(private)
        /// 結合テーブル追加(private)
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="joinType"></param>
        /// <param name="baseColumn"></param>
        /// <param name="joinColumn"></param>
        /// <param name="isInner"></param>
        private void AddJoin(Type baseType, Type joinType, string baseColumn, string joinColumn, bool isInner = true)
        {
            string joinTableName = joinType.GetAttribute<DataAccessAttribute>().TableName;
            string baseTableName = baseType.GetAttribute<DataAccessAttribute>().TableName;
            string joinTableType = (isInner) ? "inner" : "left";
            StringBuilder joinConditions = new StringBuilder();
            joinConditions.Append(string.Format("{0}.{1} = {2}.{3}", baseTableName, baseColumn, joinTableName, joinColumn));
            JoinSql.AppendLine(string.Format("{0} join {1} on {2}", joinTableType, joinTableName, joinConditions.ToString()));

            this.AddSelectType(joinType);
        }

        /// <summary>
        /// Add a column bound to when joining the table .
        /// テーブル結合時セレクト列を回収
        /// </summary>
        /// <param name="type"></param>
        private void AddSelectType(Type type)
        {
            if (!this.SelectType.ContainsKey(type))
            {
                this.SelectType.Add(type, this.GetSqliteColumn(type));
            }
        }

        #endregion

        #region ソート設定 Sort settings

        /// <summary>
        /// Add the sort field
        /// ソート項目追加
        /// </summary>
        /// <param name="column"></param>
        /// <param name="type"></param>
        /// <param name="isAsc"></param>
        public void AddOrderByColumns(string column, Type type, bool isAsc)
        {
            string[] baseArray = this.OrderByColumns;
            string[] newArray = new string[this.OrderByColumns.Length + 1];
            string format = "{0}.{1}" + (isAsc ? " asc" : " desc");
            Array.Copy(baseArray, newArray, Math.Min(baseArray.Length, newArray.Length));
            this.OrderByColumns = newArray;

            this.OrderByColumns[this.OrderByColumns.Length - 1] = string.Format(format, type.GetAttribute<DataAccessAttribute>().TableName, column);
        }

        /// <summary>
        /// Set the sort item
        /// ソート項目を設定
        /// </summary>
        /// <param name="columns">
        /// It is required is the name of the property of the same class .
        /// 同じクラスのプロパティ名であること
        /// </param>
        /// <param name="type"></param>
        public void SetOrderByColumns(string[] columns, Type type, bool isAsc)
        {
            string[] orderColumns = new string[columns.Length];
            string tableName = type.GetAttribute<DataAccessAttribute>().TableName;
            string format = "{0}.{1}" + (isAsc ? " asc" : " desc");

            for (int i = 0; i < columns.Length; i++)
            {
                orderColumns[i] = string.Format(format, tableName, columns[i]);
            }
            this.OrderByColumns = orderColumns;
        }

        #endregion

        #region プライマリーキー、値、SQLite列回収 Primary key , value , SQLite column recovery

        /// <summary>
        /// Recovering the primary key and values
        /// プライマリキーと値を回収
        /// </summary>
        private void GetPrimaryKeyValue(object component)
        {
            Type type = component.GetType();
            PropertyInfo[] propertyInfoArray = type.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                if (propertyInfo.DeclaringType.Equals(type))
                {
                    SqlitePropertyAttribute attribute = propertyInfo.GetAttribute<SqlitePropertyAttribute>();
                    if (attribute != null)
                    {
                        if (attribute.IsPrimaryKey)
                        {
                            this.AddPrimaryKey(propertyInfo.GetGetMethod().Invoke(component, null), propertyInfo.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add a primary key
        /// プライマリキーを追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void AddPrimaryKey(object obj, string properyName)
        {
            if (obj != null)
            {
                if (!this.PrimarySql.ContainsKey(properyName))
                {
                    this.PrimarySql.Add(properyName, obj.ToString());
                }
            }
            else
            {
                if (!this.PrimarySql.ContainsKey(properyName))
                {
                    this.PrimarySql.Add(properyName, string.Empty);
                }
            }

        }

        /// <summary>
        /// Get the column that is included in the SQLiteComponent
        /// SQLiteComponentに含まれる列を取得
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetSqliteColumn(Type type)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool first = true;
            string tableName = type.GetAttribute<DataAccessAttribute>().TableName;
            string format = "{0}.{1} as '{2}.{3}'";
            PropertyInfo[] propertyInfoArray = type.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                SqlitePropertyAttribute attribute = propertyInfo.GetAttribute<SqlitePropertyAttribute>();

                if (attribute != null)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        stringBuilder.Append(",");
                    }

                    stringBuilder.AppendLine(string.Format(format, tableName, propertyInfo.Name, tableName, propertyInfo.Name));
                }
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region セレクト文生成 Select SQL statement generation

        /// <summary>
        /// Select SQL statement generation
        /// セレクトSQL文生成
        /// </summary>
        public void CreateSelectSql()
        {
            this.ExecutetSql = new StringBuilder();
            DataAccessAttribute attribute = this.SelectType.Keys.First().GetAttribute<DataAccessAttribute>();
            ExecutetSql.AppendLine(string.Format(SELECT_SQL, this.CreateSelectColumn(), attribute.TableName));
            ExecutetSql.AppendLine(this.JoinSql.ToString());
            ExecutetSql.AppendLine(this.WhereSql.ToString());

            if (!this.OrderByColumns.Length.Equals(0))
            {
                ExecutetSql.AppendLine(string.Format(ORDER_BY_SQL, string.Join(",", this.OrderByColumns)));
            }
        }

        /// <summary>
        /// Generating a select SQL Retsubun
        /// セレクトSQL列文を生成
        /// </summary>
        /// <returns></returns>
        private string CreateSelectColumn()
        {
            bool firstTable = true;
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Type type in this.SelectType.Keys)
            {
                if (firstTable)
                {
                    firstTable = false;
                }
                else
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.AppendLine(this.SelectType[type]);
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region データ操作SQL生成

        /// <summary>
        /// Insert SQL statement generation
        /// インサートSQL文生成
        /// </summary>
        /// <param name="component"></param>
        public void CreateInsertSql(object component)
        {
            this.SetConnectDbName(component.GetType());
            this.ExecutetSql = new StringBuilder();
            string sql = string.Format(INSERT_SQL, component.GetType().GetAttribute<DataAccessAttribute>().TableName, this.GetInsertUpdateSql(component, true));
            this.ExecutetSql.AppendLine(sql);
            //Debug.Log(this.ExecutetSql.ToString());
        }

        /// <summary>
        /// Update SQL statement generation
        /// アップデートSQL文生成 
        /// </summary>
        /// <param name="component"></param>
        public void CreateUpdateSql(object component)
        {
            this.SetConnectDbName(component.GetType());
            this.ExecutetSql = new StringBuilder();
            string tableName = component.GetType().GetAttribute<DataAccessAttribute>().TableName;
            string sql = string.Format(UPDATE_SQL, tableName);
            this.ExecutetSql.AppendLine(sql);
            this.ExecutetSql.AppendLine(this.GetInsertUpdateSql(component, false));
            this.CreateDeleteUpdateWhere(tableName, component);
            //Debug.Log(this.ExecutetSql.ToString());
        }

        /// <summary>
        /// Delete SQL statement generation
        /// デリートSQL文生成
        /// </summary>
        /// <param name="component"></param>
        public void CreateDeleteSql(object component)
        {
            this.SetConnectDbName(component.GetType());
            this.ExecutetSql = new StringBuilder();
            string tableName = component.GetType().GetAttribute<DataAccessAttribute>().TableName;
            string sql = string.Format(DELETE_SQL, tableName);
            this.ExecutetSql.AppendLine(sql);

            this.GetPrimaryKeyValue(component);
            this.CreateDeleteUpdateWhere(tableName, component);
            //Debug.Log(this.ExecutetSql.ToString());
        }

        #region 条件生成 Condition generation

        /// <summary>
        /// Updates , common the condition generation of delete statement .
        /// アップデート、デリート文の条件生成を共通化。
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="componet"></param>
        private void CreateDeleteUpdateWhere(string tableName, object componet)
        {
            if (!this.WhereSql.ToString().Equals(string.Empty))
            {
                if (!this.JoinSql.ToString().Equals(string.Empty))
                {
                    this.ExecutetSql.AppendLine(string.Format("from {0}", tableName));
                    this.ExecutetSql.AppendLine(this.JoinSql.ToString());
                }
                this.ExecutetSql.AppendLine(this.WhereSql.ToString());
            }
            else
            {
                CreatePrimaryWhere(componet.GetType());
            }
        }

        /// <summary>
        /// Create conditional statements from the primary key
        /// プライマリキーより条件文作成
        /// </summary>
        /// <param name="type"></param>
        private void CreatePrimaryWhere(Type type)
        {
            foreach (string key in PrimarySql.Keys)
            {
                this.AddCondition(key, PrimarySql[key], type, true);
            }
            this.ExecutetSql.AppendLine(this.WhereSql.ToString());
        }

        #endregion

        /// <summary>
        /// Registration , update for SQL contents generation . Primary key recovery .
        /// 登録、更新用SQL中身生成。プライマリキー回収。
        /// </summary>
        /// <param name="component"></param>
        /// <param name="isInsert"></param>
        /// <returns></returns>
        private string GetInsertUpdateSql(object component, bool isInsert)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder propertyBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            string insertSql = " ({0}) values ({1})";
            string updateSql = "{0} = '{1}'";
            bool firstProperty = true;
            Type type = component.GetType();
            PropertyInfo[] propertyInfoArray = type.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                SqlitePropertyAttribute attribute = propertyInfo.GetAttribute<SqlitePropertyAttribute>();
                if (attribute != null)
                {
                    object value = this.GetSqliteTypeValue(propertyInfo, component);

                    // プライマリキー取得
                    // Primary key acquisition
                    if (attribute.IsPrimaryKey)
                    {
                        this.AddPrimaryKey(value, propertyInfo.Name);
                        continue;
                    }

                    if (value == null && !isInsert) continue;

                    if (firstProperty)
                    {
                        firstProperty = false;
                    }
                    else
                    {
                        stringBuilder.Append(",");
                        propertyBuilder.Append(",");
                    }

                    // insert
                    if (isInsert)
                    {
                        stringBuilder.AppendLine(string.Format("'{0}'", (value == null) ? "" : value.ToString()));
                        propertyBuilder.AppendLine(propertyInfo.Name);
                    }
                    // update
                    else
                    {
                        stringBuilder.AppendLine(string.Format(updateSql, propertyInfo.Name, value.ToString()));
                    }
                }
            }

            if (isInsert)
            {
                return string.Format(insertSql, propertyBuilder.ToString(), stringBuilder.ToString());
            }
            else
            {
                return stringBuilder.ToString();
            }
        }

        private object GetSqliteTypeValue(PropertyInfo propertyInfo, object component)
        {
            object value = null;

            // 日付
            if (propertyInfo.PropertyType.Equals(typeof(DateTime)))
            {
                value = this.GetDateFormat((DateTime)propertyInfo.GetGetMethod().Invoke(component, null));
            }
            // 論理
            else if (propertyInfo.PropertyType.Equals(typeof(bool)))
            {
                bool flg = (bool)propertyInfo.GetGetMethod().Invoke(component, null);
                value = (flg) ? "1" : "0";
            }
            // 文字列、数値
            else
            {
                value = propertyInfo.GetGetMethod().Invoke(component, null);
            }

            return value;
        }

        #endregion

        #region テーブル操作SQL

        /// <summary>
        /// テーブル生成SQL作成
        /// </summary>
        /// <param name="tableType"></param>
        public void CreateTableSql(Type tableType)
        {
            this.SetConnectDbName(tableType);
            List<string> columnSql = new List<string>();
            string tableName = tableType.GetAttribute<DataAccessAttribute>().TableName;

            PropertyInfo[] propertyInfoArray = tableType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                string sql = this.CreateColumnSql(propertyInfo);
                if (!string.IsNullOrEmpty(sql))
                {
                    columnSql.Add(sql);
                }
            }

            this.ExecutetSql.AppendLine(string.Format(CREATE_TABLE_SQL, tableName, string.Join(",", columnSql.ToArray())));
        }

        /// <summary>
        /// テーブル列SQL作成
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        private string CreateColumnSql(PropertyInfo propertyInfo)
        {
            List<string> columnSql = new List<string>();
            SqlitePropertyAttribute attribute = propertyInfo.GetAttribute<SqlitePropertyAttribute>();
            if (attribute == null) return string.Empty;
            columnSql.Add(propertyInfo.Name);

            // integer
            if (propertyInfo.PropertyType.Equals(typeof(int)))
                columnSql.Add("integer");
            // real
            else if (propertyInfo.PropertyType.Equals(typeof(float)) || propertyInfo.PropertyType.Equals(typeof(double)))
                columnSql.Add("real");
            // text
            else
                columnSql.Add("text");

            if (attribute.IsPrimaryKey)
                columnSql.Add("primary key");
            if (!attribute.NullAble)
                columnSql.Add("not null");

            return string.Join(" ", columnSql.ToArray());
        }

        /// <summary>
        /// テーブルが存在するかチェックを行う
        /// </summary>
        /// <returns></returns>
        public void CreateTableCheckSql(Type tableType)
        {
            this.SetConnectDbName(tableType);
            this.ExecutetSql.Append(string.Format("select count(*) as Count from sqlite_master where type='table' and name='{0}'", tableType.GetAttribute<DataAccessAttribute>().TableName));
        }

        /// <summary>
        /// テーブル一覧を取得するSQLを作成
        /// </summary>
        /// <param name="tableType">取得したいテーブルに含まれるクラス</param>
        public void CreateTableListSql(Type tableType)
        {
            this.SetConnectDbName(tableType);
            this.ExecutetSql.Append("select name as TableName from sqlite_master where type='table'");
        }

        /// <summary>
        /// テーブル削除SQLを作成
        /// </summary>
        /// <param name="tableType"></param>
        public void DropTableSql(Type tableType)
        {
            this.SetConnectDbName(tableType);
            this.ExecutetSql.Append(string.Format("drop table {0}", tableType.GetAttribute<DataAccessAttribute>().TableName));
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        private void SetConnectDbName(Type type)
        {
            this.DbName = type.GetAttribute<DataAccessAttribute>().DbName;
        }

        /// <summary>
        /// The format of the registration renewal date
        /// 登録更新日付のフォーマット
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private string GetDateFormat(DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd hh:mm:ss");
        }
    }
}
