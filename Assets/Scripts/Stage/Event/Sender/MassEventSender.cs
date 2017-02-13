using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Stage.Event.Sender.Base;
using AloneWar.Common;
using UnityEngine;

namespace AloneWar.Stage.Event.Sender
{
    [Serializable]
    public class MassEventSender : BaseEventSender
    {
        public string AreaArrayJson;

        public string PositionArrayJson;

        public string[] AreaArray { get; set; }

        public int[] PositionIdArray { get; set; }
    }
}
