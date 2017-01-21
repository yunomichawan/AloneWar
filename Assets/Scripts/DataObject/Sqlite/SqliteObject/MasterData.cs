using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// マスター(コード類の管理)
    /// </summary>
    [DataAccess("MasterData")]
    public class MasterData : SqliteBaseData
    {
        [SqliteProperty]
        public string Name { get; set; }

        [SqliteProperty]
        public string Category { get; set; }

        [SqliteProperty]
        public int OrderSeq { get; set; }

    }
}
