using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

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
