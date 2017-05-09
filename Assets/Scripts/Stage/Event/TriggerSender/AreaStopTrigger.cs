using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.Stage.Component;
using AloneWar.Stage.Controller;
using AloneWar.Stage.Event.EventObject;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AloneWar.Stage.Event.TriggerSender
{
    public class AreaStopTrigger : StageTriggerSender
    {
        PositionTriggerSender TriggerSender { get; set; }

        public AreaStopTrigger()
        {

        }

        #region override

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventTriggerData"></param>
        public override void ConvertSender(EventTriggerData eventTriggerData)
        {
            this.TriggerSender = JsonUtility.FromJson<PositionTriggerSender>(eventTriggerData.TriggerSender);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public override void SetTriggerToStage(StageEventHandler handler)
        {
            StageObjectController.Instance.MoveEvent.Add(handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsValidTrigger(object arg)
        {
            int positionId = (int)arg;
            string area = StageManager.Instance.StageInformation.MassComponentList[positionId].Area;
            return this.TriggerSender.AreaArray.Contains(area);
        }

        #endregion

    }
}
