using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    [DataAccess("ScenarioEventData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class ScenarioEventData : SqliteBaseData
    {
        [SqliteProperty]
        public int ScenarioId { get; set; }

        [SqliteProperty]
        public int EventId { get; set; }
    }
}
