using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("HaveSkillData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class HaveSkillData : SqliteBaseData
    {
        [SqliteProperty]
        public int SkillId { get; set; }

        [SqliteProperty]
        public int UnitId { get; set; }

    }
}
