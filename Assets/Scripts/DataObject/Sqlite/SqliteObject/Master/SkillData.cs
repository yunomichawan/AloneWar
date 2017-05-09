﻿using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// スキル管理テーブル
    /// </summary>
    [DataAccess("SkillData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class SkillData : SqliteBaseData,IDefaultSort
    {
        [SqliteProperty]
        public int SkillCategory { get; set; }

        [SqliteProperty]
        public int ValidTurnCount { get; set; }

        [SqliteProperty]
        public int SortNo { get; set; }
    }
}
