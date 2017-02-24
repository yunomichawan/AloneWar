using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// メインユニットステータス
    /// </summary>
    [DataAccess("UnitMainStatusData", AloneWarConst.SqliteDataBaseName.Master)]
    [Serializable]
    public class UnitMainStatusData : SqliteBaseData
    {
        /// <summary>
        /// FK UnitBaseStatusData.Id
        /// </summary>
        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int Mp { get; set; }

        [SqliteProperty]
        public int SummonRange { get; set; }

        [SqliteProperty]
        public string Rank { get; set; }

        [SqliteProperty]
        public int UnitCategory { get; set; }

        [SqliteProperty]
        public int MainExperience { get; set; }

    }
}
