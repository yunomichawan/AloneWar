using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.Unit.Status;
using AloneWar.DataObject;
using AloneWar.Stage.Event.EventObject;

namespace AloneWar.Unit.Component
{

    /// <summary>
    /// 
    /// </summary>
    public class UnitMainComponent : UnitBaseComponent
    {
        public UnitMainStatus UnitMainStatus { get; set; }

        #region override

        public override int SubRange
        {
            get
            {
                return this.UnitMainStatus.UnitMainStatusData.SummonRange;
            }
        }

        public override UnitBaseStatus UnitBaseStatus
        {
            get
            {
                return this.UnitMainStatus;
            }
        }

        #endregion
    }
}
