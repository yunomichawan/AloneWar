using AloneWar.Unit.Status;

namespace AloneWar.Unit.Component
{

    /// <summary>
    /// 
    /// </summary>
    public class UnitMainComponent : UnitBaseComponent
    {
        public UnitMainStatus UnitMainStatus { get; set; }

        #region override

        public override int SubRange
        {
            get
            {
                return this.UnitMainStatus.UnitMainStatusData.SummonRange;
            }
        }

        public override UnitBaseStatus UnitBaseStatus
        {
            get
            {
                return this.UnitMainStatus;
            }
        }

        #endregion
    }
}
