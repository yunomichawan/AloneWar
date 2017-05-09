using AloneWar.Stage.Event.EventObject;
using System;

namespace AloneWar.Stage.Event.TriggerSender
{
    /// <summary>
    /// ターンに対するイベントトリガーの基本クラス
    /// </summary>
    [Serializable]
    public class TurnTriggerSender
    {
        /// <summary>
        /// nターンに開始、から開始
        /// </summary>
        public int TriggerTurnCount { get; set; }

        /// <summary>
        /// 毎ターン実行されるかどうか
        /// </summary>
        public int TurnInterval { get; set; }
    }
}
