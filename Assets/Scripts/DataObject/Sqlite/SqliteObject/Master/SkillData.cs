using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// スキル管理テーブル
    /// </summary>
    [DataAccess("SkillData", AloneWarConst.SqliteDataBaseName.Master)]
    public class SkillData : SqliteBaseData,IDefaultSort
    {
        [SqliteProperty]
        public int SkillCategory { get; set; }

        [SqliteProperty]
        public int VaildTurnCount { get; set; }

        [SqliteProperty]
        public int SortNo { get; set; }
    }
}
