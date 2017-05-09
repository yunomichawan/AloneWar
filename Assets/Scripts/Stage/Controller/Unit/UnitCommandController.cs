using AloneWar.Common.Extensions;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Unit.Component;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        public List<StageEventHandler> MoveEvent { get { return this.moveEvent; } set { this.moveEvent = value; } }
        private List<StageEventHandler> moveEvent = new List<StageEventHandler>();

        /// <summary>
        /// 
        /// </summary>
        public List<StageEventHandler> KillEvent { get { return this.killEvent; } set { this.killEvent = value; } }
        private List<StageEventHandler> killEvent = new List<StageEventHandler>();

        #endregion

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
                if (m.StageTriggerSender.IsValidTrigger(this.UnitBaseComponent.PositionId))
                {
                    m.EnqueueEventTask();
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
        private void Wait()
        {
            this.UnitBaseComponent.UnitBaseStatus.StageStatus.Wait = true;
            // 影を落とす
            UnityExtensions.ShadowRender(this.UnitBaseComponent.spriteRenderer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaitTask()
        {
            this.Wait();
            yield return UnityExtensions.Wait1Frame();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Active()
        {
            this.UnitBaseComponent.UnitBaseStatus.StageStatus.Wait = false;
            // デフォルトに戻す
            UnityExtensions.ShadowRender(this.UnitBaseComponent.spriteRenderer);
            EditorDebug.DebugAlert("default rendrer");
        }

        /// <summary>
        /// 戻る
        /// </summary>
        public void Back()
        {

        }

        #endregion
    }
}
