using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("UnitAssetData", AloneWarConst.SqliteDataBaseName.Master)]
    public class UnitAssetData : SqliteBaseData
    {
        [SqliteProperty]
        public int UnitId { get; set; }

        [SqliteProperty]
        public string PrefabId { get; set; }

        [SqliteProperty]
        public string FaceId { get; set; }

        [SqliteProperty]
        public string WholeBodyId { get; set; }
    }
}
