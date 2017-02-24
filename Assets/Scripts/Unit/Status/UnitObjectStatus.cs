﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using UnityEngine;
using AloneWar.Common;

namespace AloneWar.Unit.Status
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class UnitObjectStatus<T> : UnitBaseStatus where T : SqliteBaseData
    {
        public T UnitStatus { get; set; }
    }
}
