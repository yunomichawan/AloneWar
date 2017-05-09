using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// ステージイベント管理テーブル
    /// </summary>
    [DataAccess("StageEventTriggerData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class StageEventTriggerData : SqliteBaseData
    {

        #region SqliteProperty

        /// <summary>
        /// 
        /// </summary>
        [SqliteProperty]
        public int StageId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SqliteProperty]
        public int EventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SqliteProperty]
        public int TriggerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SqliteProperty]
        public int ValidCount { get; set; }

        #endregion

    }
}
