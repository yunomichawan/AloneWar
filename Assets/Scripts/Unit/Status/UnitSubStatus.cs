using AloneWar.DataObject.Sqlite.SqliteObject.Master;

namespace AloneWar.Unit.Status
{
    public class UnitSubStatus : UnitBaseStatus
    {
        public UnitSubStatusData UnitSubStatusData { get; set; }

        public UnitAIStageStatusData UnitAIStageStatusData { get; set; }
    }
}
