using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Base
{
    /// <summary>
    /// 外部キー
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IForeignKey
    {
        int ForeignKey { get; }
    }
}
