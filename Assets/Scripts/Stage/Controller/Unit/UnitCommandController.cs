using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Stage.Component;
using AloneWar.Stage.FieldObject;
using AloneWar.Unit.Status;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.UI;
using UnityEngine;
using AloneWar.Stage.Event.EventObject;
using AloneWar.UI.Stage;
using AloneWar.Unit.Component;
using AloneWar.Common.Extensions;

namespace AloneWar.Stage.Controller.Unit
{
    /// <summary>
    /// ユニット単位のコントローラ
    /// </summary>
    public class UnitCommandController
    {
        #region property

        #region event

        /// <summary>
        /// ユニット単位の移動イベント
        /// </summary>
        public List<PositionEvent> MoveEvent { get { return this.moveEvent; } set { this.moveEvent = value; } }
        private List<PositionEvent> moveEvent = new List<PositionEvent>();

        /// <summary>
        /// 
        /// </summary>
        public List<UnitEvent> KillEvent { get { return this.killEvent; } set { this.killEvent = value; } }
        private List<UnitEvent> killEvent = new List<UnitEvent>();

        #endregion

        /// <summary>
        /// 履歴
        /// </summary>
        public Stack<UnitCommandHistory> UnitCommandHistoryStack { get { return this.unitCommandHistoryStack; } set { this.unitCommandHistoryStack = value; } }
        private Stack<UnitCommandHistory> unitCommandHistoryStack = new Stack<UnitCommandHistory>();

        /// <summary>
        /// 
        /// </summary>
        private UnitBaseComponent UnitBaseComponent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UnitRoot UnitRoot { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unitBaseStatus"></param>
        /// <param name="monoBehaviour"></param>
        public UnitCommandController(UnitBaseComponent unitBaseComponent)
        {
            this.UnitBaseComponent = unitBaseComponent;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveUnit()
        {
            // トリガーチェック
            this.KillEvent.ForEach(k =>
            {
                if (k.ValidFlg)
                {
                    // イベント実行後に行うコールバックを設定
                    k.EventAfterCallback = () =>
                    {
                        int positionId = this.UnitBaseComponent.PositionId;
                        // 内部データより削除
                        StageManager.Instance.StageInformation.UnitSubComponentList.Remove(positionId);
                        // 再設定 
                        StageManager.Instance.UnitRangeInit(positionId);
                        // 本体削除
                        UnityEngine.Object.Destroy(this.UnitBaseComponent);
                    };
                    k.EnqueueEventTask();
                }
            });
        }

        /// <summary>
        /// 現在の座標からイベントが有効かどうか判断する。
        /// </summary>
        public void ValidPositionEvent()
        {
            this.MoveEvent.ForEach(m =>
            {
                bool valid = m.SetValidEvent(this.UnitBaseComponent.PositionId);
                // イベントが有効だった場合は待機に移行
                if (valid)
                {
                    this.Wait();
                }
            });
        }

        #region Command

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toPositionId"></param>
        private void Move()
        {
            this.UnitCommandHistoryStack.Push(new UnitCommandHistory(CommandCategory.Move));
            StageManager.Instance.TaskQueue.Enqueue(this.MoveTask);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator MoveTask()
        {
            this.Move();
            yield return this.UnitBaseComponent.StartCoroutine(this.MoveRoot(this.UnitRoot.RootPositionList));
            // 座標更新 & イベントチェック
            StageObjectController.Instance.UpdateUnitPosition(this.UnitBaseComponent, this.UnitRoot.RootList.Last().MassComponent.PositionId);
            // ユニークイベントチェック
            this.ValidPositionEvent();

            StageManager.Instance.UnitRangeInit(this.UnitBaseComponent.UnitBaseStatus.StageStatus.BeforePositionId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionRoot"></param>
        /// <returns></returns>
        private IEnumerator MoveRoot(List<Vector3> positionRoot)
        {
            foreach (Vector3 position in positionRoot)
            {
                // 座標を設定
                this.UnitBaseComponent.NavMeshAgent.SetDestination(position);
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    EditorDebug.DebugAlert("移動制御");
                    // 対象座標付近まで移動したら次の座標へ
                    if (Vector3.Distance(this.UnitBaseComponent.transform.position, position) < 0.1f)
                    {
                        this.UnitBaseComponent.transform.position = position;
                        break;
                    }
                }
            }

            // 
            this.UnitBaseComponent.transform.position = positionRoot.Last();
            this.UnitBaseComponent.NavMeshAgent.ResetPath();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Wait()
        {
            this.unitCommandHistoryStack.Clear();
            this.UnitBaseComponent.UnitBaseStatus.StageStatus.Wait = true;
        }

        /// <summary>
        /// 戻る
        /// </summary>
        public void Back()
        {
            UnitCommandHistory unitCommandHistory = this.UnitCommandHistoryStack.Pop();
            UIUnitCommand.Instance.SetCommand(unitCommandHistory);
        }

        #endregion
    }
}
