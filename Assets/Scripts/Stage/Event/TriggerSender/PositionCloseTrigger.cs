using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.Stage.Component;
using AloneWar.Stage.Event.EventObject;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AloneWar.Stage.Event.TriggerSender
{
    public class PositionCloseTrigger : StageTriggerSender
    {
        PositionTriggerSender TriggerSender { get; set; }

        public PositionCloseTrigger()
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

            foreach (int positionId in this.TriggerSender.PositionIdArray)
            {
                MassComponent mass = StageManager.Instance.StageInformation.MassComponentList[positionId];
                // イベントの持ち方を修正
                mass.StageEventHandlerList.Add(handler);
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool IsValidTrigger(object arg)
        {
            int positionId = (int)arg;
            return StageManager.Instance.StageInformation.MassComponentList[positionId].IsClose.Equals(true);
        }

        #endregion

    }
}
