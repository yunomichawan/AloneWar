using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// ステージ管理テーブル
    /// </summary>
    [DataAccess("StageTableData")]
    public class StageTableData : SqliteBaseData
    {
        /// <summary>
        /// 
        /// </summary>
        [SqliteProperty]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SqliteProperty]
        public int Width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SqliteProperty]
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SqliteProperty]
        public string Introduction { get; set; }

        /// <summary>
        /// 構成
        /// </summary>
        [SqliteProperty]
        public string ConstitutionJson { get; set; }
    }
}
