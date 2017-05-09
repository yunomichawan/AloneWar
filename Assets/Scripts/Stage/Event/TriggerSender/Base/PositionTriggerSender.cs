using AloneWar.Stage.Event.EventObject;
using System;

namespace AloneWar.Stage.Event.TriggerSender
{
    /// <summary>
    /// 座標に対するイベント引数クラス
    /// </summary>
    [Serializable]
    public class PositionTriggerSender
    {

        public string[] AreaArray { get; set; }

        public int[] PositionIdArray { get; set; }
    }
}
