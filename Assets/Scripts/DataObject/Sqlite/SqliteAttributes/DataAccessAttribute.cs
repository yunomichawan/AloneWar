﻿using System;

namespace AloneWar.DataObject.Sqlite.SqliteAttributes
{
/// <summary>
/// テーブル名を定義する属性
/// Attributes that define the table name
/// </summary>
    public class DataAccessAttribute : Attribute
    {
        public string TableName { get; set; }

        public string DbName { get; set; }

        public DataAccessAttribute(string tableName,string dbName)
        {
            this.TableName = tableName;
            this.DbName = dbName;
        }
    }
}
