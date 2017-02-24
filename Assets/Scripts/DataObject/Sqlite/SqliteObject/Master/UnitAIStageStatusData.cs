using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("UnitAIStageStatusData", AloneWarConst.SqliteDataBaseName.Master)]
    public class UnitAIStageStatusData : SqliteBaseData
    {
        #region SqliteProperty

        [SqliteProperty]
        public int UnitStageId { get; set; }

        [SqliteProperty]
        public int Level { get; set; }

        [SqliteProperty]
        public int AiCategory { get; set; }

        [SqliteProperty]
        public string SearchArea { get; set; }

        #endregion
    }
}
