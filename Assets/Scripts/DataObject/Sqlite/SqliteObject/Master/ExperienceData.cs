using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("ExperienceData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class ExperienceData : SqliteBaseData
    {
        [SqliteProperty]
        public int CommandCategory { get; set; }

        [SqliteProperty]
        public int Experience { get; set; }

    }
}
