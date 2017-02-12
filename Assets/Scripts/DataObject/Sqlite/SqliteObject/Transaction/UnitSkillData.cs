﻿using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    [DataAccess("UnitSkillData", AloneWarConst.SqliteDataBaseName.Transaction)]
    public class UnitSkillData : SqliteBaseData,IForeignKey
    {
        #region SqliteProperty

        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public int SkillId { get; set; }

        #endregion

        /// <summary>
        /// 関連付け項目(SkillTableData)
        /// </summary>
        public int ForeignKey { get { return this.SkillId; } }
    }
}
