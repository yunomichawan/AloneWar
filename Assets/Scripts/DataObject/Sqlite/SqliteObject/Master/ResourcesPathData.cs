using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("ResourcesPathData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class ResourcesPathData : SqliteBaseData
    {
        [SqliteProperty]
        public string ResourceId { get; set; }

        [SqliteProperty]
        public int ResourceCategory { get; set; }

        [SqliteProperty]
        public string Path { get; set; }
    }
}
