using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Stage;
using AloneWar.Stage.Component;
using AloneWar.Stage.Controller.Unit;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Unit.Status;
using System;
using System.Collections.Generic;

namespace AloneWar.Unit.Component
{
    [Serializable]
    public abstract class UnitBaseComponent : BaseStageObject
    {
        /// <summary>
        /// ユニークユニット移動イベント
        /// </summary>
        public List<PositionEvent> UniqueMoveEvent { get { return this.uniqueMoveEvent; } set { this.uniqueMoveEvent = value; } }
        private List<PositionEvent> uniqueMoveEvent = new List<PositionEvent>();

        public override int StageObjectId
        {
            get { return this.UnitBaseStatus.StageStatus.Id; }
        }

        public override int PositionId
        {
            get
            {
                return this.UnitBaseStatus.StageStatus.PositionId;
            }
            set
            {
                this.UnitBaseStatus.StageStatus.PositionId = value;
                this.UniqueMoveEvent.ForEach(u => {
                    u.SetValidEvent(this.PositionId);
                });
            }
        }

        public override string Area
        {
            get { return StageManager.Instance.StageInformation.GetPositionArea(this.PositionId); }
        }

        #region controller

        /// <summary>
        /// 破棄された場合、Sqliteクラスのインスタンスが破棄されるのでは？
        /// </summary>
        public UnitCommandController UnitCommandController { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UnitRange UnitRange { get; set; }

        #endregion

        #region 抽象

        /// <summary>
        /// 
        /// </summary>
        public abstract UnitBaseStatus UnitBaseStatus { get; }

        #region default range & command

        /// <summary>
        /// default is Move
        /// </summary>
        public virtual int MainRange
        {
            get
            {
                return this.UnitBaseStatus.BaseStatus.Move;
            }
        }

        /// <summary>
        /// default is Range
        /// </summary>
        public virtual int SubRange
        {
            get
            {
                return this.UnitBaseStatus.BaseStatus.Range;
            }
        }

        /// <summary>
        /// default is InvalidRange
        /// </summary>
        public virtual int InvalidRange
        {
            get
            {
                return this.UnitBaseStatus.BaseStatus.InvalidRange;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// コントローラ初期化
        /// </summary>
        public void InitController()
        {
            this.UnitCommandController = new UnitCommandController(this);
            this.UnitCommandController.UnitRoot = new UnitRoot(this);
            this.UnitRange = new UnitRange(this);
            this.UnitRange.SetRange(this.UnitBaseStatus.MainCommand, this.UnitBaseStatus.SubCommand, this.MainRange, this.SubRange, 0, this.InvalidRange);
        }
    }
}