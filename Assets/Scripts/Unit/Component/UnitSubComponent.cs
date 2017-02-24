using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.DataObject;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Unit.Status;

namespace AloneWar.Unit.Component
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitSubComponent : UnitBaseComponent
    {
        public UnitSubStatus UnitSubStatus { get; set; }

        public override UnitBaseStatus UnitBaseStatus
        {
            get
            {
                return this.UnitSubStatus;
            }
        }
    }
}
