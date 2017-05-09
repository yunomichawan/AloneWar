using AloneWar.Common;
using AloneWar.Common.TaskHelper;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Stage.Event.EventSender;
using AloneWar.Stage.Event.TriggerSender;
using System.Collections;
using UnityEngine;

namespace AloneWar.Stage
{
    public class StageEventBuilder : TaskCoroutineBeahavior
    {
        /// <summary>
        /// 
        /// </summary>
        public static StageEventBuilder Instance
        {
            get
            {
                return StageManager.Instance.stageEventBuilder;
            }
        }

        public IEnumerator EventDataTaskRun(EventData eventTableData)
        {
            EventCategory eventCategory = (EventCategory)eventTableData.EventCategory;
            switch (eventCategory)
            {
                case EventCategory.GetUnit:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
                case EventCategory.GetItem:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
                case EventCategory.GetSkill:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
                case EventCategory.AiChange:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
                case EventCategory.SideChange:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
                case EventCategory.UnitDamage:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
                case EventCategory.UnitKill:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
                case EventCategory.UnitSummon:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
                case EventCategory.PositionClose:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
                case EventCategory.Talk:
                    Debug.Log("call event:" + eventCategory.ToString());
                    break;
            }

            yield return null;
        }

        /// <summary>
        /// イベントハンドラーを作成
        /// </summary>
        /// <param name="stageEventInformation"></param>
        /// <returns></returns>
        public StageEventHandler CreateEventHandler(StageEventInformation stageEventInformation)
        {
            StageTriggerSender stageTriggerSender = this.GetTriggerSender(stageEventInformation.EventTriggerData.Category);
            StageEventSender stageEventSender = this.GetEventSender(stageEventInformation.EventData.Category);

            return new StageEventHandler(stageEventInformation, stageEventSender, stageTriggerSender);
        }

        /// <summary>
        /// イベント引数を作成
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public StageEventSender GetEventSender(EventCategory category)
        {
            switch (category)
            {
                case EventCategory.AiChange:
                    return new UnitAISender();
            }

            return null;
        }

        /// <summary>
        /// トリガー引数を作成
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public StageTriggerSender GetTriggerSender(EventTriggerCategory category)
        {
            switch (category)
            {
                case EventTriggerCategory.PositionClose:
                    return new PositionCloseTrigger();
                case EventTriggerCategory.AreaStop:
                    return null;
            }
            return null;
        }
    }
}
