using AloneWar.Common.Component;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using System.Collections.Generic;
using AloneWar.DataObject;
using AloneWar.Common;

namespace AloneWar.Unit.Status
{
    public class UnitMainStatus : UnitBaseStatus
    {
        public UnitMainStatusData UnitMainStatusData { get; set; }
    }
}
