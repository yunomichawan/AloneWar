using System;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Base
{
    /// <summary>
    /// Sqliteテーブルの基本クラス
    /// </summary>
    [SqliteProperty]
    public abstract class SqliteBaseData
    {
        [PrimaryKey]
        public int Id { get; set; }
    }
}
