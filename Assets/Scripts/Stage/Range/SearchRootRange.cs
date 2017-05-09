using AloneWar.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AloneWar.Stage.Range
{
    /// <summary>
    /// 範囲外のオブジェクトに対してルート検索を行う
    /// </summary>
    public class SearchRootRange
    {
        #region property

        private Dictionary<int, RangeCommand> SettingRangeList { get; set; }

        private Dictionary<int, List<RangeCommand>> DistanceRangeCommandList { get; set; }

        private int FromPositionId { get; set; }

        private int ToPositionId { get; set; }

        private int Move { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="rangeCommand">起点</param>
        /// <param name="toPositionId"></param>
        public SearchRootRange(RangeCommand rangeCommand, Dictionary<int, RangeCommand> settingRangeList, int toPositionId)
        {
            this.FromPositionId = rangeCommand.MassComponent.PositionId;
            this.Move = rangeCommand.MasterRange;
            rangeCommand.Range = 1000;
            this.ToPositionId = toPositionId;
            this.SettingRangeList = settingRangeList;
            this.DistanceRangeCommandList = new Dictionary<int, List<RangeCommand>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SearchNearRootPositionId()
        {
            int count = 0;
            // 到達するまでループ <-- 無限ループに至る可能性あり
            while (this.DistanceRangeCommandList.ContainsKey(0))
            {
                this.SetNearRangeList();
                count++;
                if (count > AloneWarConst.SearchRootMaxDistance)
                {
                    return AloneWarConst.ErrorPositionId;
                }
            }

            return this.GetRootFromRangeCommand();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetNearRangeList()
        {
            List<int> keyList = this.DistanceRangeCommandList.Keys.OrderBy(k => k).ToList();
            foreach (int key in keyList)
            {
                bool addFlg = false;
                List<RangeCommand> rangeCommandList = this.DistanceRangeCommandList[key];
                rangeCommandList.ForEach(r =>
                {
                    if (!r.IsSearchCompleted)
                    {
                        r.SetRoundRange(range =>
                        {
                            if (this.AddDistanceRangeIfValid(range))
                            {
                                range.IsSearchCompleted = true;
                                addFlg = true;
                            }
                        });
                    }
                });
                // 追加されたら抜ける
                if (addFlg)
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rangeCommand"></param>
        private bool AddDistanceRangeIfValid(RangeCommand rangeCommand)
        {
            // 判定処理
            if (rangeCommand.IsValidSettingRange && rangeCommand.IsValidRangeThrough)
            {
                int distance = StageManager.Instance.StageInformation.GetSenceOfDistance(this.ToPositionId, rangeCommand.MassComponent.PositionId);
                if (this.DistanceRangeCommandList.ContainsKey(distance))
                {
                    // 同じ座標が存在するか、もしくはゼロ距離
                    if (!this.DistanceRangeCommandList[distance].Any(r => r.MassComponent.PositionId.Equals(rangeCommand.MassComponent.PositionId)) || distance.Equals(0))
                    {
                        this.DistanceRangeCommandList[distance].Add(rangeCommand);
                    }
                }
                else
                {
                    this.DistanceRangeCommandList.Add(distance, new List<RangeCommand>(new RangeCommand[] { rangeCommand }));
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rangeCommand"></param>
        /// <returns></returns>
        private int GetRootFromRangeCommand()
        {
            int positionId = this.FromPositionId;
            // 候補
            List<RangeCommand> candidateRangeCommand = new List<RangeCommand>();
            List<RangeCommand> rangeCommandList = this.DistanceRangeCommandList[0];
            // 候補座標を回収
            foreach(RangeCommand rangeCommand in rangeCommandList)
            {
                List<RangeDirection> directionList = rangeCommand.DirectionList.GetRange(0, this.Move);
                directionList.ForEach(d => positionId = StageManager.Instance.StageInformation.GetDirectionPositionId(positionId, d));
                RangeCommand moveRange;
                if (this.SettingRangeList.TryGetValue(positionId, out moveRange))
                {
                    if (moveRange.IsValidMove)
                    {
                        return moveRange.MassComponent.PositionId;
                    }
                    candidateRangeCommand.Add(moveRange);
                }
            }
            #region memo
            // 求めた座標が設定不可の場合
            // 設定したルートを遠い順から辿る
            /* 1.設定する基準となる座標を取得
             * 
             *1 |_ _|_ _|_ _|_ _|_ _|_ _|_ _|_ _|
             *2 |_ _|_7_|_ _|_ _|_ _|_ _|_ _|_ _|
             *3 |_7_|_e_|_5_|_ _|_ _|_ _|_ _|_ _|
             *4 |_6_|_5_|_4_|_5_|_ _|_ _|_ _|_ _|
             *5 |_ _|_4_|_3_|_$_|_5_|_ _|_ _|_ _|
             *6 |_*_|_*_|_*_|_3_|_4_|_ _|_ _|_ _|
             *7 |_ _|_2_|_1_|_2_|_3_|_ _|_ _|_ _|
             *8 |_ _|_ _|_p_|_1_|_2_|_ _|_ _|_ _|
             *9 |_ _|_ _|_ _|_2_|_ _|_ _|_ _|_ _|
             */
            #endregion
            return this.GetPositionIdFromCandidate(candidateRangeCommand);
        }

        /// <summary>
        /// 候補の中から移動座標を決定
        /// </summary>
        /// <param name="candidatePositionList">移動候補対象</param>
        /// <returns></returns>
        private int GetPositionIdFromCandidate(List<RangeCommand> candidateRangeCommand)
        {
            List<RangeCommand> nextLoopRangeCommand = new List<RangeCommand>();
            // 候補に近い且つ、設定可能座標を取得
            foreach (RangeCommand rangeCommand in candidateRangeCommand)
            {
                int positionId = rangeCommand.MassComponent.PositionId;
                foreach (RangeDirection rangeDirection in Enum.GetValues(typeof(RangeDirection)))
                {
                    RangeCommand moveRange;
                    int candidateId = StageManager.Instance.StageInformation.GetDirectionPositionId(positionId, rangeDirection);
                    if (this.TryGetMoveValidRange(candidateId, out moveRange, nextLoopRangeCommand))
                    {
                        return moveRange.MassComponent.PositionId;
                    }
                }
            }
            // 再帰
            return this.GetPositionIdFromCandidate(nextLoopRangeCommand);
        }

        /// <summary>
        /// 座標が有効かどうか。無効の場合リストに追加
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="rangeCommand"></param>
        /// <param name="rangeCommandList"></param>
        /// <returns></returns>
        private bool TryGetMoveValidRange(int positionId, out RangeCommand rangeCommand, List<RangeCommand> rangeCommandList)
        {
            if (this.SettingRangeList.TryGetValue(positionId, out rangeCommand))
            {
                return true;
            }
            rangeCommandList.Add(rangeCommand);
            return false;
        }
    }
}
