using AloneWar.Common.Bind;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Unit.Status;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AloneWar.DataObject.Sqlite.Service
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">UnitMainStatusData or UnitSubStatusData</typeparam>
    public class UnitStatusService<T> where T : SqliteBaseData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class UnitSqliteSummary<T> where T : SqliteBaseData
        {
            public List<T> UnitStatusList { get; set; }
            public List<UnitBaseStatusData> UnitBaseStatusList { get; set; }
            public List<UnitStageStatusData> UnitStageStatusList { get; set; }

            public List<UnitObjectStatus<T>> GetUnitObjectStatusList()
            {
                List<UnitObjectStatus<T>> unitObjectStatusList = new List<UnitObjectStatus<T>>();
                for (int i = 0; i < this.UnitStatusList.Count; i++)
                {
                    UnitObjectStatus<T> unitObjectStatus = new UnitObjectStatus<T>();
                    unitObjectStatus.UnitStatus = this.UnitStatusList[i];
                    unitObjectStatus.BaseStatus = this.UnitBaseStatusList[i];
                    unitObjectStatus.StageStatus = this.UnitStageStatusList[i];
                    unitObjectStatusList.Add(unitObjectStatus);
                }

                return unitObjectStatusList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public UnitObjectStatus<T> GetUnitObejctStatusOnStage(int unitId, int stageId)
        {
            SqliteQueryBuilder builder = this.CreateQueryForUnitOnStage<T>(stageId);
            builder.AddCondition("Id", unitId, typeof(UnitBaseStatusData), true);
            DataTable dataTable = SqliteHelper.Instance.GetSqliteObjectTable(builder);

            UnitSqliteSummary<T> unitSqliteSummary = this.SetUnitSummary(dataTable);

            return unitSqliteSummary.GetUnitObjectStatusList().First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public List<UnitObjectStatus<T>> GetUnitObejctStatusListOnStage(int stageId)
        {
            SqliteQueryBuilder builder = this.CreateQueryForUnitOnStage<T>(stageId);
            DataTable dataTable = SqliteHelper.Instance.GetSqliteObjectTable(builder);
            Object lockObject = new Object();
            List<UnitObjectStatus<T>> unitObjectStatusList = new List<UnitObjectStatus<T>>();
            
            List<T> unitStatusList = new List<T>();
            UnitSqliteSummary<T> unitSqliteSummary = this.SetUnitSummary(dataTable);

            return unitSqliteSummary.GetUnitObjectStatusList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stageId"></param>
        /// <returns></returns>
        private SqliteQueryBuilder CreateQueryForUnitOnStage<T>(int stageId)
        {
            SqliteQueryBuilder builder = new SqliteQueryBuilder(typeof(UnitBaseStatusData));
            builder.AddJoinTable(typeof(UnitBaseStatusData), typeof(T), "Id");
            builder.AddJoinTable(typeof(UnitBaseStatusData), typeof(UnitStageStatusData), "Id", "UnitId");
            builder.AddCondition("StageId", stageId, typeof(UnitStageStatusData), true);
            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private UnitSqliteSummary<T> SetUnitSummary(DataTable dataTable)
        {
            UnitSqliteSummary<T> unitSummary = new UnitSqliteSummary<T>();
            unitSummary.UnitStatusList = DataBinding<T>.DataTableToObjectList(dataTable);
            unitSummary.UnitBaseStatusList = DataBinding<UnitBaseStatusData>.DataTableToObjectList(dataTable);
            unitSummary.UnitStageStatusList = DataBinding<UnitStageStatusData>.DataTableToObjectList(dataTable);
            return unitSummary;
        }
    }
}
