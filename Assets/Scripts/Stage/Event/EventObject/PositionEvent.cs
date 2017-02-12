using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common;
using UnityEngine;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Stage.FieldObject;
using AloneWar.Stage.Event.EventObject.Base;
using AloneWar.Stage.Event.Sender;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;

namespace AloneWar.Stage.Event.EventObject
{
    /// <summary>
    /// 
    /// </summary>
    public class PositionEvent : BaseStageEvent
    {

        /// <summary>
        /// 実装方法は2つ
        /// ・inspector上で設定した配列を返すようにする
        /// ・ソース上で値を宣言する
        /// </summary>
        public override EventTriggerCategory[] VaildEventTrigger { get { return this.vaildEventTrigger; } }

        public EventTriggerCategory[] vaildEventTrigger;

        public PositionEventSender PositionEventSender { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="eventTableData"></param>
        /// <param name="stageEventTableData"></param>
        public PositionEvent(StageEventInformation stageEventInformation, MonoBehaviour monoBehaviour)
            : base(stageEventInformation, monoBehaviour)
        {
            
        }

        public override IEnumerator EventTask()
        {
            yield return this.MonoBehaviour.StartCoroutine(this.StageEventBuilder.EventDataTaskRun(this.StageEventInformation.EventData));
            base.EventTask();
        }
    }
}
