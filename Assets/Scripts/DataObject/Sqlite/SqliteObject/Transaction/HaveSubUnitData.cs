using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using System;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// ユニット基本ステータス
    /// </summary>
    [DataAccess("HaveSubUnitData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class HaveSubUnitData : SqliteBaseData, IDateData
    {
        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public bool IsFavorite { get; set; }

        [SqliteProperty]
        public DateTime CreateDate { get; set; }

        [SqliteProperty]
        public DateTime UpdateDate { get; set; }
    }
}
