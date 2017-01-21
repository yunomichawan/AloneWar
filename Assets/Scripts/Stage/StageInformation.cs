using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Unit.Status;

namespace AloneWar.Stage
{
    public class StageInformation
    {
        /// <summary>
        /// 
        /// </summary>
        public StageTableData StageTableData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StageClearTableData StageClearTableData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<StageEventTableData> StageEventTableDataList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UnitObjectStatus<UnitMainStatusData> UnitMainStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<UnitObjectStatus<UnitSubStatusData>> UnitSubStatusList { get; set; }

    }
}
