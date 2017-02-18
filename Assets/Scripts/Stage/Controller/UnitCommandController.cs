using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Stage.Component;
using AloneWar.Stage.FieldObject;
using AloneWar.Unit.Status;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.UI;
using UnityEngine;
using AloneWar.Stage.Event.EventObject;
using AloneWar.UI.Stage;
using AloneWar.Unit.Component;

namespace AloneWar.Stage.Controller
{
    /// <summary>
    /// ユニット単位のコントローラ
    /// </summary>
    public class UnitCommandController<T> where T : SqliteBaseData
    {
        #region property

        #region event

        /// <summary>
        /// 汎用移動イベント
        /// </summary>
        public List<PositionEvent> MoveEvent { get { return this.moveEvent; } set { this.moveEvent = value; } }
        private List<PositionEvent> moveEvent = new List<PositionEvent>();

        /// <summary>
        /// 
        /// </summary>
        public List<UnitEvent> KillEvent { get { return this.killEvent; } set { this.killEvent = value; } }
        private List<UnitEvent> killEvent = new List<UnitEvent>();

        #endregion

        /// <summary>
        /// 履歴
        /// </summary>
        public Stack<UnitCommandHistory> UnitCommandHistoryStack { get { return this.unitCommandHistoryStack; } set { this.unitCommandHistoryStack = value; } }
        private Stack<UnitCommandHistory> unitCommandHistoryStack = new Stack<UnitCommandHistory>();

