﻿using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Base
{
    /// <summary>
    /// Sqliteテーブルの基本クラス
    /// </summary>
    [SqliteProperty]
    public abstract class SqliteBaseData
    {
        #region sqlite propety

        [PrimaryKey]
        public int Id { get; set; }

        #endregion
        
        #region member

        public DbObjectStatus dbObjectStatus = DbObjectStatus.Update;

        #endregion
    }
}
