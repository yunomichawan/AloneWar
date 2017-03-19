using AloneWar.Common.Component;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using System.Collections.Generic;
using AloneWar.DataObject;
using AloneWar.Common;

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
