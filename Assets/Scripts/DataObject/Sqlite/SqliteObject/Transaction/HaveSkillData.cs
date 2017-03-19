﻿using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("HaveSkillData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class HaveSkillData : SqliteBaseData
    {
        [SqliteProperty]
        public int SkillId { get; set; }

        [SqliteProperty]
        public int UnitId { get; set; }

    }
}
