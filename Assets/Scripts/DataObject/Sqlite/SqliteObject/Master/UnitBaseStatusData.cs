using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// ユニット基本ステータス
    /// </summary>
    [DataAccess("UnitBaseStatusData", AloneWarConst.SqliteDataBaseName.Master)]
    public class UnitBaseStatusData : SqliteBaseData
    {
        [SqliteProperty]
        public string Name { get; set; }

        [SqliteProperty]
        public int Hp { get; set; }

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
        public int Experience { get; set; }

        [SqliteProperty]
        public int AssetId { get; set; }

    }
}
