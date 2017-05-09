using AloneWar.Common;
using AloneWar.Common.Component.Operation;
using AloneWar.Common.Extensions;
using AloneWar.Stage.Component;
using AloneWar.Stage.Controller.Unit;
using AloneWar.UI.Common.Parts.Command;
using AloneWar.Unit.Component;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AloneWar.Stage.Controller
{
    public class StageUserOperation : MonoBehaviour, IUserOperation
    {
        public static StageUserOperation Instance
        {
            get
            {
                return StageManager.Instance.stageUserOperation;
            }
        }

        #region property

        /// <summary>
        /// 選択したユニット
        /// </summary>
        public UnitBaseComponent SelectUnit { get; set; }
        
        /// <summary>
        /// 一時的に選択したオブジェクトを保持(マウスオーバー時等々)
        /// </summary>
        public TempStageObject TempStageObject { get { return this.tempStageObject; } set { this.tempStageObject = value; } }
        private TempStageObject tempStageObject = new TempStageObject();

        public Action<GameObject, UnitBaseComponent> LeftClick { get; set; }
        public Action<GameObject, UnitBaseComponent> RightClick { get; set; }
        public Action<GameObject, UnitBaseComponent> MiddleClick { get; set; }
        public Action<GameObject> MouseOver { get; set; }
        public Action<BaseStageObject> MouseOut { get; set; }

        public Action<GameObject, UnitBaseComponent> BeganTouch { get; set; }
        public Action<GameObject, UnitBaseComponent> CanceledTouch { get; set; }
        public Action<GameObject, UnitBaseComponent> EndedTouch { get; set; }
        public Action<GameObject, UnitBaseComponent> MovedTouch { get; set; }
        public Action<GameObject, UnitBaseComponent> StationaryTouch { get; set; }

        /// <summary>
        /// 履歴
        /// </summary>
        public Stack<UnitCommandHistory> UnitCommandHistoryStack { get { return this.unitCommandHistoryStack; } set { this.unitCommandHistoryStack = value; } }
        private Stack<UnitCommandHistory> unitCommandHistoryStack = new Stack<UnitCommandHistory>();

        #endregion

        #region Init

        void Awake()
        {
            this.InitOperation();
        }

        /// <summary>
        /// 操作周りを初期化
        /// </summary>
        private void InitOperation()
        {
            OperationComponent.Instance.Operation.SetAction(this);
            this.TempStageObject.ClearAction = this.MouseOut;
        }

        /// <summary>
        /// コマンドにより操作処理を初期化
        /// </summary>
        /// <param name="commandButton"></param>
        public void InitFromCommand(BaseCommandButton commandButton, bool isBack)
        {
            commandButton.OperationInitBefore(this.SelectUnit);
            StageUserOperation.Instance.LeftClick = commandButton.LeftClick;
            StageUserOperation.Instance.RightClick = commandButton.RightClick;
            StageUserOperation.Instance.MouseOver = commandButton.MouseOver;
            StageUserOperation.Instance.MouseOut = commandButton.MouseOut;
            commandButton.OperationInitAfter(this.SelectUnit);

            if (!isBack)
            {
                this.UnitCommandHistoryStack.Push(new UnitCommandHistory(commandButton));
            }
        }

        #endregion

        #region IUserOperation property

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObject"></param>
        public void LeftClickAction(GameObject clickObject)
        {
            this.LeftClick(clickObject, this.SelectUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObject"></param>
        public void RightClickAction(GameObject clickObject)
        {
            this.RightClick(clickObject, this.SelectUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObject"></param>
        public void MiddleClickAction(GameObject clickObject)
        {
            this.MiddleClick(clickObject, this.SelectUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="overObject"></param>
        public void MouseOverAction(GameObject overObject)
        {
            if (overObject != null)
            {
                BaseStageObject stageObject = overObject.GetComponent<BaseStageObject>();
                if (stageObject != null)
                {
                    this.TempStageObject.Add(stageObject);
                }
            }
            else
            {
                this.TempStageObject.Clear();
            }

            this.MouseOver(overObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObject"></param>
        public void BeganTouchAction(GameObject clickObject)
        {
            this.BeganTouch(clickObject, this.SelectUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObject"></param>
        public void CanceledTouchAction(GameObject clickObject)
        {
            this.CanceledTouch(clickObject, this.SelectUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObject"></param>
        public void EndedTouchAction(GameObject clickObject)
        {
            this.EndedTouch(clickObject, this.SelectUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObject"></param>
        public void MovedTouchAction(GameObject clickObject)
        {
            this.MovedTouch(clickObject, this.SelectUnit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickObject"></param>
        public void StationaryTouchAction(GameObject clickObject)
        {
            this.StationaryTouch(clickObject, this.SelectUnit);
        }

        #endregion

        /// <summary>
        /// Back(戻る)
        /// </summary>
        public void BackCommand()
        {
            if (this.UnitCommandHistoryStack.Count > 0)
            {
                UnitCommandHistory unitCommandHistory = this.UnitCommandHistoryStack.Pop();
                this.InitFromCommand(unitCommandHistory.CommandButton, true);
            }
        }

        #region Unit

        /// <summary>
        /// 選択ユニット設定
        /// </summary>
        /// <param name="selectUnit"></param>
        public void SetUnit(UnitBaseComponent selectUnit)
        {
            this.SelectUnit = selectUnit;
            EditorDebug.DebugAlert("Command Menu");
        }

        /// <summary>
        /// 選択状態解除
        /// </summary>
        public void UnSelectUnit()
        {
            this.SelectUnit = null;
        }

        #endregion

        #region Free

        /// <summary>
        /// フリー操作時の処理を設定
        /// </summary>
        public void SetFreeCommand()
        {
            this.LeftClick = this.FreeLeftClick;
            this.RightClick = this.FreeRightClick;
            this.MouseOver = this.FreeMouseOver;
            this.MouseOut = this.FreeMouseOut;
        }

        /// <summary>
        /// フリー操作時の左クリック処理を実装
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="unitBaseComponent"></param>
        private void FreeLeftClick(GameObject obj, UnitBaseComponent unitBaseComponent)
        {
            this.TargetObjectAction(obj, b =>
            {
                if (b.GameObjectCategory.Equals(GameObjectCategory.Unit))
                {
                    UnitBaseComponent unit = (UnitBaseComponent)b;
                    if (unit.UnitSide.Equals(UnitSideCategory.Player))
                    {
                        this.SetUnit(unit);
                    }
                }
                else if (b.GameObjectCategory.Equals(GameObjectCategory.Mass))
                {

                }
            }, null);
        }

        /// <summary>
        /// フリー操作時の右クリック処理を実装
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="unitBaseComponent"></param>
        private void FreeRightClick(GameObject obj, UnitBaseComponent unitBaseComponent)
        {
            if (obj != null)
            {

            }
            else
            {
                // display main menu

            }
        }

        /// <summary>
        /// フリー操作時のマウスオーバー処理を実装
        /// </summary>
        /// <param name="obj"></param>
        private void FreeMouseOver(GameObject obj)
        {
            if (obj != null)
            {
                this.TargetObjectAction(obj, b =>
                {

                }, () => { });
            }
        }

        /// <summary>
        /// フリー操作時のマウスアウト処理
        /// </summary>
        /// <param name="stageObject"></param>
        private void FreeMouseOut(BaseStageObject stageObject)
        {
            if (stageObject.GameObjectCategory.Equals(GameObjectCategory.Mass))
            {
                MassComponent mass = (MassComponent)stageObject;
                mass.SetMaterialColor(Color.clear);
            }
            else if (stageObject.GameObjectCategory.Equals(GameObjectCategory.Unit))
            {
                UnitBaseComponent unit = (UnitBaseComponent)stageObject;
                unit.StageRange.SetRangeColor(true);
            }
        }

        /// <summary>
        /// クリックしたオブジェクトに対して処理を行う
        /// </summary>
        /// <param name="target"></param>
        /// <param name="clickCallback"></param>
        /// <param name="noneObjectCallback"></param>
        private void TargetObjectAction(GameObject target, Action<BaseStageObject> clickCallback,Action noneObjectCallback)
        {
            if(target != null)
            {
                BaseStageObject baseStageObject = target.GetComponent<BaseStageObject>();
                if (baseStageObject != null)
                {
                    clickCallback(baseStageObject);
                }
            }
            else
            {
                noneObjectCallback.SafeCall();
            }
        }

        #endregion

    }

    /// <summary>
    /// マウスオーバー時等の一時オブジェクトを管理
    /// </summary>
    public class TempStageObject
    {
        /// <summary>
        /// 一時オブジェクトリスト
        /// </summary>
        public List<BaseStageObject> TempStageObjectList { get { return this.tempStageObjectList; } set { this.tempStageObjectList = value; } }
        private List<BaseStageObject> tempStageObjectList = new List<BaseStageObject>();

        /// <summary>
        /// クリア前に行うアクション
        /// </summary>
        public Action<BaseStageObject> ClearAction { get; set; }

        /// <summary>
        /// 一時オブジェクトを追加
        /// </summary>
        /// <param name="stageObject"></param>
        public void Add(BaseStageObject stageObject)
        {
            UnitSummaryComponent summary = StageManager.Instance.StageInformation.SearchUnit(stageObject.PositionId);
            this.Clear();
            MassComponent mass = StageManager.Instance.StageInformation.MassComponentList[stageObject.PositionId];
            summary.ForEach(u => this.TempStageObjectList.Add(u));
            this.TempStageObjectList.Add(mass);
        }

        /// <summary>
        /// 一時オブジェクトをクリア
        /// </summary>
        public void Clear()
        {
            if (this.ClearAction != null)
            {
                this.TempStageObjectList.ForEach(b => this.ClearAction(b));
            }
            this.TempStageObjectList.Clear();
        }
    }

}
