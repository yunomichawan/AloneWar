using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("SkillLevelData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class SkillLevelData : SqliteBaseData
    {
        [SqliteProperty]
        public int SkillId { get; set; }

        [SqliteProperty]
        public int Level { get; set; }
    }
}
