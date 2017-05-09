using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("HaveItemData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class HaveItemData : SqliteBaseData
    {
        [SqliteProperty]
        public int ItemId { get; set; }

        [SqliteProperty]
        public bool IsFavorite { get; set; }

    }
}
