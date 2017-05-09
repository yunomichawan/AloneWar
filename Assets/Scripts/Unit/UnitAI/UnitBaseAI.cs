using AloneWar.Battle;
using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.Stage;
using AloneWar.Stage.Range;
using AloneWar.Unit.Component;
using System.Collections.Generic;
using System.Linq;

namespace AloneWar.Unit.UnitAI
{
    public abstract class UnitBaseAI
    {
        #region property

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

        #endregion

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
        protected void Init()
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
        /// 範囲内のユニットとの予測結果を集計
        /// </summary>
        private void SearchUnitFromInRange()
        {
            UnitSummaryComponent unitSummaryComponent = this.UnitSubComponent.StageRange.SearchUnitFromInRange(this.UnitSubComponent.UnitSide.Reverse());
            this.PredictionBattleResultList = this.AggregatePredictionBattleResult(unitSummaryComponent);
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
        /// 予測結果を集計
        /// </summary>
        /// <param name="unitSummaryComponent"></param>
        protected List<PredictionBattleResult> AggregatePredictionBattleResult(UnitSummaryComponent unitSummaryComponent)
        {
            List<PredictionBattleResult> predictionBattleResultList = new List<PredictionBattleResult>();

            // main priority
            if (unitSummaryComponent.UnitMainComponent != null)
            {
                PredictionBattleResult predictionBattleResult = this.GetPredictionBattleResult(unitSummaryComponent.UnitMainComponent);
                if (predictionBattleResult != null)
                {
                    predictionBattleResultList.Add(predictionBattleResult);
                }
            }
            List<PredictionBattleResult> subPredictionBattleResultList = new List<PredictionBattleResult>(unitSummaryComponent.UnitSubComponentList.Count);
            unitSummaryComponent.UnitSubComponentList.ForEach(u =>
            {
                // 予測結果を計算
                PredictionBattleResult predictionBattleResult = this.GetPredictionBattleResult(u);
                if (predictionBattleResult != null)
                {
                    subPredictionBattleResultList.Add(predictionBattleResult);
                }
            });

            predictionBattleResultList = subPredictionBattleResultList;

            return predictionBattleResultList;
        }

        #region Search Out Range

        /// <summary>
        /// 範囲外のユニットとの予測結果を集計
        /// </summary>
        protected void SearchUnitFromSearchArea()
        {
            string[] searchArea = this.UnitSubComponent.UnitSubStatus.UnitAIStageStatusData.SearchAreaArray;
            UnitSummaryComponent unitSummaryComponent = StageManager.Instance.StageInformation.SearchUnit(u => searchArea.Contains(u.Area) && u.UnitSide.Equals(this.UnitSubComponent.UnitSide.Reverse()));
            
            if (!unitSummaryComponent.IsEmpty)
            {
                this.PredictionBattleResultList = this.AggregatePredictionBattleResult(unitSummaryComponent);
                this.SetUnitTargetPriority();
                this.SetRootPosition(this.PredictionBattleResultList.First().TempPositionId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitStageStatusId"></param>
        protected void SeachUnitFromTarget(int unitStageStatusId)
        {
            UnitSummaryComponent unitSummaryComponent = StageManager.Instance.StageInformation.SearchUnitStage(unitStageStatusId);
            if (!unitSummaryComponent.IsEmpty)
            {
                this.PredictionBattleResultList = this.AggregatePredictionBattleResult(unitSummaryComponent);
                this.SetRootPosition(this.PredictionBattleResultList.First().TempPositionId);
            }
        }

        /// <summary>
        /// 対象座標までの経由座標を設定
        /// </summary>
        /// <param name="positionId"></param>
        private void SetRootPosition(int positionId)
        {
            RangeCommand rangeCommand;
            if (this.UnitSubComponent.StageRange.MainRangeCommandList.TryGetValue(this.UnitSubComponent.PositionId, out rangeCommand))
            {
                SearchRootRange searchRootRange = new SearchRootRange(rangeCommand, this.UnitSubComponent.StageRange.MainRangeCommandList, positionId);
                this.TargetMovePosition = searchRootRange.SearchNearRootPositionId();
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void SetAITask()
        {
            this.ResetAITarget();
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
            if (!this.TargetUnitPosition.Equals(AloneWarConst.ErrorPositionId) && !this.UnitSubComponent.UnitBaseStatus.StageStatus.Wait)
            {
                this.UnitSubComponent.StageRange.SetRange(this.UnitSubComponent.SubCommand, CommandCategory.None, this.UnitSubComponent.SubRange, 0, this.UnitSubComponent.InvalidRange, 0);
                this.UnitSubComponent.StageRange.SetRangeColor(false);
                StageManager.Instance.TaskQueue.Enqueue(UnityExtensions.Wait1Frame);
                // 
                EditorDebug.DebugAlert("AI battle");
                StageManager.Instance.TaskQueue.Enqueue(this.UnitSubComponent.UnitCommandController.WaitTask);
            }
            else
            {
                StageManager.Instance.TaskQueue.Enqueue(this.UnitSubComponent.UnitCommandController.WaitTask);
            }
        }

        /// <summary>
        /// 優先対象が存在しなかった場合、再度対象を設定する
        /// </summary>
        private void ResetAITarget()
        {
            PredictionBattleResult result = this.PredictionBattleResultList.FirstOrDefault();
            if (result != null)
            {
                // 優先度の高い対象の存在チェック
                if (StageManager.Instance.StageInformation.SearchUnit(result.TempPositionId, this.UnitSubComponent.UnitSide.Reverse()).IsEmpty)
                {
                    this.SetTarget();
                }
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
        /// デフォルトの範囲設定では無い場合(待機AI等)にオーバーライドする必要がありそう
        /// </summary>
        public virtual void SetTarget()
        {
            // 目標座標リセット
            this.Init();
            this.SearchUnitFromInRange();
            // 範囲内に存在した場合
            if (this.PredictionBattleResultList.Count > 0)
            {
                this.SetUnitTargetPriority();
            }
            else
            {
                this.SetMoveTarget();
            }
        }

        /// <summary>
        /// 設定されているAIから移動座標を取得
        /// </summary>
        protected virtual void SetMoveTarget()
        {
            this.TargetUnitPosition = AloneWarConst.ErrorPositionId;
            this.SearchUnitFromSearchArea();
        }

        /// <summary>
        /// 予測結果から優先順位を設定
        /// default : 期待値
        /// </summary>
        protected virtual void SetUnitTargetPriority()
        {
            this.PredictionBattleResultList = this.PredictionBattleResultList.OrderByDescending(p => p.ResultExpectedValue).ToList();
        }

        #endregion

    }
}