using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// ユニット成長パラム
    /// </summary>
    [DataAccess("GrowTableData")]
    public class GrowTableData : SqliteBaseData
    {
        [SqliteProperty]
        public int UnitId { get; set; }

    }
}
