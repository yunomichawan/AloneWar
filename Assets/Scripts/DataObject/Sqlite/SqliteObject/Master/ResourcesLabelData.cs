using System;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteAttributes;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("ResourcesLabelData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class ResourcesLabelData : SqliteBaseData
    {
        [SqliteProperty]
        public string LabelId { get; set; }

        [SqliteProperty]
        public string LabelJp { get; set; }

        [SqliteProperty]
        public string LabelEn { get; set; }

        [SqliteProperty]
        public bool IsEditor { get; set; }
    }
}
