using AloneWar.Common.Extensions;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.Stage.Event.EventObject;

namespace AloneWar.Stage.Event.TriggerSender
{
    /// <summary>
    /// トリガーの基本クラス
    /// ・継承先のコンストラクタで引数を実装しないこと
    /// </summary>
    public abstract class StageTriggerSender
    {

        #region 抽象

        /// <summary>
        /// 引数クラスに変換する処理を実装
        /// </summary>
        /// <param name="eventTriggerData"></param>
        public abstract void ConvertSender(EventTriggerData eventTriggerData);

        /// <summary>
        /// 循環参照になる？
        /// </summary>
        /// <param name="handler"></param>
        public abstract void SetTriggerToStage(StageEventHandler handler);

        /// <summary>
        /// トリガー有効判定処理
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValidTrigger(object arg);

        #endregion
    }
}
