using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

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
