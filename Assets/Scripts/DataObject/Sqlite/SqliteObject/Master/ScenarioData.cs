using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// シナリオテーブル
    /// </summary>
    [DataAccess("ScenarioData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class ScenarioData : SqliteBaseData
    {

        [SqliteProperty]
        public string ScenarioPath { get; set; }

        [SqliteProperty]
        public int ScenarioCategory { get; set; }

    }
}
