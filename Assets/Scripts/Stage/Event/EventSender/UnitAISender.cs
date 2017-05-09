using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using System;
using System.Collections;
using UnityEngine;

namespace AloneWar.Stage.Event.EventSender
{
    public class UnitAISender : StageEventSender
    {

        UnitEventSender EventSender { get; set; }

        public override IEnumerator InvokeEvent()
        {
            // target unit ai change
            yield return null;
        }

        public override void ConvertSender(EventData eventData)
        {
            this.EventSender = JsonUtility.FromJson<UnitEventSender>(eventData.EventSender);
        }
    }
}