        private UnitBaseComponent<T> UnitBaseComponent { get; set; }


        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unitBaseStatus"></param>
        /// <param name="monoBehaviour"></param>
        public UnitCommandController(UnitBaseComponent<T> unitBaseComponent)
        {
            this.UnitBaseComponent = unitBaseComponent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionId"></param>
        public void UpdatePosition(int positionId)
        {
            this.UnitBaseComponent.UnitObjectStatus.StageStatus.BeforePositionId = this.UnitBaseComponent.UnitObjectStatus.StageStatus.PositionId;
            this.UnitBaseComponent.UnitObjectStatus.StageStatus.PositionId = positionId;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveUnit()
        {
            // トリガーチェック
            this.KillEvent.ForEach(k =>
            {
                if (k.VaildFlg)
                {
                    // イベント実行後に行うコールバックを設定
                    k.EventAfterCallback = () =>
                    {
                        int positionId = this.UnitBaseComponent.UnitObjectStatus.StageStatus.PositionId;
                        // 内部データより削除
                        StageManager.Instance.StageInformation.UnitSubComponentList.Remove(positionId);
                        // 
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
        public void VaildPositionEvent()
        {
            this.MoveEvent.ForEach(m =>
            {
                bool vaild = m.SetVaildEvent(this.UnitBaseComponent.UnitObjectStatus.StageStatus.PositionId);
                // イベントが有効だった場合は待機に移行
                if (vaild)
                {
                    this.Wait();
                }
            });
        }

        #region Command

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toPositionId"></param>
        public void Move(int toPositionId)
        {
            this.UnitCommandHistoryStack.Push(new UnitCommandHistory(CommandCategory.Move));
            this.UpdatePosition(toPositionId);
            StageManager.Instance.TaskQueue.Enqueue(this.MoveTask);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerator MoveTask()
        {
            // MoveRoot

            while (true)
            {
                yield return new WaitForEndOfFrame();
                break;
            }

            // イベントチェック
            this.VaildPositionEvent();

            StageManager.Instance.UnitRangeInit(this.UnitBaseComponent.UnitObjectStatus.StageStatus.BeforePositionId);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Wait()
        {
            this.unitCommandHistoryStack.Clear();
        }

        /// <summary>
        /// 戻る
        /// </summary>
        public void Back()
        {
            UnitCommandHistory unitCommandHistory = this.UnitCommandHistoryStack.Pop();
            UIUnitCommand.Instance.SetCommand(unitCommandHistory);
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class UnitRange<T> where T : SqliteBaseData
    {
        #region property

        private UnitBaseComponent<T> UnitBaseComponent { get; set; }

        private int MainRange { get; set; }

        private int SubRange { get; set; }

        private CommandCategory MainCommandCategory { get; set; }

        private CommandCategory SubCommandCategory { get; set; }

        /// <summary>
        /// key is positionId
        /// </summary>
        public Dictionary<int, RangeCommand> MainRangeCommandList { get { return this.mainCommandList; } set { this.mainCommandList = value; } }
        private Dictionary<int, RangeCommand> mainCommandList = new Dictionary<int, RangeCommand>();

        /// <summary>
        /// key is positionId
        /// </summary>
        public Dictionary<int, RangeCommand> SubRangeCommandList { get { return this.subCommandList; } set { this.subCommandList = value; } }
        private Dictionary<int, RangeCommand> subCommandList = new Dictionary<int, RangeCommand>();

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="unitBaseStatus"></param>
        /// <param name="monoBehaviour"></param>
        public UnitRange(UnitBaseComponent<T> unitBaseComponent)
        {
            this.UnitBaseComponent = unitBaseComponent;
            this.InitRange();
        }

        #region Range

        /// <summary>
        /// 空初期化
        /// </summary>
        public void InitRange()
        {
            this.MainRange = 0;
            this.SubRange = 0;
            this.MainCommandCategory = CommandCategory.None;
            this.SubCommandCategory = CommandCategory.None;
            this.MainRangeCommandList.Clear();
            this.SubRangeCommandList.Clear(); ;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void InitRange(int mainRange, int subRange, CommandCategory mainCommand, CommandCategory subCommand)
        {
            this.MainRange = mainRange;
            this.SubRange = subRange;
            this.MainCommandCategory = CommandCategory.None;
            this.SubCommandCategory = CommandCategory.None;
            this.MainRangeCommandList.Clear();
            this.SubRangeCommandList.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetRange(int mainRange, int subRange, CommandCategory mainCommand, CommandCategory subCommand)
        {
            this.InitRange(mainRange, subRange, mainCommand, subCommand);
            int positionId = this.UnitBaseComponent.UnitObjectStatus.StageStatus.PositionId;

            RangeCommand massRange = new RangeCommand(positionId, this.MainRange, this.UnitBaseComponent.UnitObjectStatus.StageStatus.UnitSide);
            List<RangeCommand> subRangeCommandList = new List<RangeCommand>();
            this.MainRangeCommandList.Add(positionId, massRange);
            this.SetRangeLoop(new List<RangeCommand>(new RangeCommand[] { massRange }), subRangeCommandList);
            this.SetRangeLoop(subRangeCommandList, new List<RangeCommand>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainRangeCommandList"></param>
        private void SetRangeLoop(List<RangeCommand> mainRangeCommandList, List<RangeCommand> subRangeCommandList)
        {
            List<RangeCommand> rangeCommandNext = new List<RangeCommand>();
            int w = StageManager.Instance.StageInformation.StageTableData.Width;
            mainRangeCommandList.ForEach(m =>
            {
                MassStatus massStatus = m.MassComponent.MassStatus;
                // top
                RangeCommand upRange = new RangeCommand(m, massStatus.PositionId - w, RangeDirection.Top);
                this.AddRangeIfVaild(upRange, mainRangeCommandList, subRangeCommandList);
                // bottom
                RangeCommand bottomRange = new RangeCommand(m, massStatus.PositionId + w, RangeDirection.Bottom);
                this.AddRangeIfVaild(bottomRange, mainRangeCommandList, subRangeCommandList);
                // right
                RangeCommand rightRange = new RangeCommand(m, massStatus.PositionId + 1, RangeDirection.Right);
                this.AddRangeIfVaild(rightRange, mainRangeCommandList, subRangeCommandList);
                // left
                RangeCommand leftRange = new RangeCommand(m, massStatus.PositionId - 1, RangeDirection.Left);
                this.AddRangeIfVaild(rightRange, mainRangeCommandList, subRangeCommandList);
            });
            // 再帰処理
            this.SetRangeLoop(rangeCommandNext, subRangeCommandList);
        }

        /// <summary>
        /// 対象マスを0範囲に追加
        /// 有効でない場合は1範囲に追加
        /// </summary>
        /// <param name="rangeCommand"></param>
        /// <param name="rangeCommandList0"></param>
        /// <param name="rangeCommandList1"></param>
        private void AddRangeIfVaild(RangeCommand rangeCommand, List<RangeCommand> rangeCommandList0, List<RangeCommand> rangeCommandList1)
        {
            if (!this.MainRangeCommandList.ContainsKey(rangeCommand.MassComponent.PositionId) && !this.SubRangeCommandList.ContainsKey(rangeCommand.MassComponent.PositionId))
            {
                if (rangeCommand.IsVaildRange && rangeCommand.IsVaildRangeUnit)
                {
                    this.MainRangeCommandList.Add(rangeCommand.MassComponent.PositionId, rangeCommand);
                    rangeCommandList0.Add(rangeCommand);
                }
                else
                {
                    RangeCommand subRange = new RangeCommand(rangeCommand, rangeCommand.MassComponent.PositionId, rangeCommand.DirectionArray[rangeCommand.DirectionArray.Length - 1], this.SubCommandCategory);
                    subRange.Range = this.SubRange;
                    this.SubRangeCommandList.Add(subRange.MassComponent.PositionId, subRange);
                    rangeCommandList1.Add(subRange);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetRangeColor()
        {
            this.MainRangeCommandList.ToList().ForEach(pair =>
            {
                RangeCommand rangeCommand = this.MainRangeCommandList[pair.Key];
                MassComponent massComponent = rangeCommand.MassComponent;
                //rangeCommand.CommandCategory より色を決定
                massComponent.GetComponent<SpriteRenderer>().color = new UnityEngine.Color();
            });

            this.SubRangeCommandList.ToList().ForEach(pair =>
            {
                RangeCommand rangeCommand = this.SubRangeCommandList[pair.Key];
                MassComponent massComponent = rangeCommand.MassComponent;
                //rangeCommand.CommandCategory より色を決定
                massComponent.GetComponent<SpriteRenderer>().color = new UnityEngine.Color();
            });
        }

        /// <summary>
        /// 対象座標を範囲に含んでいた場合、範囲を再設定する
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public void ResetRange(int positionId)
        {
            if (this.MainRangeCommandList.ContainsKey(positionId))
            {
                this.SetRange(this.UnitBaseComponent.MainRange, this.UnitBaseComponent.SubRange, this.UnitBaseComponent.UnitObjectStatus.MainCommand, this.UnitBaseComponent.UnitObjectStatus.SubCommand);
            }
        }

        #endregion

        public UnitSummaryComponent SearchRange()
        {
            UnitSummaryComponent unitSummaryComponent = new UnitSummaryComponent();
            List<int> mainKeys = this.MainRangeCommandList.Keys.ToList();
            List<int> subKeys = this.SubRangeCommandList.Keys.ToList();
            mainKeys.AddRange(subKeys);
            
            return StageManager.Instance.StageInformation.SearchUnitComponent(mainKeys);
        }
    }
}
