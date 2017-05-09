using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// 中断ステージ
    /// </summary>
    [DataAccess("StageContinueData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class StageContinueData : SqliteBaseData
    {
        [SqliteProperty]
        public int StageId { get; set; }

    }
}
