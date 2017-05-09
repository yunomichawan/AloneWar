using AloneWar.Common;
using System;
using System.Collections;
using UnityEngine;
using AloneWar.Stage.Event.TriggerSender;
using AloneWar.Stage.Event.EventSender;

namespace AloneWar.Stage.Event.EventObject
{
    /// <summary>
    /// イベントのベースクラス
    /// </summary>
    public class StageEventHandler
    {

        #region property

        /// <summary>
        /// 有効フラグ
        /// </summary>
        public bool ValidFlg
        {
            get
            {
                return this.StageEventInformation.EventStatusData.Count < this.StageEventInformation.StageEventTriggerData.ValidCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public StageEventSender StageEventSender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StageTriggerSender StageTriggerSender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StageEventInformation StageEventInformation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private StageEventBuilder StageEventBuilder
        {
            get
            {
                return StageEventBuilder.Instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Action EventAfterCallback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Action EventBeforeCallback { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="stageEventInformation"></param>
        /// <param name="stageEventSender"></param>
        /// <param name="stageTriggerSender"></param>
        public StageEventHandler(StageEventInformation stageEventInformation, StageEventSender stageEventSender, StageTriggerSender stageTriggerSender)
        {
            this.StageEventInformation = stageEventInformation;
            this.StageEventSender = stageEventSender;
            this.StageTriggerSender = stageTriggerSender;
            // ConvertSenderはIFで実装できそうでは？引数はstringに変更。←微妙
            this.StageEventSender.ConvertSender(this.StageEventInformation.EventData);
            this.StageTriggerSender.ConvertSender(this.StageEventInformation.EventTriggerData);
        }

        /// <summary>
        /// 
        /// </summary>
        private IEnumerator EventTask()
        {
            yield return this.StageEventBuilder.StartCoroutine(this.EventDetail());
            this.StageEventInformation.EventStatusData.Count++;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EnqueueEventTask()
        {
            if (this.ValidFlg)
            {
                StageEventBuilder.Instance.TaskQueue.Enqueue(this.EventTask);
            }
            else
            {
                Debug.Log("unvalid event");
            }
        }

        /// <summary>
        /// イベントトリガーをステージに設定
        /// </summary>
        public void SetStageEventTrigger()
        {
            this.StageTriggerSender.SetTriggerToStage(this);
        }

        private IEnumerator EventDetail()
        {
            yield return this.StageEventBuilder.StartCoroutine( this.StageEventSender.InvokeEvent());
        }

    }
}
