using AloneWar.Stage.Event;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Stage.Event.EventObject.Base;
using AloneWar.Stage.FieldObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AloneWar.Stage.Component
{
    public class MassComponent : BaseStageObject
    {
        public override int StageObjectId
        {
            get { return this.MassStatus.PositionId; }
        }

        public override int PositionId { get { return this.MassStatus.PositionId; } set { this.MassStatus.PositionId = value; } }

        public override string Area { get { return this.MassStatus.Area; } }

        public MassStatus MassStatus { get; set; }
    }
}
