using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Base
{
    /// <summary>
    /// デフォルトソート用のインターフェースを実装
    /// </summary>
    public interface IDefaultSort
    {
        [SqliteProperty]
        int SortNo { get; set; }
    }
}
