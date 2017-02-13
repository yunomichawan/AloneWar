using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("ExperienceData", AloneWarConst.SqliteDataBaseName.Master)]
    public class ExperienceData : SqliteBaseData
    {
        [SqliteProperty]
        public int CommandCategory { get; set; }

        [SqliteProperty]
        public int Experience { get; set; }

    }
}
