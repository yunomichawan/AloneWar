﻿using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject;
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

        public override EventTriggerCategory[] ValidEventTrigger { get { return this.validEventTrigger; } }

        public EventTriggerCategory[] validEventTrigger;

        public override IEnumerator EventTask()
        {

            base.EventTask();
            yield return null;
        }
    }
}
