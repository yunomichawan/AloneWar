using AloneWar.Common;
using AloneWar.Unit.Component;
using AloneWar.Unit.UnitAI;
using System.Collections.Generic;
using System.Linq;

namespace AloneWar.Stage.Controller.Unit
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitAIController
    {
        /// <summary>
        /// 
        /// </summary>
        public void SetAITaskLsit(UnitSideCategory unitSideCategory)
        {
            UnitSummaryComponent unitSummaryComponent = StageManager.Instance.StageInformation.SearchUnit(unitSideCategory);
            // ソート条件をArea,MaxResultExpectedValue desc
            // でないと行動順がかなりバラバラになってしまう。このソートでもバラバラにならないわけではないがやらないよかまし？
            Queue<UnitBaseAI> aiQueue = new Queue<UnitBaseAI>(this.ConvertToAiQueue(unitSummaryComponent.UnitSubComponentList)
                .OrderBy(a => a.UnitSubComponent.Area)
                // 期待値が優先順位となっていない場合もあるため考慮が必要
                .OrderByDescending(a => a.MaxResultExpectedValue));
            this.CreateAIQueueFromPrediction(aiQueue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitSubComponentList"></param>
        /// <returns></returns>
        private Queue<UnitBaseAI> ConvertToAiQueue(List<UnitSubComponent> unitSubComponentList)
        {
            Queue<UnitBaseAI> aiQueue = new Queue<UnitBaseAI>();
            unitSubComponentList.ForEach(u =>
            {
                UnitBaseAI unitBaseAI = null;
                switch (u.UnitSubStatus.UnitAIStageStatusData.Ai)
                {
                    case AiCategory.Cooperation:
                        break;
                    case AiCategory.Escape:
                        break;
                    case AiCategory.Fool:
                        break;
                    case AiCategory.InRange:
                        unitBaseAI = new UnitInRangeAI(u);
                        break;
                    case AiCategory.SacrificedPiece:
                        break;
                    case AiCategory.Simple:
                        break;
                    case AiCategory.TargetMain:
                        unitBaseAI = new UnitTargetMainAI(u);
                        break;
                    case AiCategory.Wait:
                        unitBaseAI = new UnitWaitAI(u);
                        break;
                }

                if (unitBaseAI != null)
                {
                    unitBaseAI.SetTarget();
                    aiQueue.Enqueue(unitBaseAI);
                }
            });

            return aiQueue;
        }

        /// <summary>
        /// 予測結果を基に確定キューを作成
        /// </summary>
        /// <returns></returns>
        private void CreateAIQueueFromPrediction(Queue<UnitBaseAI> predictionQueue)
        {
            while (true)
            {
                // キューが無くなり次第終了
                if (predictionQueue.Count < 0)
                    break;
                // 
                UnitBaseAI ai = predictionQueue.Dequeue();
                ai.SetAITask();
            }
        }
    }
}