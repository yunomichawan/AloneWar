using AloneWar.Stage.Event;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Stage.FieldObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AloneWar.Common;

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

        public override GameObjectCategory GameObjectCategory
        {
            get
            {
                return GameObjectCategory.Mass;
            }
        }
        
        public MassStatus MassStatus { get; set; }

        public List<PositionEvent> CloseEventList { get { return this.closeEventList; } set { this.closeEventList = value; } }
        private List<PositionEvent> closeEventList = new List<PositionEvent>();

        public bool IsClose
        {
            get
            {
                return this.MassStatus.IsClose;
            }
            set
            {
                this.MassStatus.IsClose = value;
                if (this.MassStatus.IsClose)
                {
                    this.CloseEventList.ForEach(m =>
                    {
                        if (m.ValidFlg)
                        {
                            m.EnqueueEventTask();
                        }
                    });
                }
            }
        }
    }
}
