﻿using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// サブユニットステータス
    /// </summary>
    [DataAccess("UnitSubStatusData", AloneWarConst.SqliteDataBaseName.Master)]
    [Serializable]
    public class UnitSubStatusData : SqliteBaseData
    {
        /// <summary>
        /// FK UnitBaseStatusData.Id
        /// </summary>
        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int Cost { get; set; }

        [SqliteProperty]
        public int AiCategory { get; set; }
    }
}
