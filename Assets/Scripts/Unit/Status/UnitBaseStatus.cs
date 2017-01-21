using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AloneWar.DataObject.Sqlite.SqliteObject;
using UnityEngine;

namespace AloneWar.Unit.Status
{
    public abstract class UnitBaseStatus
    {
        public UnitBaseStatusData BaseStatus { get; set; }

        public UnitStageStatusData StageStatus { get; set; }

        public List<ItemData> ItemList { get; set; }

        public List<SkillData> SkillList { get; set; }

        public UnitBaseStatus()
        {

        }
    }
}
