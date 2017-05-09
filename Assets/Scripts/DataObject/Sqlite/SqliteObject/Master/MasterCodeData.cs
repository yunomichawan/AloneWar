using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// マスター(コード類の管理)
    /// </summary>
    [DataAccess("MasterCodeData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class MasterCodeData : SqliteBaseData, IDefaultSort
    {
        [SqliteProperty]
        public string Category { get; set; }

        [SqliteProperty]
        public int CodeId { get; set; }

        [SqliteProperty]
        public string Name { get; set; }

        [SqliteProperty]
        public int SortNo { get; set; }

    }
}
