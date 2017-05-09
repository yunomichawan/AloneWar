using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// ステージユニット配置管理テーブル
    /// </summary>
    [DataAccess("UnitStageStatusData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class UnitStageStatusData : SqliteBaseData,IDefaultSort
    {
        #region SqliteProperty

        [SqliteProperty]
        public int StageId { get; set; }

        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int PositionId { get; set; }

        [SqliteProperty]
        public int UnitSideCategory { get; set; }

        [SqliteProperty]
        public int Damage { get; set; }

        [SqliteProperty]
        public bool IsEvent { get; set; }

        [SqliteProperty]
        public int SortNo { get; set; }

        #endregion

        #region Property

        public UnitSideCategory UnitSide { get { return (UnitSideCategory)this.UnitSideCategory; } }

        public bool Wait { get; set; }

        public int BeforePositionId { get; set; }

        #endregion

    }
}
