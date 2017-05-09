using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    [DataAccess("StageHistorydData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
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
