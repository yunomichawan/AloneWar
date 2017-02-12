﻿using AloneWar.Common.Component;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.Stage.Component;
using AloneWar.Unit.Component;
using AloneWar.Unit.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AloneWar.Stage
{
    /// <summary>
    /// 
    /// </summary>
    public class StageInformation
    {
        #region DB Property

        /// <summary>
        /// 
        /// </summary>
        public StageData StageTableData { get { return this.stageTableData; } set { this.stageTableData = value; } }
        private StageData stageTableData = new StageData();

        /// <summary>
        /// 
        /// </summary>
        public StageClearTriggerData StageClearTriggerData { get { return this.stageClearTriggerData; } set { this.stageClearTriggerData = value; } }
        private StageClearTriggerData stageClearTriggerData = new StageClearTriggerData();

        /// <summary>
        /// 
        /// </summary>
        public List<StageEventInformation> StageEventTableDataList { get { return this.stageEventTableDataList; } set { this.stageEventTableDataList = value; } }
        private List<StageEventInformation> stageEventTableDataList = new List<StageEventInformation>();

        /// <summary>
        /// メイン配置
        /// </summary>
        public UnitObjectStatus<UnitMainStatusData> UnitMainStatus { get { return this.unitMainStatus; } set { this.unitMainStatus = value; } }
        private UnitObjectStatus<UnitMainStatusData> unitMainStatus = new UnitObjectStatus<UnitMainStatusData>();

        /// <summary>
        /// サブ配置
        /// </summary>
        public List<UnitObjectStatus<UnitSubStatusData>> UnitSubStatusList { get { return this.unitSubStatusList; } set { this.unitSubStatusList = value; } }
        private List<UnitObjectStatus<UnitSubStatusData>> unitSubStatusList = new List<UnitObjectStatus<UnitSubStatusData>>();

        /// <summary>
        /// 初期配置可能座標
        /// </summary>
        public List<UnitStagePlacementData> UnitStagePlacementDataList { get { return this.unitStagePlacementDataList; } set { this.unitStagePlacementDataList = value; } }
        private List<UnitStagePlacementData> unitStagePlacementDataList = new List<UnitStagePlacementData>();

        #endregion

        #region Component

        /// <summary>
        /// 
        /// </summary>
        public UnitMainComponent UnitMainComponent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, MassComponent> MassComponentList { get { return this.massComponentList; } set { this.massComponentList = value; } }
        private Dictionary<int, MassComponent> massComponentList = new Dictionary<int, MassComponent>();

        /// <summary>
        /// /
        /// </summary>
        public Dictionary<int, UnitSubComponent> UnitSubComponentList { get { return this.unitSubComponentList; } set { this.unitSubComponentList = value; } }
        private Dictionary<int, UnitSubComponent> unitSubComponentList = new Dictionary<int, UnitSubComponent>();

        #endregion

        #region Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stageEventTableDataList"></param>
        /// <param name="eventTableDataList"></param>
        public void SetStageEventTableDataList(List<StageEventTriggerData> stageEventTriggerDataList, List<EventTriggerData> eventTriggerDataList, List<EventData> eventDataList, List<EventStatusData> eventStatusDataList)
        {
            for (int i = 0; i < stageEventTableDataList.Count; i++)
            {
                this.StageEventTableDataList.Add(new StageEventInformation(stageEventTriggerDataList[i], eventTriggerDataList[i], eventDataList[i], eventStatusDataList[i]));
            }
        }

        #region 座標

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public string GetPositionArea(int positionId)
        {
            return this.MassComponentList[positionId].MassStatus.Area;
        }

        /// <summary>
        /// 座標Id
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetPositionId(int x, int y)
        {
            return this.StageTableData.Width * y + x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public int GetPositionX(int positionId)
        {
            return positionId % this.StageTableData.Width;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public int GetPositionY(int positionId)
        {
            return positionId / this.StageTableData.Width;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPositionFromId(int positionId)
        {
            int x = GetPositionX(positionId);
            int y = GetPositionY(positionId);
            return this.GetPositionTransformFromXY(x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Vector3 GetPositionTransformFromXY(int x, int y)
        {
            float massScale = 1.0f;
            return new Vector3((float)(x * massScale), (float)(y * massScale));
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class StageEventInformation
    {
        /// <summary>
        /// 
        /// </summary>
        public StageEventTriggerData StageEventTriggerData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EventTriggerData EventTriggerData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EventData EventData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EventStatusData EventStatusData { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="stageEventTableData"></param>
        /// <param name="eventTableData"></param>
        public StageEventInformation(StageEventTriggerData stageEventTriggerData, EventTriggerData eventTriggerData, EventData eventData, EventStatusData eventStatusData)
        {
            this.StageEventTriggerData = stageEventTriggerData;
            this.EventTriggerData = eventTriggerData;
            this.EventData = eventData;
            this.EventStatusData = eventStatusData;
        }
    }
}
