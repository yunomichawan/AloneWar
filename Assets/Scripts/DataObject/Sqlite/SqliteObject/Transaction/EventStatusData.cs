using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

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