using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// ステージ管理テーブル
    /// </summary>
    [DataAccess("StageData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class StageData : SqliteBaseData, IDefaultSort
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

        /// <summary>
        /// 
        /// </summary>
        [SqliteProperty]
        public int StageClearCategory { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        [SqliteProperty]
        public string StageClearDetail { get; set; }

        [SqliteProperty]
        public int SortNo { get; set; }
    }
}