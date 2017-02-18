using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Stage.Event.EventObject.Base;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.Unit.Status;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AloneWar.Stage.Event.EventObject
{
    public class UnitEvent : BaseStageEvent
    {
        public UnitEvent(StageEventInformation stageEventInformation, MonoBehaviour monoBehaviour)
            : base(stageEventInformation, monoBehaviour)
        {

        }

        public override EventTriggerCategory[] VaildEventTrigger { get { return this.vaildEventTrigger; } }

        public EventTriggerCategory[] vaildEventTrigger;

        public override IEnumerator EventTask()
        {

            base.EventTask();
            yield return null;
        }
    }
}
