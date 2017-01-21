using System;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Base
{
    /// <summary>
    /// 日付用インターフェース
    /// </summary>
    public interface IDateData
    {
        /// <summary>
        /// 開始日時
        /// </summary>
        DateTime StartDate { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        DateTime EndDate { get; set; }
    }
}
