﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Unit.Status;

namespace AloneWar.Unit.Component
{
    [Serializable]
    public abstract class UnitBaseComponent<T> : MonoBehaviour
    {
        public UnitObjectStatus<T> UnitObjectStatus { get; set; }

    }
}
