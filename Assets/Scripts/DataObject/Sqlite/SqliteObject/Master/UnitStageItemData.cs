using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// ステージユニット用のアイテム関連付けテーブル
    /// </summary>
    [DataAccess("UnitStageItemData", AloneWarConst.SqliteDataBaseName.Master)]
    public class UnitStageItemData : SqliteBaseData, IForeignKey
    {
        #region SqliteProperty

        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int StageId { get; set; }

        [SqliteProperty]
        public int ItemId { get; set; }

        #endregion

        /// <summary>
        /// 関連付けキー
        /// </summary>
        public int ForeignKey { get { return this.ItemId; } }

    }
}
