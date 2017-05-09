﻿using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    [DataAccess("ScenarioEventTriggerData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class ScenarioEventTriggerData : SqliteBaseData
    {
        [SqliteProperty]
        public int ScenarioId { get; set; }

        [SqliteProperty]
        public int TriggerId { get; set; }
    }
}
