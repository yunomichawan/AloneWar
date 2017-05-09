using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.Stage.Component;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AloneWar.Stage.Range
{
    public class RangeCommand
    {
        #region property

        public UnitSideCategory UnitSideCategory { get; set; }

        public CommandCategory CommandCategory { get; set; }

        public int MasterRange { get; set; }

        public int Range { get; set; }

        public int InvalidRange { get; set; }

        public List<RangeDirection> DirectionList { get; set; }

        public MassComponent MassComponent { get; set; }

        public bool IsSearchCompleted { get; set; }

        #region valid judge

        //外部クラスを利用するプロパティはメンバ変数を用意(パフォーマンスへの影響を考えて)

        /// <summary>
        /// 範囲設定 有効/無効
        /// </summary>
        public bool IsValidSettingRange
        {
            get
            {
                return this.Range <= this.MasterRange || this.MassComponent.IsClose;
            }
        }

        /// <summary>
        /// 座標に移動 有効/無効
        /// </summary>
        public bool IsValidMove
        {
            get
            {
                return !this.IsOnUnit && this.CommandCategory.Equals(CommandCategory.Move) && !this.MassComponent.IsClose;
            }
        }

        /// <summary>
        /// ユニット通過 可/否
        /// </summary>
        public bool IsValidRangeThrough
        {
            get
            {
                if (this.isValidRangeThrough == null)
                    this.isValidRangeThrough = StageManager.Instance.StageInformation.SearchUnit(this.MassComponent.PositionId, this.UnitSideCategory.Reverse()).IsEmpty;
                return (bool)this.isValidRangeThrough;
            }
        }
        private bool? isValidRangeThrough = null;

        /// <summary>
        /// ユニット存在チェック
        /// </summary>
        public bool IsOnUnit
        {
            get
            {
                if (this.isOnUnit == null)
                    this.isOnUnit = StageManager.Instance.StageInformation.SearchUnit(this.MassComponent.PositionId).IsEmpty;
                return (bool)this.isOnUnit;
            }
        }
        private bool? isOnUnit = null;

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
            this.Range = range;
            this.Init(positionId, range, invalidRange, CommandCategory.None, unitSideCategory);
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
            this.Init(nextPositionId, baseRangeCommand.MasterRange, baseRangeCommand.InvalidRange, baseRangeCommand.CommandCategory, baseRangeCommand.UnitSideCategory);
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
            this.Range = range;
            this.Init(baseRangeCommand.MassComponent.PositionId, range, invalidRange, commandCategory, baseRangeCommand.UnitSideCategory);
            this.DirectionList.Add(baseRangeCommand.DirectionList.Last());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="invalidRange"></param>
        /// <param name="commandCategory"></param>
        /// <param name="unitSideCategory"></param>
        private void Init(int positionId, int masterRange, int invalidRange, CommandCategory commandCategory, UnitSideCategory unitSideCategory)
        {
            this.MassComponent = StageManager.Instance.StageInformation.MassComponentList[positionId];
            this.MasterRange = masterRange;
            this.InvalidRange = invalidRange;
            this.CommandCategory = commandCategory;
            this.UnitSideCategory = unitSideCategory;
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
                this.Range = rangeCommand.Range + this.MassComponent.MassStatus.Weight;
            }
            else
            {
                this.Range = rangeCommand.Range + 1;
            }
        }

        /// <summary>
        /// 周りの範囲をに対して処理を行う
        /// </summary>
        /// <param name="rangeCommand"></param>
        /// <param name="callback"></param>
        public void SetRoundRange(Action<RangeCommand> callback)
        {
            int w = StageManager.Instance.StageInformation.StageData.Width;
            int positionId = this.MassComponent.MassStatus.PositionId;

            foreach (RangeDirection rangeDirection in Enum.GetValues(typeof(RangeDirection)))
            {
                int directionId = StageManager.Instance.StageInformation.GetDirectionPositionId(positionId, rangeDirection);
                RangeCommand upRange = new RangeCommand(this, directionId, rangeDirection);
                callback(upRange);
            }
            
        }

    }
}