using AloneWar.Unit.Status;

namespace AloneWar.Unit.Component
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitSubComponent : UnitBaseComponent
    {
        public UnitSubStatus UnitSubStatus { get; set; }

        public override UnitBaseStatus UnitBaseStatus
        {
            get
            {
                return this.UnitSubStatus;
            }
        }
    }
}
