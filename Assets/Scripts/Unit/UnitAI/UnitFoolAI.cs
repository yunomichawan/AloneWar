using AloneWar.Unit.Component;
using System.Linq;

namespace AloneWar.Unit.UnitAI
{
    public class UnitFoolAI : UnitBaseAI
    {
        public UnitFoolAI(UnitSubComponent unitSubComponent)
            : base(unitSubComponent)
        {

        }

        /// <summary>
        /// 優先度設定
        /// </summary>
        protected override void SetUnitTargetPriority()
        {
            // 期待値昇順ソート
            this.PredictionBattleResultList = this.PredictionBattleResultList.OrderBy(p => p.ResultExpectedValue).ToList();
        }
    }
}
