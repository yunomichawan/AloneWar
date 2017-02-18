using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Unit.Status;
using AloneWar.Stage;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Stage.Component;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Stage.Event.EventObject.Base;
using AloneWar.Stage.Controller;
using AloneWar.Common;

namespace AloneWar.Unit.Component
{
    public class UnitSummaryComponent
    {
        public UnitMainComponent UnitMainComponent { get; set; }

        public List<UnitSubComponent> UnitSubComponentList { get; set; }

        public UnitSummaryComponent()
        {
            this.UnitSubComponentList = new List<UnitSubComponent>();
        }
    }
}