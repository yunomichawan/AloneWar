using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// アイテム管理テーブル
    /// </summary>
    [DataAccess("ItemData")]
    public class ItemData : SqliteBaseData
    {
        [SqliteProperty]
        public string Category { get; set; }
    }
}
