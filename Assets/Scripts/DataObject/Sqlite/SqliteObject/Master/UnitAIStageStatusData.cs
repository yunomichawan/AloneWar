using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Master
{
    /// <summary>
    /// 
    /// </summary>
    [DataAccess("UnitAIStageStatusData", AloneWarConst.SqliteDataBaseName.MasterDb)]
    public class UnitAIStageStatusData : SqliteBaseData
    {
        #region SqliteProperty

        [SqliteProperty]
        public int UnitStageId { get; set; }

        [SqliteProperty]
        public int Level { get; set; }

        [SqliteProperty]
        public int AiCategory { get; set; }

        [SqliteProperty]
        public string SearchArea { get; set; }

        #endregion

        public AiCategory Ai
        {
            get
            {
                return (AiCategory)this.AiCategory;
            }
        }

        public string[] SearchAreaArray
        {
            get
            {
                return this.SearchArea.Split(',');
            }
        }
    }
}
