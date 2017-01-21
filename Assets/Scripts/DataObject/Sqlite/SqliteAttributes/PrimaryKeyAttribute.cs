using System;

namespace AloneWar.DataObject.Sqlite.SqliteAttributes
{
    /// <summary>
    /// プライマリキーを設定する属性
    /// </summary>
    public class PrimaryKeyAttribute : SqlitePropertyAttribute
    {
        public override bool IsPrimaryKey { get; set; }

        public PrimaryKeyAttribute()
        {
            this.IsPrimaryKey = true;
        }
    }
}
