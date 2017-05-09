using AloneWar.DataObject;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;

namespace AloneWar.Unit.Status
{
    public abstract class UnitBaseStatus
    {
        #region get only

        public int DamageHp
        {
            get
            {
                return this.BaseStatus.Hp - this.StageStatus.Damage;
            }
        }

        #endregion

        public UnitBaseStatusData BaseStatus { get; set; }

        public UnitStageStatusData StageStatus { get; set; }

        public UnitAssetData UnitAssetData { get; set; }

        /// <summary>
        /// Main:UnitItemData
        /// Sub:UnitStageItemData
        /// </summary>
        public ForeignKeyObject<ItemData> UnitItemDataList { get; set; }

        /// <summary>
        /// Main:UnitSkillData
        /// Sub:UnitStageStatusData
        /// </summary>
        public ForeignKeyObject<SkillData> UnitSkillDataList { get; set; }

    }
}
