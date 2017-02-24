using AloneWar.Unit.Component;
using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.Battle;
using System.Collections;
using System.Collections.Generic;
using AloneWar.Stage;
using AloneWar.Stage.Controller.Unit;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

namespace AloneWar.Unit.UnitAI.Base
{
    public abstract class UnitBaseAI
    {
        public int TargetMovePosition { get; set; }

        public int TargetUnitPosition { get; set; }

        public UnitSubComponent UnitSubComponent { get; set; }

        public UnitBaseAI(UnitSubComponent unitSubComponent)
        {
            this.UnitSubComponent = unitSubComponent;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            this.TargetMovePosition = AloneWarConst.ErrorPositionId;
            this.TargetUnitPosition = AloneWarConst.ErrorPositionId;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void SetTarget()
        {
            UnitSummaryComponent unitSummaryComponent = this.UnitSubComponent.UnitRange.SearchRange(UnitSideCategory.Player);
            if (unitSummaryComponent.IsEmpty)
            {
                this.SearchUnitFromInRange(unitSummaryComponent);
            }

            // 
            if (this.TargetMovePosition.Equals(AloneWarConst.ErrorPositionId))
            {
                this.SearchUnitFromAI();
            }
        }

        /// <summary>
        /// 範囲内から目標(移動、ユニット)を決定
        /// </summary>
        protected void SearchUnitFromInRange(UnitSummaryComponent unitSummaryComponent)
        {
            // main priority
            if (unitSummaryComponent.UnitMainComponent != null)
            {
                this.TargetUnitPosition = unitSummaryComponent.UnitMainComponent.PositionId;
                this.TargetMovePosition = this.GetPositionIdFromInRangeUnit(this.TargetUnitPosition, this.UnitSubComponent.SubRange, this.UnitSubComponent.InvalidRange);
            }

            if (this.TargetMovePosition.Equals(AloneWarConst.ErrorPositionId))
            {
                PredictionBattleResult result = null;
                unitSummaryComponent.UnitSubComponentList.ForEach(u => {
                    // 仮座標の決定
                    int positionId = this.GetPositionIdFromInRangeUnit(u.PositionId, this.UnitSubComponent.SubRange, this.UnitSubComponent.InvalidRange);
                    if (!positionId.Equals(AloneWarConst.ErrorPositionId))
                    {
                        int distance = StageManager.Instance.StageInformation.GetSenceOfDistance(positionId, this.UnitSubComponent.PositionId);
                        PredictionBattleResult predictionBattleResult = new PredictionBattleResult(this.UnitSubComponent.UnitBaseStatus, u.UnitBaseStatus, distance, positionId);
                        if (result == null)
                        {
                            result = predictionBattleResult;
                        }
                        else
                        {
                            this.CompareBatlleResult(result, predictionBattleResult);
                        }
                    }
                });

                //  結果より座標を決定
                if (result != null)
                {
                    this.TargetMovePosition = result.TempPositionId;
                    this.TargetUnitPosition = result.DefenceBattleResultInfo.UnitBaseStatus.StageStatus.PositionId;
                }
            }
        }

        /// <summary>
        /// 範囲内からユニットに有効な座標を取得
        /// </summary>
        /// <param name="unitPositionId"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        private int GetPositionIdFromInRangeUnit(int unitPositionId, int distance, int invalidRange)
        {
            while (distance > 0)
            {
                List<int> positionIdList = StageManager.Instance.StageInformation.GetDistaceAwayPositionList(unitPositionId, distance,
                    (id) => { return this.UnitSubComponent.UnitRange.MainRangeCommandList.ContainsKey(id); });
                foreach (int positionId in positionIdList)
                {
                    RangeCommand rangeCommand = this.UnitSubComponent.UnitRange.MainRangeCommandList[positionId];
                    if (!rangeCommand.IsOnUnit || !rangeCommand.IsInvalidRange)
                    {
                        return rangeCommand.MassComponent.PositionId;
                    }
                }
                distance--;

                // 範囲外
                if (distance.Equals(invalidRange))
                {
                    break;
                }
            }

            return AloneWarConst.ErrorPositionId;
        }

        #region 抽象メソッド

        /// <summary>
        /// default 
        /// override 継承先でタスクを設定する処理を実装
        /// </summary>
        public virtual void SetAITask()
        {
            this.SetTarget();
            // if move action need
            if (!this.TargetMovePosition.Equals(AloneWarConst.ErrorPositionId) && !this.TargetMovePosition.Equals(this.UnitSubComponent.PositionId))
            {
                // ルート設定&着色
                this.UnitSubComponent.UnitCommandController.UnitRoot.SetNewRoot(this.TargetMovePosition);
                StageManager.Instance.TaskQueue.Enqueue(UnityExtensions.Wait1Frame);
                StageManager.Instance.TaskQueue.Enqueue(this.UnitSubComponent.UnitCommandController.MoveTask);
                // イベントのことを考慮すること
                //this.UnitSubComponent.UnitObjectStatus.StageStatus.Wait;
            }
            // if attack action need
            if (this.TargetUnitPosition.Equals(AloneWarConst.ErrorPositionId) && !this.UnitSubComponent.UnitBaseStatus.StageStatus.Wait)
            {
                this.UnitSubComponent.UnitRange.SetRange(this.UnitSubComponent.UnitBaseStatus.SubCommand, CommandCategory.None, this.UnitSubComponent.SubRange, 0, this.UnitSubComponent.InvalidRange, 0);
                this.UnitSubComponent.UnitRange.SetRangeColor();
                StageManager.Instance.TaskQueue.Enqueue(UnityExtensions.Wait1Frame);
                // 
            }
        }

        /// <summary>
        /// 設定されているAIから移動座標を取得
        /// </summary>
        protected abstract void SearchUnitFromAI();

        /// <summary>
        ///  default 結果比較し優勢な結果を選択
        ///  override AIによる
        /// </summary>
        /// <param name="predictionBattleResult"></param>
        /// <param name="compare"></param>
        protected virtual void CompareBatlleResult(PredictionBattleResult baseResult, PredictionBattleResult compareResult)
        {
            if (baseResult.ResultExpectedValue < compareResult.ResultExpectedValue)
            {
                baseResult = compareResult;
            }
        }

        #endregion

    }
}