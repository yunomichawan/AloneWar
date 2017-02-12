using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Unit.Status;
using AloneWar.Stage;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Stage.Component;
using AloneWar.Stage.Event.EventObject;

namespace AloneWar.Unit.Component
{
    [Serializable]
    public abstract class UnitBaseComponent<T> : BaseStageObject
        where T : SqliteBaseData
    {

        /// <summary>
        /// ユニット移動イベント
        /// </summary>
        public List<PositionEvent> MoveEvent { get { return this.moveEvent; } set { this.moveEvent = value; } }
        private List<PositionEvent> moveEvent = new List<PositionEvent>();

        public override int StageObjectId
        {
            get { return this.UnitObjectStatus.StageStatus.Id; }
        }

        public override int PositionId
        {
            get
            {
                return this.UnitObjectStatus.StageStatus.PositionId;
            }
            set
            {
                this.UnitObjectStatus.StageStatus.PositionId = value;
                this.MoveEvent.ForEach(p => {
                    if (p.VaildFlg)
                    {
                        // 特定座標イベント
                        if (p.PositionEventSender.PositionIdArray.Contains(this.PositionId))
                        {
                            StageManager.Instance.stageEventBuilder.TaskQueue.Enqueue(p.EventTask);
                        }
                        // 特定エリアイベント
                        else if (p.PositionEventSender.AreaArray.Contains(this.Area))
                        {

                        }
                    }
                });
            }
        }

        public override string Area
        {
            get { return StageManager.Instance.StageInformation.GetPositionArea(this.PositionId); }
        }

        public UnitObjectStatus<T> UnitObjectStatus { get; set; }

    }
}