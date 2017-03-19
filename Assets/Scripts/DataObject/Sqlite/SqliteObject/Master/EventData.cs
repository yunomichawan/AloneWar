using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    [DataAccess("EventData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class EventData : SqliteBaseData
    {

        [SqliteProperty]
        public int EventCategory { get; set; }

        [SqliteProperty]
        public string EventSender { get; set; }
    }
}
