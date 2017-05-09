using AloneWar.Stage;
using AloneWar.Unit.Component;
using System.Linq;

namespace AloneWar.Unit.UnitAI
{
    public class UnitTargetMainAI : UnitBaseAI
    {
        public UnitTargetMainAI(UnitSubComponent unitSubComponent)
            : base(unitSubComponent)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void SetMoveTarget()
        {
            // [SetTarget]が役割を兼ねているため不要
        }

        /// <summary>
        /// 
        /// </summary>
        public override void SetTarget()
        {
            this.Init();
            this.SeachUnitFromTarget(StageManager.Instance.StageInformation.UnitMainComponent.UnitBaseStatus.StageStatus.Id);
            UnitSummaryComponent unitSummaryComponent = StageManager.Instance.StageInformation.SearchUnit(u =>
            {
                return this.UnitSubComponent.SubRange >= StageManager.Instance.StageInformation.GetSenceOfDistance(u.PositionId, this.TargetMovePosition);
            });
            this.PredictionBattleResultList = this.AggregatePredictionBattleResult(unitSummaryComponent);
            this.SetUnitTargetPriority();

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void SetUnitTargetPriority()
        {
            this.PredictionBattleResultList = (from result in this.PredictionBattleResultList
                                               orderby result.DefenceBattleResultInfo.UnitBaseStatus.StageStatus.PositionId.Equals(StageManager.Instance.StageInformation.UnitMainComponent.PositionId),
                                                   result.ResultExpectedValue descending
                                               select result).ToList();
        }
    }
}
