using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    [DataAccess("EventTriggerData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class EventTriggerData : SqliteBaseData
    {

        [SqliteProperty]
        public int TriggerCategory { get; set; }

        [SqliteProperty]
        public string TriggerSender { get; set; }

        public EventTriggerCategory Category { get { return (EventTriggerCategory)this.TriggerCategory; } }
    }
}
