using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// ユニット基本ステータス
    /// </summary>
    [DataAccess("HaveSubUnitData", AloneWarConst.SqliteDataBaseName.Transaction)]
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
