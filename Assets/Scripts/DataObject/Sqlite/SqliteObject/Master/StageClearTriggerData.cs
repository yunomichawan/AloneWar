using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

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
