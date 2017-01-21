using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    /// <summary>
    /// コンフィグ
    /// </summary>
    [DataAccess("ConfigData")]
    public class ConfigData : SqliteBaseData
    {
        [SqliteProperty]
        public float Volume { get; set; }

        [SqliteProperty]
        public float GameSpeed { get; set; }

        [SqliteProperty]
        public float TextSpeed { get; set; }
    }
}
