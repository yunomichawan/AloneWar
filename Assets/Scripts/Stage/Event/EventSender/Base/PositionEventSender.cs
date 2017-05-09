using System;
using System.Collections;

namespace AloneWar.Stage.Event.EventSender
{
    /// <summary>
    /// 座標イベントの引数クラス
    /// </summary>
    public class PositionEventSender
    {
        public int[] PositionIdArray { get; set; }

        public string[] AreaArray { get; set; }
    }
}