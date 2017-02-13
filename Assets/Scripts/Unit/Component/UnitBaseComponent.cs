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
using AloneWar.Stage.Event.EventObject.Base;

namespace AloneWar.Unit.Component
{
    [Serializable]
    public abstract class UnitBaseComponent<T> : BaseStageObject
        where T : SqliteBaseData
    {
        public override int StageObjectId
        {
            get { return this.UnitObjectStatus.StageStatus.Id; }
        }

        public override int PositionId { get { return this.UnitObjectStatus.StageStatus.PositionId; } set { this.UnitObjectStatus.StageStatus.PositionId = value; } }

        public override string Area
        {
            get { return StageManager.Instance.StageInformation.GetPositionArea(this.PositionId); }
        }

        public UnitObjectStatus<T> UnitObjectStatus { get; set; }

    }
}