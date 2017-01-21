using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    [DataAccess("RUnitSkillData")]
    public class RUnitSkillData : SqliteBaseData
    {
        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int SkillId { get; set; }
    }
}
