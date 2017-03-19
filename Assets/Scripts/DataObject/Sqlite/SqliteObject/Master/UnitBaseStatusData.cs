using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// ユニット基本ステータス
    /// </summary>
    [DataAccess("UnitBaseStatusData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class UnitBaseStatusData : SqliteBaseData
    {
        #region

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
        public int MainCommandCategory { get; set; }

        [SqliteProperty]
        public int SubCommandCategory { get; set; }

        [SqliteProperty]
        public int Experience { get; set; }

        [SqliteProperty]
        public int AssetId { get; set; }

        #endregion

        #region property

        public CommandCategory MainCommand
        {
            get
            {
                return (CommandCategory)this.MainCommandCategory;
            }
        }

        public CommandCategory SubCommand
        {
            get
            {
                return (CommandCategory)this.SubCommandCategory;
            }
        }

        #endregion

    }
}
