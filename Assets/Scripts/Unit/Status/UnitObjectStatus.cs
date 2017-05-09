using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using System;

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
