using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("EventStatusData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class EventStatusData : SqliteBaseData
    {
        [SqliteProperty]
        public int EventId { get; set; }

        [SqliteProperty]
        public int Count { get; set; }

    }
}