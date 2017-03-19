using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    [DataAccess("UnitStageSkillData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class UnitStageSkillData : SqliteBaseData, IForeignKey
    {
        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int StageId { get; set; }

        [SqliteProperty]
        public int SkillId { get; set; }

        public int ForeignKey { get { return this.SkillId; } }

    }
}
