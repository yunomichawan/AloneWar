using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("HaveItemData", AloneWarConst.SqliteDataBaseName.Transaction)]
    public class HaveItemData : SqliteBaseData
    {
        [SqliteProperty]
        public int ItemId { get; set; }

        [SqliteProperty]
        public bool IsFavorite { get; set; }

    }
}
