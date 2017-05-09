using AloneWar.Common;
using AloneWar.Unit.Component;

namespace AloneWar.Unit.UnitAI
{
    public class UnitInRangeAI : UnitBaseAI
    {
        public UnitInRangeAI(UnitSubComponent unitSubComponent)
            : base(unitSubComponent)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void SetMoveTarget()
        {
            // 移動を行わない
            this.TargetMovePosition = AloneWarConst.ErrorPositionId;
        }
    }
}
