using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("StageClearTriggerData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class StageClearTriggerData : SqliteBaseData
    {
        [SqliteProperty]
        public int StageId { get; set; }

        [SqliteProperty]
        public int TriggerId { get; set; }
    }
}
