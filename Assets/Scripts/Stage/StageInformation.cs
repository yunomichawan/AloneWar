using AloneWar.Common;
using AloneWar.Common.Component;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.Stage.Component;
using AloneWar.Stage.Controller.Unit;
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
        public StageData StageData { get { return this.stageData; } set { this.stageData = value; } }
        private StageData stageData = new StageData();

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
        /// 初期配置可能座標
        /// </summary>
        public List<UnitStagePlacementData> UnitStagePlacementDataList { get { return this.unitStagePlacementDataList; } set { this.unitStagePlacementDataList = value; } }
        private List<UnitStagePlacementData> unitStagePlacementDataList = new List<UnitStagePlacementData>();

        /// <summary>
        /// 
        /// </summary>
        public UnitMainStatus UnitMainStatus { get { return this.unitMainStatus; } set { this.unitMainStatus = value; } }
        private UnitMainStatus unitMainStatus = new UnitMainStatus();

        /// <summary>
        /// 
        /// </summary>
        public List<UnitSubStatus> UnitSubStatusList { get { return this.unitSubStatusList; } set { this.unitSubStatusList = value; } }
        private List<UnitSubStatus> unitSubStatusList = new List<UnitSubStatus>();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionIdArray"></param>
        /// <returns></returns>
        public UnitSummaryComponent SearchUnitComponent(List<int> positionIdArray, UnitSideCategory unitSideCategory)
        {
            UnitSummaryComponent unitSummaryComponent = new UnitSummaryComponent();

            foreach (int positionId in positionIdArray)
            {
                if (this.UnitMainComponent.UnitMainStatus.StageStatus.PositionId.Equals(positionId))
                {
                    unitSummaryComponent.UnitMainComponent = this.UnitMainComponent;
                }

                if (this.UnitSubComponentList.ContainsKey(positionId))
                {
                    UnitSubComponent unitSubComponent = this.UnitSubComponentList[positionId];
                    if (unitSubComponent.UnitBaseStatus.StageStatus.UnitSide.Equals(unitSideCategory))
                    {
                        unitSummaryComponent.UnitSubComponentList.Add(unitSubComponent);
                    }
                }
            }

            return unitSummaryComponent;
        }

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
            if (x > this.StageData.Width || y > this.StageData.Height)
            {
                return AloneWarConst.ErrorPositionId;
            }
            else
            {
                return this.StageData.Width * y + x;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public int GetPositionX(int positionId)
        {
            return positionId % this.StageData.Width;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public int GetPositionY(int positionId)
        {
            return positionId / this.StageData.Width;
        }

        /// <summary>
        /// 座標IDに方角分の座標を加算
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="rangeDirection"></param>
        /// <returns></returns>
        public int GetDirectionPositionId(int positionId, RangeDirection rangeDirection)
        {
            return positionId + this.GetDirectionPositionParam(rangeDirection);
        }

        /// <summary>
        /// 方角から加算する座標値を取得
        /// </summary>
        /// <param name="rangeDirection"></param>
        /// <returns></returns>
        public int GetDirectionPositionParam(RangeDirection rangeDirection)
        {
            switch (rangeDirection)
            {
                case RangeDirection.Top:
                    return this.StageData.Width;
                case RangeDirection.Bottom:
                    return this.StageData.Width;
                case RangeDirection.Right:
                    return 1;
                case RangeDirection.Left:
                    return -1;
            }

            return 0;
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

        /// <summary>
        /// 距離間を取得
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="comparePositionId"></param>
        /// <returns></returns>
        public int GetSenceOfDistance(int positionId, int comparePositionId)
        {
            int x = this.GetPositionX(positionId);
            int y = this.GetPositionY(positionId);

            int compareX = this.GetPositionX(comparePositionId);
            int compareY = this.GetPositionY(comparePositionId);

            return Math.Abs(x - compareX) + Math.Abs(y - compareY);
        }

        /// <summary>
        /// 起点座標から離れた座標リストを取得
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="distance"></param>
        /// <param name="getCondition">座標取得条件</param>
        /// <returns></returns>
        public List<int> GetDistaceAwayPositionList(int positionId, int distance, Func<int, bool> getCondition = null)
        {
            List<int> positionList = new List<int>();
            int range0 = distance;
            int range1 = 0;
            int x = this.GetPositionX(positionId);
            int y = this.GetPositionY(positionId);

            while (range0 > 0)
            {
                // top - right
                this.AddDistancePositionIfValid(positionList, this.GetPositionId(x + range1, y - range0));
                // right - bottom
                this.AddDistancePositionIfValid(positionList, this.GetPositionId(x + range0, y + range1));
                // bottom - left
                this.AddDistancePositionIfValid(positionList, this.GetPositionId(x - range1, y + range0));
                // left - top
                this.AddDistancePositionIfValid(positionList, this.GetPositionId(x - range0, y - range1));

                range0--;
                range1++;
            }

            return positionList;
        }

        /// <summary>
        /// 座標が有効であれば座標をリストに追加
        /// </summary>
        /// <param name="positionList"></param>
        /// <param name="positionId"></param>
        /// <param name="getCondition"></param>
        private void AddDistancePositionIfValid(List<int> positionList, int positionId, Func<int, bool> getCondition = null)
        {
            if (!positionId.Equals(AloneWarConst.ErrorPositionId))
            {
                if (getCondition != null)
                {
                    if (getCondition(positionId))
                    {
                        positionList.Add(positionId);
                    }
                }
                else
                {
                    positionList.Add(positionId);
                }
            }
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
