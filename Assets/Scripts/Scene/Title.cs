using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AloneWar.Common.Component;
using AloneWar.DataObject.Sqlite.Service;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.SqliteObject;

namespace AloneWar.Scene
{
    public class Title : MonoBehaviour
    {
        void Start()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private void Init()
        {
            //SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder(typeof(SaveData));
            //SaveData saveData = SqliteHelper.Instance.GetSqliteObjectFirst<SaveData>(sqliteQueryBuilder);
            
            //sqliteQueryBuilder = new SqliteQueryBuilder(typeof(SkillTableData));
            //List<SkillTableData> skillTableDataList = SqliteHelper.Instance.GetSqliteObjectList<SkillTableData>(sqliteQueryBuilder);
            
            //sqliteQueryBuilder = new SqliteQueryBuilder(typeof(ItemData));
            //List<ItemData> itemTableDataList = SqliteHelper.Instance.GetSqliteObjectList<ItemData>(sqliteQueryBuilder);

            //sqliteQueryBuilder = new SqliteQueryBuilder(typeof(UnitMainStatusData));
            //UnitMainStatusData unitMainStatusData = SqliteHelper.Instance.GetSqliteObjectFirst<UnitMainStatusData>(sqliteQueryBuilder);

            //sqliteQueryBuilder = new SqliteQueryBuilder(typeof(LabelResourcesData),true);
            //sqliteQueryBuilder.AddCondition("IsEditor", "0", typeof(LabelResourcesData), true);
            //List<LabelResourcesData> labelResourcesDataList = SqliteHelper.Instance.GetSqliteObjectList<LabelResourcesData>(sqliteQueryBuilder);

        }

    }
}
