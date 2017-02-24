using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Stage.Component;
using AloneWar.Stage.FieldObject;
using AloneWar.Unit.Component;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AloneWar.Stage.Controller.Unit
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitRange
    {
        #region property

        private UnitBaseComponent UnitBaseComponent { get; set; }

        private int MainRange { get; set; }

        private int SubRange { get; set; }

        private int InvalidMainRange { get; set; }

        private int InvalidSubRange { get; set; }

        private CommandCategory MainCommandCategory { get; set; }

        private CommandCategory SubCommandCategory { get; set; }

        /// <summary>
        /// key is positionId
        /// </summary>
        public Dictionary<int, RangeCommand> MainRangeCommandList { get { return this.mainCommandList; } set { this.mainCommandList = value; } }
        private Dictionary<int, RangeCommand> mainCommandList = new Dictionary<int, RangeCommand>();

        /// <summary>
        /// key is positionId
        /// </summary>
        public Dictionary<int, RangeCommand> SubRangeCommandList { get { return this.subCommandList; } set { this.subCommandList = value; } }
        private Dictionary<int, RangeCommand> subCommandList = new Dictionary<int, RangeCommand>();

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unitBaseStatus"></param>
        /// <param name="monoBehaviour"></param>
        public UnitRange(UnitBaseComponent unitBaseComponent)
        {
            this.UnitBaseComponent = unitBaseComponent;
            this.InitRange();
        }

        #region Range

        /// <summary>
        /// 空初期化
        /// </summary>
        public void InitRange()
        {
            this.MainRange = 0;
            this.SubRange = 0;
            this.InvalidMainRange = 0;
            this.InvalidSubRange = 0;
            this.MainCommandCategory = CommandCategory.None;
            this.SubCommandCategory = CommandCategory.None;
            this.MainRangeCommandList.Clear();
            this.SubRangeCommandList.Clear();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void InitRange(CommandCategory mainCommand, CommandCategory subCommand, int mainRange, int subRange, int invalidMainRange, int invalidSubRange)
        {
            this.MainRange = mainRange;
            this.SubRange = subRange;
            this.InvalidMainRange = invalidMainRange;
            this.InvalidSubRange = invalidSubRange;
            this.MainCommandCategory = mainCommand;
            this.SubCommandCategory = subCommand;
            this.MainRangeCommandList.Clear();
            this.SubRangeCommandList.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainCommand"></param>
        /// <param name="subCommand"></param>
        /// <param name="mainRange"></param>
        /// <param name="subRange"></param>
        /// <param name="invalidMainRange"></param>
        /// <param name="invalidSubRange"></param>
        public void SetRange(CommandCategory mainCommand, CommandCategory subCommand, int mainRange, int subRange, int invalidMainRange = 0, int invalidSubRange = 0)
        {
            this.InitRange(mainCommand, subCommand, mainRange, subRange, invalidMainRange, invalidSubRange);
            int positionId = this.UnitBaseComponent.UnitBaseStatus.StageStatus.PositionId;

            RangeCommand massRange = new RangeCommand(this.UnitBaseComponent.UnitBaseStatus.StageStatus.UnitSide, positionId, this.MainRange, this.InvalidMainRange);
            this.MainRangeCommandList.Add(massRange.MassComponent.PositionId, massRange);
            this.SetRangeLoop(new List<RangeCommand>(new RangeCommand[] { massRange }), this.MainRangeCommandList, this.SubRangeCommandList);
            this.SetRangeLoop(this.MainRangeCommandList.ToList().ConvertAll<RangeCommand>(r => r.Value), this.SubRangeCommandList, new Dictionary<int, RangeCommand>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rangeCommandList0"></param>
        /// <param name="rangeCommandList1"></param>
        private void SetRangeLoop(List<RangeCommand> rangeCommandLoop, Dictionary<int, RangeCommand> rangeCommandList0, Dictionary<int, RangeCommand> rangeCommandList1)
        {
            // ループ用リスト
            List<RangeCommand> rangeCommandNext = new List<RangeCommand>();
            int w = StageManager.Instance.StageInformation.StageData.Width;
            rangeCommandLoop.ForEach(r =>
            {
                MassStatus massStatus = r.MassComponent.MassStatus;
                // top
                RangeCommand upRange = new RangeCommand(r, massStatus.PositionId - w, RangeDirection.Top);
                if (this.AddRangeIfValid(upRange, rangeCommandList0, rangeCommandList1))
                {
                    rangeCommandNext.Add(upRange);
                }
                // bottom
                RangeCommand bottomRange = new RangeCommand(r, massStatus.PositionId + w, RangeDirection.Bottom);
                if (this.AddRangeIfValid(bottomRange, rangeCommandList0, rangeCommandList1))
                {
                    rangeCommandNext.Add(bottomRange);
                }
                // right
                RangeCommand rightRange = new RangeCommand(r, massStatus.PositionId + 1, RangeDirection.Right);
                if (this.AddRangeIfValid(rightRange, rangeCommandList0, rangeCommandList1))
                {
                    rangeCommandNext.Add(rightRange);
                }
                // left
                RangeCommand leftRange = new RangeCommand(r, massStatus.PositionId - 1, RangeDirection.Left);
                if (this.AddRangeIfValid(rightRange, rangeCommandList0, rangeCommandList1))
                {
                    rangeCommandNext.Add(leftRange);
                }
            });
            // 再帰処理
            this.SetRangeLoop(rangeCommandNext, rangeCommandList0, rangeCommandList1);
        }

        /// <summary>
        /// 対象マスを0範囲に追加
        /// 有効でない場合は1範囲に追加
        /// </summary>
        /// <param name="rangeCommand"></param>
        /// <param name="rangeCommandList0"></param>
        /// <param name="rangeCommandList1"></param>
        private bool AddRangeIfValid(RangeCommand rangeCommand, Dictionary<int, RangeCommand> rangeCommandList0, Dictionary<int, RangeCommand> rangeCommandList1)
        {
            if (!rangeCommandList0.ContainsKey(rangeCommand.MassComponent.PositionId) && !rangeCommandList1.ContainsKey(rangeCommand.MassComponent.PositionId))
            {
                if (rangeCommand.IsValidRange && rangeCommand.IsValidRangeUnit)
                {
                    rangeCommandList0.Add(rangeCommand.MassComponent.PositionId, rangeCommand);
                    return true;
                }
                else
                {
                    RangeCommand subRange = null;
                    if (rangeCommand.IsOnUnit)
                    {
                        // 1つ前の座標取得
                        int beforePositionId = rangeCommand.MassComponent.PositionId + -1 * StageManager.Instance.StageInformation.GetDirectionPositionParam(rangeCommand.DirectionList.Last());
                        RangeCommand before = rangeCommandList0[beforePositionId];
                        subRange = new RangeCommand(before, this.SubCommandCategory, this.SubRange, this.InvalidSubRange);
                        rangeCommandList0.Remove(beforePositionId);
                    }
                    else
                    {
                        subRange = new RangeCommand(rangeCommand, this.SubCommandCategory, this.SubRange, this.InvalidSubRange);
                    }
                    rangeCommandList1.Add(subRange.MassComponent.PositionId, subRange);
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetRangeColor()
        {
            this.MainRangeCommandList.ToList().ForEach(pair =>
            {
                RangeCommand rangeCommand = this.MainRangeCommandList[pair.Key];
                MassComponent massComponent = rangeCommand.MassComponent;
                //rangeCommand.CommandCategory より色を決定
                massComponent.GetComponent<SpriteRenderer>().color = new UnityEngine.Color();
            });

            this.SubRangeCommandList.ToList().ForEach(pair =>
            {
                RangeCommand rangeCommand = this.SubRangeCommandList[pair.Key];
                MassComponent massComponent = rangeCommand.MassComponent;
                //rangeCommand.CommandCategory より色を決定
                massComponent.GetComponent<SpriteRenderer>().color = new UnityEngine.Color();
            });
        }

        /// <summary>
        /// 対象座標を範囲に含んでいた場合、範囲を再設定する
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public void ResetRange(int positionId)
        {
            if (this.MainRangeCommandList.ContainsKey(positionId))
            {
                this.SetRange(this.UnitBaseComponent.UnitBaseStatus.MainCommand, this.UnitBaseComponent.UnitBaseStatus.SubCommand, this.UnitBaseComponent.MainRange, this.UnitBaseComponent.SubRange, 0, this.UnitBaseComponent.InvalidRange);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UnitSummaryComponent SearchRange(UnitSideCategory unitSideCategory)
        {
            UnitSummaryComponent unitSummaryComponent = new UnitSummaryComponent();
            List<int> mainKeys = this.MainRangeCommandList.Keys.ToList();
            List<int> subKeys = this.SubRangeCommandList.Keys.ToList();
            mainKeys.AddRange(subKeys);

            return StageManager.Instance.StageInformation.SearchUnitComponent(mainKeys, unitSideCategory);
        }
    }
}
