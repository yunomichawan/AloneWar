using System;

namespace AloneWar.DataObject.Sqlite.SqliteAttributes
{
    /// <summary>
    /// Sqlite基本属性クラス
    /// </summary>
    public class SqlitePropertyAttribute : Attribute
    {
        /// <summary>
        /// プライマリキーフラグ
        /// </summary>
        public virtual bool IsPrimaryKey { get; set; }

        /// <summary>
        /// NULL許可否
        /// </summary>
        public virtual bool NullAble { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SqlitePropertyAttribute()
        {
            this.IsPrimaryKey = false;
            this.NullAble = false;
        }

    }
}
