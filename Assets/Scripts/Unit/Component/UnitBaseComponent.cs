using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Stage;
using AloneWar.Stage.Component;
using AloneWar.Stage.Controller.Unit;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Unit.Status;
using System;
using System.Collections.Generic;
using AloneWar.Common;
using AloneWar.Stage.Controller.Range;
using UnityEngine.AI;

namespace AloneWar.Unit.Component
{
    [Serializable]
    public abstract class UnitBaseComponent : BaseStageObject, IRangeHandler
    {
        #region unity member

        /// <summary>
        /// 
        /// </summary>
        public NavMeshAgent NavMeshAgent { get; set; }

        #endregion

        #region override

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
            }
        }

        public override string Area
        {
            get { return StageManager.Instance.StageInformation.GetPositionArea(this.PositionId); }
        }

        public override GameObjectCategory GameObjectCategory
        {
            get
            {
                return GameObjectCategory.Unit;
            }
        }

        #endregion

        #region controller

        /// <summary>
        /// 
        /// </summary>
        public UnitCommandController UnitCommandController { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StageRange StageRange { get; set; }

        #endregion

        #region 抽象

        /// <summary>
        /// 
        /// </summary>
        public abstract UnitBaseStatus UnitBaseStatus { get; }

        #endregion

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

        /// <summary>
        /// 
        /// </summary>
        public CommandCategory MainCommand
        {
            get
            {
                return this.UnitBaseStatus.BaseStatus.MainCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public CommandCategory SubCommand
        {
            get
            {
                return this.UnitBaseStatus.BaseStatus.SubCommand;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public UnitSideCategory UnitSide
        {
            get
            {
                return this.UnitBaseStatus.StageStatus.UnitSide;
            }
        }

        #endregion

        /// <summary>
        /// コントローラ初期化
        /// </summary>
        public void InitController()
        {
            this.UnitCommandController = new UnitCommandController(this);
            this.UnitCommandController.UnitRoot = new UnitRoot(this);
            this.StageRange = new StageRange(this);
            this.StageRange.SetRange(this.MainCommand, this.SubCommand, this.MainRange, this.SubRange, 0, this.InvalidRange);
            this.NavMeshAgent = this.gameObject.AddComponent<NavMeshAgent>();
        }
    }
}