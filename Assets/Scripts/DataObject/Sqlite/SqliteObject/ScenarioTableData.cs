using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// シナリオテーブル
    /// </summary>
    [DataAccess("ScenarioTableData")]
    public class ScenarioTableData : SqliteBaseData
    {
        [SqliteProperty]
        public int No { get; set; }

        [SqliteProperty]
        public string ScenarioPath { get; set; }

    }
}
