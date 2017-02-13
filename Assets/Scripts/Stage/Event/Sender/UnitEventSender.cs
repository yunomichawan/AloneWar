using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Stage.Event.Sender.Base;
using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;

namespace AloneWar.Stage.Event.Sender
{
    /// <summary>
    /// 特定ユニットに向けたイベント引数を実装
    /// </summary>
    public class UnitEventSender : BaseEventSender
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
