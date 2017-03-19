using AloneWar.Unit.Component;
using AloneWar.Unit.Status;
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
using AloneWar.Stage.Controller.Range;

namespace AloneWar.Unit.UnitAI
{
    public abstract class UnitBaseAI
    {
        /// <summary>
        /// 
        /// </summary>
        public int TargetMovePosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TargetUnitPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UnitSubComponent UnitSubComponent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<PredictionBattleResult> PredictionBattleResultList { get { return this.predictionBattleResultList; } set { this.predictionBattleResultList = value; } }
        private List<PredictionBattleResult> predictionBattleResultList = new List<PredictionBattleResult>();

        /// <summary>
        /// 最大期待値
        /// </summary>
        public float MaxResultExpectedValue
        {
            get
            {
                if (this.PredictionBattleResultList.Count < 0)
                {
                    return 0f;
                }
                else
                {
                    return this.PredictionBattleResultList.First().ResultExpectedValue;
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unitSubComponent"></param>
        public UnitBaseAI(UnitSubComponent unitSubComponent)
        {
            this.UnitSubComponent = unitSubComponent;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Init()
        {
            this.TargetMovePosition = AloneWarConst.ErrorPositionId;
            this.TargetUnitPosition = AloneWarConst.ErrorPositionId;
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
                    (id) => { return this.UnitSubComponent.StageRange.MainRangeCommandList.ContainsKey(id); });
                foreach (int positionId in positionIdList)
                {
                    RangeCommand rangeCommand = this.UnitSubComponent.StageRange.MainRangeCommandList[positionId];
                    if (rangeCommand.IsValidMove)
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

        /// <summary>
        /// 対象との予測結果を取得。取得できない場合はNULL
        /// </summary>
        /// <param name="targetUnitBaseComponent"></param>
        /// <returns></returns>
        private PredictionBattleResult GetPredictionBattleResult(UnitBaseComponent targetUnitBaseComponent)
        {
            int positionId = this.GetPositionIdFromInRangeUnit(targetUnitBaseComponent.PositionId, this.UnitSubComponent.SubRange, this.UnitSubComponent.InvalidRange);
            if (!positionId.Equals(AloneWarConst.ErrorPositionId))
            {
                int distance = StageManager.Instance.StageInformation.GetSenceOfDistance(positionId, this.UnitSubComponent.PositionId);
                PredictionBattleResult predictionBattleResult = new PredictionBattleResult(this.UnitSubComponent.UnitBaseStatus, targetUnitBaseComponent.UnitBaseStatus, distance, positionId);
                return predictionBattleResult;
            }

            return null;
        }

        /// <summary>
        /// 範囲内のユニットとの予測結果を集計
        /// </summary>
        private void SearchUnitFromInRange()
        {
            UnitSummaryComponent unitSummaryComponent = this.UnitSubComponent.StageRange.SearchRange(this.UnitSubComponent.UnitSide.Reverse());
            this.PredictionBattleResultList.Clear();
            // main priority
            if (unitSummaryComponent.UnitMainComponent != null)
            {
                PredictionBattleResult predictionBattleResult = this.GetPredictionBattleResult(unitSummaryComponent.UnitMainComponent);
                if (predictionBattleResult != null)
                {
                    this.PredictionBattleResultList.Add(predictionBattleResult);
                }
            }

            if (this.TargetMovePosition.Equals(AloneWarConst.ErrorPositionId))
            {
                List<PredictionBattleResult> predictionBattleResultList = new List<PredictionBattleResult>(unitSummaryComponent.UnitSubComponentList.Count);
                unitSummaryComponent.UnitSubComponentList.ForEach(u =>
                {
                    // 予測結果を計算
                    PredictionBattleResult predictionBattleResult = this.GetPredictionBattleResult(u);
                    if (predictionBattleResult != null)
                    {
                        predictionBattleResultList.Add(predictionBattleResult);
                    }
                });

                this.PredictionBattleResultList = predictionBattleResultList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetAITask()
        {
            // if move action need
            if (!this.TargetMovePosition.Equals(AloneWarConst.ErrorPositionId) && !this.TargetMovePosition.Equals(this.UnitSubComponent.PositionId))
            {
                // ルート設定&着色
                this.UnitSubComponent.UnitCommandController.UnitRoot.SetNewRoot(this.TargetMovePosition);
                StageManager.Instance.TaskQueue.Enqueue(UnityExtensions.Wait1Frame);
                StageManager.Instance.TaskQueue.Enqueue(this.UnitSubComponent.UnitCommandController.MoveTask);
                // イベントのことを考慮すること
            }
            // if attack action need
            if (this.TargetUnitPosition.Equals(AloneWarConst.ErrorPositionId) && !this.UnitSubComponent.UnitBaseStatus.StageStatus.Wait)
            {
                this.UnitSubComponent.StageRange.SetRange(this.UnitSubComponent.SubCommand, CommandCategory.None, this.UnitSubComponent.SubRange, 0, this.UnitSubComponent.InvalidRange, 0);
                this.UnitSubComponent.StageRange.SetRangeColor();
                StageManager.Instance.TaskQueue.Enqueue(UnityExtensions.Wait1Frame);
                // 
            }
        }

        #region 仮想、抽象メソッド

        /*
         *仮想、抽象での実装を増やした場合、設定処理を各AIごとに分散させてしまう。
         *その為、仮想、抽象で実装する処理はできるだけ部分的に行う。 
         *結論：分散しすぎない実装
         */

        /// <summary>
        /// 移動、目標座標を設定する
        /// </summary>
        public virtual void SetTarget()
        {
            // 目標座標リセット
            this.Init();
            this.SearchUnitFromInRange();
            if (this.PredictionBattleResultList.Count > 0)
            {
                this.SetUnitTargetPriority();
            }
            else
            {
                this.SetMoveTarget();
            }

            this.SetAITask();
        }

        /// <summary>
        /// 設定されているAIから移動座標他を取得
        /// </summary>
        protected abstract void SetMoveTarget();

        /// <summary>
        /// 予測結果から優先順位を設定
        /// default : 期待値
        /// </summary>
        protected virtual void SetUnitTargetPriority()
        {
            this.PredictionBattleResultList.OrderByDescending(p => p.ResultExpectedValue);
        }

        #endregion

    }
}