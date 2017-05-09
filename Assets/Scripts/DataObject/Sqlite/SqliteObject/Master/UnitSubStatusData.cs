using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using System;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// サブユニットステータス
    /// </summary>
    [DataAccess("UnitSubStatusData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    [Serializable]
    public class UnitSubStatusData : SqliteBaseData
    {
        /// <summary>
        /// FK UnitBaseStatusData.Id
        /// </summary>
        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int CostHp { get; set; }

        [SqliteProperty]
        public int CostMp { get; set; }
    }
}
