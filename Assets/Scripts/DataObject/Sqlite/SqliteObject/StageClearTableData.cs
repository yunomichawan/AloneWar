using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// ステージクリアフラグ管理テーブル
    /// </summary>
    [DataAccess("StageClearTableData")]
    public class StageClearTableData : SqliteBaseData
    {
        [SqliteProperty]
        public int StageId { get; set; }

        [SqliteProperty]
        public bool ClearFlg { get; set; }
    }
}
