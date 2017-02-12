using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// 中断ステージ
    /// </summary>
    [DataAccess("StageContinueData", AloneWarConst.SqliteDataBaseName.Transaction)]
    public class StageContinueData : SqliteBaseData
    {
        [SqliteProperty]
        public int StageId { get; set; }

    }
}
