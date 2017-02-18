using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Unit.Status;
using AloneWar.Stage;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Stage.Component;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Stage.Event.EventObject.Base;
using AloneWar.Stage.Controller;
using AloneWar.Common;

namespace AloneWar.Unit.Component
{
    [Serializable]
    public abstract class UnitBaseComponent<T> : BaseStageObject
        where T : SqliteBaseData
    {
        /// <summary>
        /// ユニークユニット移動イベント
        /// </summary>
        public List<PositionEvent> UniqueMoveEvent { get { return this.uniqueMoveEvent; } set { this.uniqueMoveEvent = value; } }
        private List<PositionEvent> uniqueMoveEvent = new List<PositionEvent>();

        public override int StageObjectId
        {
            get { return this.UnitObjectStatus.StageStatus.Id; }
        }

        public override int PositionId
        {
            get
            {
                return this.UnitObjectStatus.StageStatus.PositionId;
            }
            set
            {
                this.UnitObjectStatus.StageStatus.PositionId = value;
                this.UniqueMoveEvent.ForEach(u => {
                    u.SetVaildEvent(this.PositionId);
                });
            }
        }

        public override string Area
        {
            get { return StageManager.Instance.StageInformation.GetPositionArea(this.PositionId); }
        }

        public UnitObjectStatus<T> UnitObjectStatus { get; set; }

        #region controller

        /// <summary>
        /// 破棄された場合、Sqliteクラスのインスタンスが破棄されるのでは？
        /// </summary>
        public UnitCommandController<T> UnitCommandController { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UnitRange<T> UnitRange { get; set; }

        #endregion

        #region default range & command

        /// <summary>
        /// default is Move
        /// </summary>
        public virtual int MainRange
        {
            get
            {
                return this.UnitObjectStatus.BaseStatus.Move;
            }
        }

        /// <summary>
        /// default is Range
        /// </summary>
        public virtual int SubRange
        {
            get
            {
                return this.UnitObjectStatus.BaseStatus.Range;
            }
        }

        #endregion

    }
}