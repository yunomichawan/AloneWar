using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// シナリオテーブル
    /// </summary>
    [DataAccess("ScenarioData", AloneWarConst.SqliteDataBaseName.Master)]
    public class ScenarioData : SqliteBaseData
    {

        [SqliteProperty]
        public string ScenarioPath { get; set; }

        [SqliteProperty]
        public int ScenarioCategory { get; set; }

    }
}
