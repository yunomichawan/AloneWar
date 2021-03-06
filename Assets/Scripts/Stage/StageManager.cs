﻿using AloneWar.Common;
using AloneWar.Common.Component;
using AloneWar.Common.Extensions;
using AloneWar.DataObject.Json.Helper;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.Stage.Component;
using AloneWar.Stage.Controller;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Stage.Event.TriggerSender;
using AloneWar.Stage.FieldObject;
using AloneWar.Unit.Component;
using AloneWar.Unit.Status;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AloneWar.Stage
{
    /// <summary>
    /// ステージ全体の進行、データを管理
    /// </summary>
    [RequireComponent(typeof(TurnProgression))]
    [RequireComponent(typeof(StageEventBuilder))]
    [RequireComponent(typeof(StageObjectController))]
    [RequireComponent(typeof(StageUserOperation))]
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

        public TurnProgression tunrProgression;

        public StageEventBuilder stageEventBuilder;

        public StageObjectController stageObjectController;

        public StageUserOperation stageUserOperation;

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
                    yield return StartCoroutine(this.TaskNext());
                }
                // 通常タスク
                else if (this.TaskQueue.Count > 0)
                {
                    yield return StartCoroutine(this.TaskNext());
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
        /// 対象座標を範囲に含んでいるユニットの範囲を再設定
        /// </summary>
        /// <param name="positionId"></param>
        public void UnitRangeInit(int positionId)
        {
            this.StageInformation.UnitSubComponentList.Values.ToList().ForEach(u =>
            {
                // 移動が行われた場合、範囲を再設定
                u.StageRange.ResetRange(positionId);
            });
            this.StageInformation.UnitMainComponent.StageRange.ResetRange(positionId);
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
                this.CreateField(this.StageInformation.StageData.ConstitutionJson);
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
                        this.SetUnitPropety(c, u);
                    });
                });

                GameObject mainUnit = new GameObject();// ResourceManager.Load<GameObject>(u.BaseStatus.AssetId, ResourceCategory.UnitPrefab);
                EditorDebug.DebugAlert("prefab");
                this.CreateStageObject<UnitMainComponent>(mainUnit, c =>
                {
                    this.StageInformation.UnitMainComponent = c;
                    this.SetUnitPropety(c, this.StageInformation.UnitMainStatus);
                    c.UnitMainStatus = this.StageInformation.UnitMainStatus;
                });
            }

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="unit"></param>
            /// <param name="status"></param>
            private void SetUnitPropety(UnitBaseComponent unit, UnitBaseStatus status)
            {
                unit.transform.parent = this.UnitParent;
                unit.InitController();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="ParentT">BaseStageObjectを継承したクラス型</typeparam>
            /// <param name="prefab"></param>
            /// <param name="callback"></param>
            private void CreateStageObject<ParentT>(GameObject prefab, Action<ParentT> callback)
                where ParentT : BaseStageObject
            {
                GameObject stageObject = prefab ?? new GameObject();
                ParentT attachParent = stageObject.AddComponent<ParentT>();
                stageObject.name = attachParent.StageObjectId.ToString();
                GameObject instantiateObject = Instantiate(stageObject, StageManager.Instance.StageInformation.GetPositionFromId(attachParent.PositionId), Quaternion.identity);
                instantiateObject.AddComponent<Collider2D>();
                EditorDebug.DebugAlert("Collider2Dの詳細設定");

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
                StageEventBuilder builder = StageManager.Instance.stageEventBuilder;
                foreach (StageEventInformation stageEventInformation in this.StageInformation.StageEventTableDataList)
                {
                    StageEventHandler handler = builder.CreateEventHandler(stageEventInformation);
                    handler.SetStageEventTrigger();
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
