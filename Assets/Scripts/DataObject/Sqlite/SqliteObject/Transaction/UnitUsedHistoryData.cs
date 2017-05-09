using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// ユニット使用履歴
    /// </summary>
    [DataAccess("UnitUsedHistoryData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class UnitUsedHistoryData : SqliteBaseData
    {
        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int Count { get; set; }

    }
}
