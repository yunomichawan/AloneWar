using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    [DataAccess("ScenarioEventTriggerData", AloneWarConst.SqliteDataBaseName.Master)]
    public class ScenarioEventTriggerData : SqliteBaseData
    {
        [SqliteProperty]
        public int ScenarioId { get; set; }

        [SqliteProperty]
        public int TriggerId { get; set; }
    }
}
