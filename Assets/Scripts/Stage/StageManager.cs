using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.Common.Component;
using AloneWar.Common.TaskHelper;
using AloneWar.DataObject.Json.Helper;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.Stage.Component;
using AloneWar.Stage.Event;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Stage.Event.EventObject.Base;
using AloneWar.Stage.FieldObject;
using AloneWar.Unit.Component;
using AloneWar.Unit.Status;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using AloneWar.Stage.Controller;

namespace AloneWar.Stage
{
    /// <summary>
    /// ステージ全体の進行、データを管理
    /// </summary>
    public class StageManager : SingletonTaskComponent<StageManager>
    {

        #region poroperty

        public StageInformation StageInformation { get; set; }

        /// <summary>
        /// 現在状況をメモ書き程度で保持(デバッグ用？)
        /// </summary>
        public string StageStatus { get; set; }

        #endregion

        #region inspector property

        /*
         タスク処理を1箇所にまとめるためinspectorで管理
         */

        public TunrProgression tunrProgression;

        public StageEventBuilder stageEventBuilder;

        public StageObjectController stageObjectController;

        /// <summary>
        /// 親
        /// </summary>
        public Transform unitParent;

        /// <summary>
        /// 親
        /// </summary>
        public Transform stageParent;

        #endregion

        IEnumerator Start()
        {
            while (true)
            {
                // イベントタスクを優先
                if (this.stageEventBuilder.TaskQueue.Count > 0)
                {
                    yield return StartCoroutine(this.TaskRun());
                }
                // 通常タスク
                else if (this.TaskQueue.Count > 0)
                {
                    yield return StartCoroutine(this.TaskRun());
                }
                yield return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stageInformation"></param>
        public void StageInit(StageInformation stageInformation)
        {
            this.StageInformation = stageInformation;
            // Field
            StageBuilder stageBuilder = new StageBuilder(this.StageInformation, this.unitParent, this.stageParent);
            stageBuilder.CreateStage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        public void UnitRangeInit(int positionId)
        {
            this.StageInformation.UnitSubComponentList.Values.ToList().ForEach(u =>
            {
                // 移動が行われた場合、範囲を再設定
                u.UnitRange.ResetRange(positionId);
            });
            this.StageInformation.UnitMainComponent.UnitRange.ResetRange(positionId);
        }

        /// <summary>
        /// 
        /// </summary>
        public class StageBuilder
        {
            public StageInformation StageInformation { get; set; }

            public Transform UnitParent { get; set; }

            public Transform StageParent { get; set; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="stageInformation"></param>
            /// <param name="unitParent"></param>
            /// <param name="stageParent"></param>
            public StageBuilder(StageInformation stageInformation, Transform unitParent, Transform stageParent)
            {
                this.StageInformation = stageInformation;
                this.UnitParent = unitParent;
                this.StageParent = stageParent;
            }

            /// <summary>
            /// 
            /// </summary>
            public void CreateStage()
            {
                this.CreateField(this.StageInformation.StageTableData.ConstitutionJson);
                this.SetUnitPosition();
                this.SetStageEvent();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="constitutionJson"></param>
            private void CreateField(string constitutionJson)
            {
                List<MassStatus> massStatusList = JsonUtility.FromJson<JsonSerialization<MassStatus>>(constitutionJson).ToList();
                massStatusList.ForEach(m =>
                {
                    this.CreateStageObject<MassComponent>(null, (c) =>
                    {
                        this.StageInformation.MassComponentList.Add(c.PositionId, c);
                        c.MassStatus = m;
                        c.transform.parent = this.StageParent;
                    });
                });
            }

            /// <summary>
            /// 
            /// </summary>
            private void SetUnitPosition()
            {
                this.StageInformation.UnitSubStatusList.ForEach(u =>
                {
                    GameObject prefab = new GameObject();// ResourceManager.Load<GameObject>(u.BaseStatus.AssetId, ResourceCategory.UnitPrefab);
                    EditorDebug.DebugAlert("prefab");
                    this.CreateStageObject<UnitSubComponent>(prefab, c =>
                    {
                        this.StageInformation.UnitSubComponentList.Add(c.PositionId, c);
                        this.SetUnitPropety<UnitSubStatusData>(c, u);
                    });
                });

                GameObject mainUnit = new GameObject();// ResourceManager.Load<GameObject>(u.BaseStatus.AssetId, ResourceCategory.UnitPrefab);
                EditorDebug.DebugAlert("prefab");
                this.CreateStageObject<UnitMainComponent>(mainUnit, c =>
                {
                    this.StageInformation.UnitMainComponent = c;
                    this.SetUnitPropety<UnitMainStatusData>(c, this.StageInformation.UnitMainStatus);
                });
            }

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="unit"></param>
            /// <param name="status"></param>
            private void SetUnitPropety<T>(UnitBaseComponent<T> unit, UnitObjectStatus<T> status) where T : SqliteBaseData
            {
                unit.transform.parent = this.UnitParent;
                unit.UnitObjectStatus = status;
                unit.UnitCommandController = new UnitCommandController<T>(unit);
                unit.UnitRange = new UnitRange<T>(unit);
                unit.UnitRange.SetRange(unit.MainRange, unit.SubRange, unit.UnitObjectStatus.MainCommand, unit.UnitObjectStatus.SubCommand);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="ParentT">BaseStageObjectを継承したクラス型</typeparam>
            /// <typeparam name="ChildT">ParentTが継承するUnitBaseComponent<T, EventT>のクラス型</typeparam>
            /// <typeparam name="EventT">ParentTが継承するUnitBaseComponent<T, EventT>のクラス型</typeparam>
            /// <param name="prefab"></param>
            /// <param name="attachChild"></param>
            /// <param name="callback"></param>
            private void CreateStageObject<ParentT>(GameObject prefab, Action<ParentT> callback)
                where ParentT : BaseStageObject
            {
                GameObject stageObject = prefab ?? new GameObject();
                ParentT attachParent = stageObject.AddComponent<ParentT>();
                stageObject.name = attachParent.StageObjectId.ToString();
                Instantiate(stageObject, StageManager.Instance.StageInformation.GetPositionFromId(attachParent.PositionId), Quaternion.identity);

                if (callback != null)
                {
                    callback(attachParent);
                }
            }

            /// <summary>
            /// イベントは後付？もしくは生成と同タイミング？
            /// </summary>
            private void SetStageEvent()
            {
                foreach (StageEventInformation stageEventInformation in this.StageInformation.StageEventTableDataList)
                {
                    StageEventTriggerData stageEventTriggerData = stageEventInformation.StageEventTriggerData;
                    EventData eventData = stageEventInformation.EventData;
                    EventTriggerData eventTriggerData =stageEventInformation.EventTriggerData;
                    EventTriggerCategory trigger = (EventTriggerCategory)eventTriggerData.TriggerCategory;
                    switch (trigger)
                    {
                        case EventTriggerCategory.AreaStop:
                            // set trigger To unit command
                            break;
                        case EventTriggerCategory.PositionStop:
                            // set trigger To unit command
                            break;
                        case EventTriggerCategory.PositionStopUniqueUnit:
                            break;
                        case EventTriggerCategory.PositionClose:
                            // set trigger To mass
                            List<int> positionIdList = eventTriggerData.TriggerSender.Split(',').Select<string, int>(s => { return int.Parse(s); }).ToList();
                            positionIdList.ForEach(p =>
                            {
                                MassComponent massComponent = this.StageInformation.MassComponentList[p];
                                PositionEvent massEvent = new PositionEvent(stageEventInformation, massComponent);
                                massComponent.CloseEventList.Add(massEvent);
                            });
                            break;
                        case EventTriggerCategory.TargetUnitKill:
                            
                            break;
                        case EventTriggerCategory.UnitKill:
                            // set trigger To battle
                            break;
                        case EventTriggerCategory.TurnOver:
                            // set trigger To tunrProgression
                            break;
                        case EventTriggerCategory.UnitAdjacent:
                            // set trigger To unit command
                            break;
                        case EventTriggerCategory.StageStart:
                            break;
                        case EventTriggerCategory.StageEnd:
                            break;
                        default:
                            break;
                    }
                }
            }

            /// <summary>
            /// 
            /// </summary>
            private void SetClearStageTrigger()
            {
                
            }
        }

    }
}
