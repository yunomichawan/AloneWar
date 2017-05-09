using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.Stage.Component;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Stage.Event.EventSender;
using AloneWar.Stage.Event.TriggerSender;
using AloneWar.Unit.Component;
using AloneWar.Unit.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        /// 
        /// </summary>
        public Dictionary<int, UnitSubComponent> UnitSubComponentList { get { return this.unitSubComponentList; } set { this.unitSubComponentList = value; } }
        private Dictionary<int, UnitSubComponent> unitSubComponentList = new Dictionary<int, UnitSubComponent>();

        #endregion

        #region Method

        #region Search

        /* 実装メモ
         * 増えすぎた場合
         * ・仕様箇所が少ない関数は削除し、コールバック引数をpublicとする
         */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionIdArray"></param>
        /// <param name="unitSideCategory"></param>
        /// <returns></returns>
        public UnitSummaryComponent SearchUnit(List<int> positionIdArray, UnitSideCategory unitSideCategory)
        {
            return this.SearchUnit(b => b.UnitSide.Equals(unitSideCategory) && positionIdArray.Contains(b.PositionId));
        }

        /// <summary>
        /// ユニットサイド検索
        /// </summary>
        /// <param name="unitSideCategory"></param>
        /// <returns></returns>
        public UnitSummaryComponent SearchUnit(UnitSideCategory unitSideCategory)
        {
            return this.SearchUnit(b => b.UnitSide.Equals(unitSideCategory));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="unitSideCategory"></param>
        /// <returns></returns>
        public UnitSummaryComponent SearchUnit(int positionId, UnitSideCategory unitSideCategory)
        {
            return this.SearchUnit(b => b.UnitSide.Equals(unitSideCategory) && b.PositionId.Equals(positionId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public UnitSummaryComponent SearchUnit(int positionId)
        {
            return this.SearchUnit(b => b.PositionId.Equals(positionId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitStageStatusId"></param>
        /// <returns></returns>
        public UnitSummaryComponent SearchUnitStage(int unitStageStatusId)
        {
            return this.SearchUnit(u => u.UnitBaseStatus.StageStatus.Id.Equals(unitStageStatusId));
        }

        /// <summary>
        /// 条件を満たすユニットを検索
        /// publicにしても問題ないのでは？
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public UnitSummaryComponent SearchUnit(Func<UnitBaseComponent, bool> func)
        {
            UnitSummaryComponent unitSummaryComponent = new UnitSummaryComponent();
            unitSummaryComponent.UnitSubComponentList = this.UnitSubComponentList.Values.Where(u => func(u)).ToList();
            if (func(this.UnitMainComponent))
            {
                unitSummaryComponent.UnitMainComponent = this.UnitMainComponent;
            }

            return unitSummaryComponent;
        }

        #endregion

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
                    return this.StageData.Width * -1;
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

            this.SearchDistaceAwayPosition(positionId, distance, (id) =>
            {
                if (this.DistancePositionValid(id, getCondition))
                {
                    positionList.Add(id);
                }
                return false;
            });

            return positionList;
        }

        /// <summary>
        /// 対象座標から離れた距離に有効な座標が存在するかどうか
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="distance"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool IsValidDistanceRange(int positionId, int distance, Func<int, bool> condition)
        {
            bool isValid = false;
            this.SearchDistaceAwayPosition(positionId, distance, (id) =>
            {
                isValid = condition(id);
                return isValid;
            });

            return isValid;
        }

        /// <summary>
        /// 座標ループをこの関数内で共通化させる
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="distance"></param>
        /// <param name="breakLoppCallback">int = positionId</param>
        private void SearchDistaceAwayPosition(int positionId, int distance, Func<int,bool> breakLoppCallback)
        {
            int range0 = distance;
            int range1 = 0;
            int x = this.GetPositionX(positionId);
            int y = this.GetPositionY(positionId);

            while (range0 > 0)
            {
                // top - right
                if (breakLoppCallback(this.GetPositionId(x + range1, y - range0)))
                    break;
                // right - bottom
                if (breakLoppCallback(this.GetPositionId(x + range0, y + range1)))
                    break;
                // bottom - left
                if (breakLoppCallback(this.GetPositionId(x - range1, y + range0)))
                    break;
                // left - top
                if (breakLoppCallback(this.GetPositionId(x - range0, y - range1)))
                    break;

                range0--;
                range1++;
            }
        }

        /// <summary>
        /// 座標が有効/無効
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="condition">条件</param>
        private bool DistancePositionValid(int positionId, Func<int, bool> condition)
        {
            if (!positionId.Equals(AloneWarConst.ErrorPositionId))
            {
                if (condition != null)
                {
                    return condition(positionId);
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

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

        #region debug only code

        public void CreateEventHandler()
        {
            //Debug.LogError("don't use this method! this method is debug only!");
            Action<Action> act = (callback) => {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                for (int i = 0; i < 1000; i++)
                {
                    callback();
                }
                sw.Stop();
                System.Console.WriteLine("Create Instance:"+sw.Elapsed);
            };

            // 速度計測-----------------------------------------------------

            // インスタンス生成に10～40倍近くの処理時間を要する為、共通化は却下。処理速度の観点上、素直にクラスを作ること。
            // コードは残していてもいいがデバッグ用とすること
            // 0.001 / 1s … 1回、0.0004 / 1s … 1000回
            //act(() => {
            //    Type triggerType = this.GetTriggerSenderType();
            //    Type eventType = this.GetEventSenderType();
            //    Type genericEventHandlerType = typeof(GenericEventHandler<,>);
            //    Type instanceType = genericEventHandlerType.MakeGenericType(eventType, triggerType);

            //    // 主に時間が掛かっている部分がインスタンスの生成、それを速くできないか試行錯誤してみたが一旦あきらめる

            //    // 実験1
            //    // StageEventHandler stageEventHandler = (StageEventHandler)Activator.CreateInstance(instanceType, this);
                
            //    // 実験2
            //    //ConstructorInfo c = instanceType.GetConstructor(new Type[] { typeof(StageEventInformation) });
            //    //StageEventInformation stageEventInformation = new StageEventInformation(null, null, null, null);
            //    //object obj = c.Invoke(new object[] { stageEventInformation });

            //    // 1と2で大差なし

            //    // コンストラクタのMethodInfoを取得して、そのデリゲートを作成したとしてもそれぞれの型によって生成しなければいけないので、その形式で実装してもあまり意味はないのでは？
            //    // ↑MethodInfoではなくConstructorInfoだった...
            //});

            // 0.0001 / 1s … 1回、0.0002 / 1s … 1000回
            //act(() => {
            //    GenericEventHandler<UnitAISender, PositionCloseTrigger> aa = new GenericEventHandler<UnitAISender, PositionCloseTrigger>(this);
            //});
            // 速度計測-----------------------------------------------------
        }

        private Type GetEventSenderType()
        {
            switch (this.EventData.Category)
            {
                case EventCategory.AiChange:
                    return typeof(UnitAISender);
            }

            return null;
        }

        private Type GetTriggerSenderType()
        {
            switch (this.EventTriggerData.Category)
            {
                case EventTriggerCategory.PositionClose:
                    return typeof(PositionCloseTrigger);
            }

            return null;
        }

        #endregion
    }
}
