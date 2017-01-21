using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// ステージイベント管理テーブル
    /// </summary>
    [DataAccess("StageEventTableData")]
    public class StageEventTableData : SqliteBaseData
    {
    }
}
