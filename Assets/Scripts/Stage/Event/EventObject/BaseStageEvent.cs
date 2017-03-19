using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.Common;
using AloneWar.Stage;
using UnityEngine;

namespace AloneWar.Stage.Event.EventObject
{
    /// <summary>
    /// イベントのベースクラス
    /// </summary>
    public abstract class BaseStageEvent
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="stageEventInformation"></param>
        /// <param name="monoBehaviour"></param>
        public BaseStageEvent(StageEventInformation stageEventInformation, MonoBehaviour monoBehaviour)
        {
            this.StageEventInformation = stageEventInformation;
            this.MonoBehaviour = monoBehaviour;
        }

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
        /// 有効トリガー
        /// </summary>
        public abstract EventTriggerCategory[] ValidEventTrigger { get; }

        /// <summary>
        /// 
        /// </summary>
        public StageEventInformation StageEventInformation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MonoBehaviour MonoBehaviour { get; set; }        

        /// <summary>
        /// 
        /// </summary>
        public StageEventBuilder StageEventBuilder
        {
            get
            {
                return StageEventBuilder.Instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IEnumerator EventTask()
        {
            this.StageEventInformation.EventStatusData.Count++;
            yield break;
        }

        /// <summary>
        /// 
        /// </summary>
        public Action EventAfterCallback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Action EventBeforeCallback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void EnqueueEventTask()
        {
            StageEventBuilder.Instance.TaskQueue.Enqueue(this.EventTask);
        }
    }
}
