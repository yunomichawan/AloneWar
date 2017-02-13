using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    [DataAccess("StageHistorydData", AloneWarConst.SqliteDataBaseName.Transaction)]
    public class StageHistorydData : SqliteBaseData
    {
        [SqliteProperty]
        public int StageId { get; set; }

        [SqliteProperty]
        public int PlayCount { get; set; }

        [SqliteProperty]
        public int ClearCount { get; set; }
        
        [SqliteProperty]
        public bool ClearFlg { get; set; }
    }
}
