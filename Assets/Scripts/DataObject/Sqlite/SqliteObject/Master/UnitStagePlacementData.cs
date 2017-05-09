using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("UnitStagePlacementData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class UnitStagePlacementData : SqliteBaseData
    {
        #region SqliteProperty

        [SqliteProperty]
        public int StageId { get; set; }

        [SqliteProperty]
        public int PositionId { get; set; }

        #endregion

    }
}
