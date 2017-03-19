using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 付与するパラメーター
    /// ・Item,Event,Skill,etc...more?
    /// </summary>
    [DataAccess("StatusGrantedParamData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class StatusGrantedParamData : SqliteBaseData
    {
        [SqliteProperty]
        public int StatusGrantedId { get; set; }

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

    }
}
