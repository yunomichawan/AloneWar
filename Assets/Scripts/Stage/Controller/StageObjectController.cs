using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common.TaskHelper;
using UnityEngine;
using AloneWar.Unit.Component;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Stage.FieldObject;
using AloneWar.Stage.Component;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Unit.Status;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Common;

namespace AloneWar.Stage.Controller
{
    public class StageObjectController : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public static StageObjectController Instance
        {
            get
            {
                return StageManager.Instance.stageObjectController;
            }
        }

        /// <summary>
        /// 汎用移動イベント
        /// </summary>
        public List<PositionEvent> MoveEvent { get { return this.moveEvent; } set { this.moveEvent = value; } }
        private List<PositionEvent> moveEvent = new List<PositionEvent>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseStageObject"></param>
        /// <param name="positionId"></param>
        public void UpdateUnitPosition(UnitBaseComponent unitBaseComponent, int positionId)
        {
            unitBaseComponent.UnitBaseStatus.StageStatus.BeforePositionId = unitBaseComponent.PositionId;
            unitBaseComponent.PositionId = positionId;
            this.MoveEvent.ForEach(m =>
            {
                m.SetValidEvent(positionId);
            });
        }
    }
}
