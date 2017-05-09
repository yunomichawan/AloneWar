using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.Stage.Event.EventObject;
using System.Collections.Generic;

namespace AloneWar.Stage.Event.TriggerSender
{
    /// <summary>
    /// ユニットに対するイベントトリガーの基本クラス
    /// </summary>
    /// 
    public class UnitTriggerSender
    {
        /// <summary>
        /// 
        /// </summary>
        public List<int> UnitStageStatusIdList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AiCategory ChangeAiCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UnitSideCategory ChangeUnitSideCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StatusGrantedParamId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StatusGrantedParamData StatusGrantedParamData { get; set; }

    }
}
