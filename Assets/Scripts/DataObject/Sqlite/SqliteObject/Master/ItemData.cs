using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// アイテム管理テーブル
    /// </summary>
    [DataAccess("ItemData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class ItemData : SqliteBaseData, IDefaultSort
    {
        [SqliteProperty]
        public string Name { get; set; }

        [SqliteProperty]
        public int ItemCategory { get; set; }

        [SqliteProperty]
        public string SpritePath { get; set; }

        [SqliteProperty]
        public int SortNo { get; set; }

        [SqliteProperty]
        public int StatusGrantedParamId { get; set; }
    }
}
