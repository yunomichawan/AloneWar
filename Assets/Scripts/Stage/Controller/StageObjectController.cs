using AloneWar.Stage.Event.EventObject;
using AloneWar.Unit.Component;
using System.Collections.Generic;
using UnityEngine;

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
        public List<StageEventHandler> MoveEvent { get { return this.moveEvent; } set { this.moveEvent = value; } }
        private List<StageEventHandler> moveEvent = new List<StageEventHandler>();

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
                if (m.StageTriggerSender.IsValidTrigger(positionId))
                {
                    m.EnqueueEventTask();
                }
            });
        }
    }
}
