using AloneWar.Common.Component;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using System.Collections.Generic;
using AloneWar.DataObject;
using AloneWar.Common;

namespace AloneWar.Unit.Status
{
    public class UnitSubStatus : UnitBaseStatus
    {
        public UnitSubStatusData UnitSubStatusData { get; set; }

        public UnitAIStageStatusData UnitAIStageStatusData { get; set; }
    }
}
