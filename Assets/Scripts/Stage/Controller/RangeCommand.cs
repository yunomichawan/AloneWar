using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common;
using AloneWar.Stage.Component;
using AloneWar.Unit.Component;

namespace AloneWar.Stage.Controller
{
    public class RangeCommand
    {
        #region property

        public UnitSideCategory UnitSideCategory { get; set; }

        public CommandCategory CommandCategory { get; set; }

        public int Range { get; set; }

        public int InvalidRange { get; set; }

        public RangeDirection[] DirectionArray { get; set; }

        public MassComponent MassComponent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVaildRange
        {
            get
            {
                return this.Range < 0 || this.MassComponent.MassStatus.IsClose;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVaildRangeUnit
        {
            get
            {
                bool vaild = true;
                UnitSubComponent unitSubComponent;
                // Sub
                if (StageManager.Instance.StageInformation.UnitSubComponentList.TryGetValue(this.MassComponent.PositionId, out unitSubComponent))
                {
                    vaild = unitSubComponent.UnitObjectStatus.StageStatus.UnitSide.Equals(this.UnitSideCategory);
                }
                if (vaild)
                {
                    // Main
                    vaild = this.UnitSideCategory.Equals(StageManager.Instance.StageInformation.UnitMainComponent.UnitObjectStatus.StageStatus.UnitSide);
                }
                return vaild;
            }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 起点
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="move"></param>
        public RangeCommand(int positionId, int range, UnitSideCategory unitSideCategory)
        {
            this.MassComponent = StageManager.Instance.StageInformation.MassComponentList[positionId];
            this.Range = range;
            this.CommandCategory = Common.CommandCategory.None;
            this.UnitSideCategory = unitSideCategory;
        }

        /// <summary>
        ///途中 
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="baseRangeCommand"></param>
        public RangeCommand(RangeCommand baseRangeCommand, int positionId, RangeDirection direction)
        {
            this.MassComponent = StageManager.Instance.StageInformation.MassComponentList[positionId];
            this.CommandCategory = baseRangeCommand.CommandCategory;
            this.UnitSideCategory = baseRangeCommand.UnitSideCategory;
            this.SetRange(baseRangeCommand, baseRangeCommand.CommandCategory);
            
            this.DirectionArray = new RangeDirection[baseRangeCommand.DirectionArray.Length + 1];
            baseRangeCommand.DirectionArray.CopyTo(this.DirectionArray, 0);
            this.DirectionArray[baseRangeCommand.DirectionArray.Length] = direction;
        }

        /// <summary>
        ///途中(切替) 
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="baseRangeCommand"></param>
        public RangeCommand(RangeCommand baseRangeCommand, int positionId, RangeDirection direction, CommandCategory commandCategory)
        {
            this.MassComponent = StageManager.Instance.StageInformation.MassComponentList[positionId];
            this.CommandCategory = commandCategory;
            this.UnitSideCategory = baseRangeCommand.UnitSideCategory;
            this.SetRange(baseRangeCommand, commandCategory);
            
            this.DirectionArray = new RangeDirection[baseRangeCommand.DirectionArray.Length + 1];
            baseRangeCommand.DirectionArray.CopyTo(this.DirectionArray, 0);
            this.DirectionArray[baseRangeCommand.DirectionArray.Length] = direction;
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
                this.Range = rangeCommand.Range- this.MassComponent.MassStatus.Weight;
            }
            else
            {
                this.Range = rangeCommand.Range - 1;
            }
        }

    }
}
