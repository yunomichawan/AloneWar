using AloneWar.Common;
using AloneWar.Unit.Component;

namespace AloneWar.Unit.UnitAI
{
    public class UnitWaitAI : UnitBaseAI
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitSubComponent"></param>
        public UnitWaitAI(UnitSubComponent unitSubComponent)
            : base(unitSubComponent)
        {

        }

        public override void SetTarget()
        {
            // 範囲を再設定後に通常の処理を行う
            this.UnitSubComponent.StageRange.SetRange(CommandCategory.Attack, CommandCategory.None, this.UnitSubComponent.SubRange, 0, this.UnitSubComponent.InvalidRange, 0);
            base.SetTarget();
        }

        /// <summary>
        ///  座標決定
        /// </summary>
        protected override void SetMoveTarget()
        {
            // 移動を行わない
            this.TargetMovePosition = AloneWarConst.ErrorPositionId;
        }
    }
}
