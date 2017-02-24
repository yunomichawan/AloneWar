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
        /// <summary>
        /// 存在Y/N
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.UnitMainComponent == null && this.UnitSubComponentList.Count.Equals(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public UnitMainComponent UnitMainComponent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<UnitSubComponent> UnitSubComponentList { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnitSummaryComponent()
        {
            this.UnitSubComponentList = new List<UnitSubComponent>();
        }
    }
}