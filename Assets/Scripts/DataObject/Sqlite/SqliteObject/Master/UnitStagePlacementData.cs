using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("UnitStagePlacementData", AloneWarConst.SqliteDataBaseName.Master)]
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
