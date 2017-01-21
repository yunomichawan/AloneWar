using System;

namespace AloneWar.DataObject.Sqlite.SqliteAttributes
{
    /// <summary>
    /// NULL許可否属性
    /// </summary>
    public class NullAbleAttribute : SqlitePropertyAttribute
    {
        public override bool NullAble { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="nullAble"></param>
        public NullAbleAttribute(bool nullAble = true)
        {
            this.NullAble = nullAble;
        }
    }
}
