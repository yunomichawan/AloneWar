using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// ステージユニット配置管理テーブル
    /// </summary>
    [DataAccess("UnitStageStatusData")]
    public class UnitStageStatusData : SqliteBaseData
    {
        [SqliteProperty]
        public int StageId { get; set; }

        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int AiCategory { get; set; }

        [SqliteProperty]
        public int PositionId { get; set; }

    }
}
