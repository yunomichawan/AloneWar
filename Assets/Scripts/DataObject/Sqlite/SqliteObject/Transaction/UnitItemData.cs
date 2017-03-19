using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    [DataAccess("UnitItemData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class UnitItemData : SqliteBaseData, IForeignKey
    {
        #region SqliteProperty

        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int ItemId { get; set; }

        #endregion

        /// <summary>
        /// 関連付けキーを返す
        /// </summary>
        public int ForeignKey { get { return this.ItemId; } }
    }
}
