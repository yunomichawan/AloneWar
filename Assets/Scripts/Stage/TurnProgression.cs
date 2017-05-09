using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.Stage.Event.EventObject;
using AloneWar.Unit.Component;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AloneWar.Stage
{
    /// <summary>
    /// 
    /// </summary>
    public class TurnProgression : MonoBehaviour
    {
        #region property

        public int Turn { get; set; }

        public TurnCategory TurnCategory { get { return this.turnCategory; } set { this.turnCategory = value; } }
        private TurnCategory turnCategory = TurnCategory.Player;

        public static TurnProgression Instance
        {
            get
            {
                return StageManager.Instance.tunrProgression;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<StageEventHandler> StageEventHandlerList { get { return this.stageEventHandlerList; } set { this.stageEventHandlerList = value; } }
        private List<StageEventHandler> stageEventHandlerList = new List<StageEventHandler>();

        #endregion

        /// <summary>
        /// ターン開始時処理
        /// </summary>
        /// <param name="turn"></param>
        public void TurnStart(TurnCategory turn)
        {
            UnitSummaryComponent unitSummaryComponent = new UnitSummaryComponent();
            this.TurnCategory = turn;
            switch (turn)
            {
                case TurnCategory.Player:
                    unitSummaryComponent = StageManager.Instance.StageInformation.SearchUnit(UnitSideCategory.Player);
                    break;
                case TurnCategory.PlayerUnit: //
                    break;
                case TurnCategory.Enemy:
                    unitSummaryComponent = StageManager.Instance.StageInformation.SearchUnit(UnitSideCategory.Enemy);
                    break;
                case TurnCategory.EnemyUnit: //
                    break;
                case TurnCategory.Another: //
                    break;
            }

            this.UnitInitWhenTurnChange();

            StageManager.Instance.TaskQueue.Enqueue(this.DisplayTurnBanner);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitSummaryComponent"></param>
        private void UnitInitWhenTurnChange()
        {
            StageManager.Instance.StageInformation.UnitSubComponentList.Values.ToList().ForEach(u =>
            {
                u.UnitCommandController.Active();
            });
            StageManager.Instance.StageInformation.UnitMainComponent.UnitCommandController.Active();
        }

        /// <summary>
        /// バナーをアニメーション表示する
        /// </summary>
        /// <param name="turnCategory"></param>
        /// <returns></returns>
        private IEnumerator DisplayTurnBanner()
        {
            EditorDebug.DebugAlert("Banner Slide Anim");
            yield return null;
            // 以下を想定
            // yield return StartCorutine();

            // 初期化処理終了後にイベント発火
            this.StageEventHandlerList.ForEach(t =>
            {
                if (t.StageTriggerSender.IsValidTrigger(this.Turn))
                {
                    t.EnqueueEventTask();
                }
            });
        }
    }
}
