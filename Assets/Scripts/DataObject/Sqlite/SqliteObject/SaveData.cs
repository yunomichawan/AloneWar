using System;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject
{
    [DataAccess("SaveData")]
    public class SaveData : SqliteBaseData,IDateData
    {
        // 進行
        
        [SqliteProperty]
        public DateTime StartDate { get; set; }

        [SqliteProperty]
        public DateTime EndDate { get; set; }
    }
}
