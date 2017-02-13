using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UnityEngine;
using AloneWar.DataObject.Sqlite.Service;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.Common.Component;
using AloneWar.Common;

namespace AloneWar.DataObject.Sqlite.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadStatusService
    {
        /// <summary>
        /// 
        /// </summary>
        public void SetLoadStatus()
        {
            
        }

        private void SetAloneWarMasterStatus()
        {
            SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder(typeof(SaveData));
            SaveData saveData = SqliteHelper.Instance.GetSqliteObjectFirst<SaveData>(sqliteQueryBuilder);

            sqliteQueryBuilder = new SqliteQueryBuilder(typeof(GameConfigData));
            GameConfigData configData = SqliteHelper.Instance.GetSqliteObjectFirst<GameConfigData>(sqliteQueryBuilder);

            sqliteQueryBuilder = new SqliteQueryBuilder(typeof(ItemData));
            List<ItemData> itemTableDataList = SqliteHelper.Instance.GetSqliteObjectList<ItemData>(sqliteQueryBuilder);

            sqliteQueryBuilder = new SqliteQueryBuilder(typeof(SkillData));
            List<SkillData> skillTableDataList = SqliteHelper.Instance.GetSqliteObjectList<SkillData>(sqliteQueryBuilder);

            SceneCommonComponent.Instance.SetMasterCommonObject(typeof(SaveData), saveData);
            SceneCommonComponent.Instance.SetMasterCommonObject(typeof(GameConfigData), configData);
            SceneCommonComponent.Instance.SetMasterCommonObject(typeof(Dictionary<int, ItemData>), itemTableDataList.ToDictionary(i => i.Id));
            SceneCommonComponent.Instance.SetMasterCommonObject(typeof(Dictionary<int, SkillData>), skillTableDataList.ToDictionary(i => i.Id));

            sqliteQueryBuilder = new SqliteQueryBuilder(typeof(ResourcesLabelData));
            sqliteQueryBuilder.AddCondition("IsEditor", "0", typeof(ResourcesLabelData), true);
            List<ResourcesLabelData> labelResourcesDataList = SqliteHelper.Instance.GetSqliteObjectList<ResourcesLabelData>(sqliteQueryBuilder);
        }
    }
}
