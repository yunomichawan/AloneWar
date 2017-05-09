using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using System;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("SaveData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class SaveData : SqliteBaseData, IDateData
    {
        [SqliteProperty]
        public int ScenarioId { get; set; }

        /// <summary>
        /// 必要？
        /// </summary>
        [SqliteProperty]
        public int MainUnitId { get; set; }

        [SqliteProperty]
        public DateTime CreateDate { get; set; }

        [SqliteProperty]
        public DateTime UpdateDate { get; set; }
    }
}
