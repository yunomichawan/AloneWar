using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Unit.Component;
using AloneWar.Stage.Component;
using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.Unit.UnitAI;
using AloneWar.Battle;

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
                        break;
                    case AiCategory.SacrificedPiece:
                        break;
                    case AiCategory.Simple:
                        break;
                    case AiCategory.TargetMain:
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
                this.SetAITarget(ai);
                ai.SetAITask();
            }
        }

        /// <summary>
        /// Aiの目標を決める
        /// </summary>
        private void SetAITarget(UnitBaseAI ai)
        {
            PredictionBattleResult result = ai.PredictionBattleResultList.FirstOrDefault();
            if (result != null)
            {
                // 優先度の高い対象の存在チェック
                if (StageManager.Instance.StageInformation.SearchUnit(result.TempPositionId, ai.UnitSubComponent.UnitSide.Reverse()).IsEmpty)
                {
                    ai.SetTarget();
                    // 再帰
                    this.SetAITarget(ai);
                    // もしくは終了していないユニットのみで再集計を行い行動順を再設定する。
                }
                // 確定座標を設定
                ai.TargetMovePosition = result.TempPositionId;
                ai.TargetUnitPosition = result.DefenceBattleResultInfo.UnitBaseStatus.StageStatus.PositionId;
            }
        }
    }
}