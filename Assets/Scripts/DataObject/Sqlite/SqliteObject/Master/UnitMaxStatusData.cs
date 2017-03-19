using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("UnitMaxStatusData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class UnitMaxStatusData 
    {
        [SqliteProperty]
        public int UnitId { get; set; }

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
