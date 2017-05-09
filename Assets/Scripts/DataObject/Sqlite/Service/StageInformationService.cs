using AloneWar.Common.Bind;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.Stage;
using System.Collections.Generic;
using System.Linq;

namespace AloneWar.DataObject.Sqlite.Service
{
    public class StageInformationService
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StageInformationService()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainUnitId"></param>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public StageInformation GetStageInformation(int mainUnitId,int stageId)
        {
            StageInformation stageInformation = new StageInformation();
            // Stage
            SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder(typeof(StageData));
            sqliteQueryBuilder.AddJoinTable(typeof(StageData), typeof(StageClearTriggerData), "Id", "StageId");
            sqliteQueryBuilder.AddCondition("Id", stageId, typeof(StageData), true);
            DataTable dataTable = SqliteHelper.Instance.GetSqliteObjectTable(sqliteQueryBuilder);
            stageInformation.StageData = DataBinding<StageData>.DataTableToObjectList(dataTable).First();
            stageInformation.StageClearTriggerData = DataBinding<StageClearTriggerData>.DataTableToObjectList(dataTable).First();
            
            // Placement
            sqliteQueryBuilder = new SqliteQueryBuilder(typeof(UnitStagePlacementData));
            sqliteQueryBuilder.AddCondition("Id", stageId, typeof(StageData), true);
            List<UnitStagePlacementData> unitStagePlacementDataList = SqliteHelper.Instance.GetSqliteObjectList<UnitStagePlacementData>(sqliteQueryBuilder);
            stageInformation.UnitStagePlacementDataList = unitStagePlacementDataList;

            // Event
            sqliteQueryBuilder = new SqliteQueryBuilder(typeof(StageEventTriggerData));
            sqliteQueryBuilder.AddJoinTable(typeof(EventData), typeof(StageEventTriggerData), "Id", "EventId");
            sqliteQueryBuilder.AddJoinTable(typeof(EventTriggerData), typeof(StageEventTriggerData), "Id", "TriggerId");
            sqliteQueryBuilder.AddCondition("StageId", stageId, typeof(StageEventTriggerData), true);
            sqliteQueryBuilder.AddOrderByColumns("SortNo", typeof(StageEventTriggerData), true);
            dataTable = SqliteHelper.Instance.GetSqliteObjectTable(sqliteQueryBuilder);
            List<StageEventTriggerData> stageEventTriggerDataList = DataBinding<StageEventTriggerData>.DataTableToObjectList(dataTable);
            List<EventTriggerData> eventTriggerDataList = DataBinding<EventTriggerData>.DataTableToObjectList(dataTable);
            List<EventData> eventDataList = DataBinding<EventData>.DataTableToObjectList(dataTable);

            sqliteQueryBuilder = new SqliteQueryBuilder(typeof(EventStatusData));
            sqliteQueryBuilder.AddCondition("StageId", stageId, typeof(EventStatusData), true);
            List<EventStatusData> eventStatusDataList = SqliteHelper.Instance.GetSqliteObjectList<EventStatusData>(sqliteQueryBuilder);
            stageInformation.SetStageEventTableDataList(stageEventTriggerDataList, eventTriggerDataList, eventDataList, eventStatusDataList);
            
            // Unit
            UnitStatusService unitStatusService = new UnitStatusService();
            // メインについてはデータの持ち方を確立してから
            //stageInformation.UnitMainStatus = unitStatusService.GetUnitMainObejctStatusOnStage(mainUnitId, stageId);
            // Sub
            stageInformation.UnitSubStatusList = unitStatusService.GetUnitSubObejctStatusListOnStage(stageId, true);

            return stageInformation;
        }
    }
}
