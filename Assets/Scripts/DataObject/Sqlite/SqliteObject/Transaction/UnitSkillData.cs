using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    [DataAccess("UnitSkillData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class UnitSkillData : SqliteBaseData,IForeignKey
    {
        #region SqliteProperty

        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int SkillId { get; set; }

        #endregion

        /// <summary>
        /// 関連付け項目(SkillTableData)
        /// </summary>
        public int ForeignKey { get { return this.SkillId; } }
    }
}
