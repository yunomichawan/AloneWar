﻿using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.Common;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// ステージユニット配置管理テーブル
    /// </summary>
    [DataAccess("UnitStageStatusData", AloneWarConst.SqliteDataBaseName.Master)]
    public class UnitStageStatusData : SqliteBaseData,IDefaultSort
    {
        #region SqliteProperty

        [SqliteProperty]
        public int StageId { get; set; }

        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int Level { get; set; }

        [SqliteProperty]
        public int AiCategory { get; set; }

        [SqliteProperty]
        public int PositionId { get; set; }

        [SqliteProperty]
        public int UnitSideCategory { get; set; }

        [SqliteProperty]
        public bool IsEvent { get; set; }

        [SqliteProperty]
        public int SortNo { get; set; }

        #endregion

        #region Property

        public UnitSideCategory UnitSide { get { return (UnitSideCategory)this.UnitSideCategory; } }

        #endregion
    }
}
