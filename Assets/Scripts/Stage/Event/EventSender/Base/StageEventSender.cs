using AloneWar.Common.Extensions;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using System.Collections;

namespace AloneWar.Stage.Event.EventSender
{
    /// <summary>
    /// イベントの基本クラス
    /// ・継承先のコンストラクタで引数を実装しないこと
    /// </summary>
    public abstract class StageEventSender
    {
        public StageEventSender()
        {
        }

        #region 抽象

        public abstract IEnumerator InvokeEvent();

        /// <summary>
        /// 引数クラスに変換する処理を実装
        /// 共通化できそうな気もしなくもない
        /// </summary>
        /// <param name="eventTriggerData"></param>
        public abstract void ConvertSender(EventData eventData);

        #endregion
    }
}
