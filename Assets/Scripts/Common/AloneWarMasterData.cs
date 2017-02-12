using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Common.Component;

namespace AloneWar.Common
{
    /// <summary>
    /// 不変のデータ、全シーンで使用するデータを保持
    /// </summary>
    public class AloneWarMasterStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public SaveData SaveData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public GameConfigData GameConfigData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, ItemData> ItemTableDataList { get { return this.itemTableDataList; } set { this.itemTableDataList = value; } }
        private Dictionary<int, ItemData> itemTableDataList = new Dictionary<int, ItemData>();
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, SkillData> SkillTableDataList { get { return this.skillTableDataList; } set { this.skillTableDataList = value; } }
        private Dictionary<int, SkillData> skillTableDataList = new Dictionary<int, SkillData>();
        
    }
}
