﻿using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("UnitGrowMasterData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class UnitGrowMasterData : SqliteBaseData
    {
        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int Level { get; set; }

        [SqliteProperty]
        public int Hp { get; set; }

        [SqliteProperty]
        public int Mp { get; set; }

        [SqliteProperty]
        public int Attack { get; set; }

        [SqliteProperty]
        public int Defence { get; set; }

        [SqliteProperty]
        public int Range { get; set; }

        [SqliteProperty]
        public int InvalidRange { get; set; }

        [SqliteProperty]
        public int Move { get; set; }

        [SqliteProperty]
        public int Luck { get; set; }

        [SqliteProperty]
        public int Hit { get; set; }

        [SqliteProperty]
        public int Avoid { get; set; }

        [SqliteProperty]
        public int Cost { get; set; }
    }
}
