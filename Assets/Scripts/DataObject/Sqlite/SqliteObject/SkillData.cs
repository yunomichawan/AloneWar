using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// スキル管理テーブル
    /// </summary>
    [DataAccess("SkillData")]
    public class SkillData : SqliteBaseData
    {
        [SqliteProperty]
        public string Category { get; set; }

    }
}
