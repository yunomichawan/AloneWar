using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common;
using AloneWar.Stage.Component;
using AloneWar.Unit.Component;

namespace AloneWar.Stage.Controller.Unit
{
    public class RangeCommand
    {
        #region property

        public UnitSideCategory UnitSideCategory { get; set; }

        public CommandCategory CommandCategory { get; set; }

        public int Range { get; set; }

        public int InvalidRange { get; set; }

        public List<RangeDirection> DirectionList { get; set; }

        public MassComponent MassComponent { get; set; }

        #region valid judge

        /// <summary>
        /// 範囲設定 有効/無効
        /// </summary>
        public bool IsValidRange
        {
            get
            {
                return this.Range < 0 || this.MassComponent.MassStatus.IsClose;
            }
        }

        /// <summary>
        /// 無効レンジ 有効/無効
        /// </summary>
        public bool IsInvalidRange
        {
            get
            {
                return this.Range - this.InvalidRange > 0;
            }
        }

        /// <summary>
        /// ユニット通過 可/否
        /// </summary>
        public bool IsValidRangeUnit
        {
            get
            {
                bool valid = true;
                UnitSubComponent unitSubComponent;
                // Sub
                if (StageManager.Instance.StageInformation.UnitSubComponentList.TryGetValue(this.MassComponent.PositionId, out unitSubComponent))
                {
                    valid = unitSubComponent.UnitBaseStatus.StageStatus.UnitSide.Equals(this.UnitSideCategory);
                }
                if (valid)
                {
                    // Main
                    valid = this.UnitSideCategory.Equals(StageManager.Instance.StageInformation.UnitMainComponent.UnitMainStatus.StageStatus.UnitSide);
                }
                return valid;
            }
        }

        /// <summary>
        /// ユニット存在チェック
        /// </summary>
        public bool IsOnUnit
        {
            get
            {
                bool valid = true;

                valid = StageManager.Instance.StageInformation.UnitSubComponentList.ContainsKey(this.MassComponent.PositionId);
                if (!valid)
                {
                    valid = StageManager.Instance.StageInformation.UnitMainComponent.PositionId.Equals(this.MassComponent.PositionId);
                }
                return valid;
            }
        }

        #endregion

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 起点
        /// </summary>
        /// <param name="unitSideCategory"></param>
        /// <param name="positionId"></param>
        /// <param name="range"></param>
        /// <param name="invalidRange"></param>
        public RangeCommand(UnitSideCategory unitSideCategory, int positionId, int range, int invalidRange)
        {
            this.MassComponent = StageManager.Instance.StageInformation.MassComponentList[positionId];
            this.Range = range;
            this.InvalidRange = invalidRange;
            this.CommandCategory = Common.CommandCategory.None;
            this.UnitSideCategory = unitSideCategory;
            this.DirectionList.Capacity = range;
        }

        /// <summary>
        /// 途中 
        /// </summary>
        /// <param name="baseRangeCommand"></param>
        /// <param name="nextPositionId"></param>
        /// <param name="direction"></param>
        public RangeCommand(RangeCommand baseRangeCommand, int nextPositionId, RangeDirection direction)
        {
            this.MassComponent = StageManager.Instance.StageInformation.MassComponentList[nextPositionId];
            this.CommandCategory = baseRangeCommand.CommandCategory;
            this.UnitSideCategory = baseRangeCommand.UnitSideCategory;
            this.SetRange(baseRangeCommand, baseRangeCommand.CommandCategory);
            this.DirectionList.Add(direction);
        }

        /// <summary>
        /// 途中(切替) 
        /// </summary>
        /// <param name="baseRangeCommand"></param>
        /// <param name="commandCategory"></param>
        /// <param name="range"></param>
        /// <param name="invalidRange"></param>
        public RangeCommand(RangeCommand baseRangeCommand, CommandCategory commandCategory, int range, int invalidRange)
        {
            this.MassComponent = StageManager.Instance.StageInformation.MassComponentList[baseRangeCommand.MassComponent.PositionId];
            this.Range = range - 1;
            this.InvalidRange = invalidRange;
            this.CommandCategory = commandCategory;
            this.UnitSideCategory = baseRangeCommand.UnitSideCategory;
            this.DirectionList.Add(baseRangeCommand.DirectionList.Last());

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rangeCommand"></param>
        /// <param name="commandCategory"></param>
        private void SetRange(RangeCommand rangeCommand, CommandCategory commandCategory)
        {
            if (commandCategory.Equals(CommandCategory.Move))
            {
                this.Range = rangeCommand.Range - this.MassComponent.MassStatus.Weight;
            }
            else
            {
                this.Range = rangeCommand.Range - 1;
            }
        }
    }
}
