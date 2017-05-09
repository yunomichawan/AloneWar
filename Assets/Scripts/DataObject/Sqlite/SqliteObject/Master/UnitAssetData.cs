using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("UnitAssetData", AloneWarConst.SqliteDataBaseName.MasterDb)]
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
