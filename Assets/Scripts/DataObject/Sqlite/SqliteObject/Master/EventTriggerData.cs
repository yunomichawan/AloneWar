using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    [DataAccess("EventTriggerData", AloneWarConst.SqliteDataBaseName.Master)]
    public class EventTriggerData : SqliteBaseData
    {

        [SqliteProperty]
        public int TriggerCategory { get; set; }

        [SqliteProperty]
        public string TriggerSender { get; set; }
    }
}
