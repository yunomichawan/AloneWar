using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common.Component.Operation;

namespace AloneWar.Common.Component.Operation.State
{
    /// <summary>
    /// 操作状態クラス
    /// </summary>
    public class OperationStateInfo
    {
        public bool Enabled { get; set; }
        public TouchState TouchState { get; set; }
        public MouseState MouseState { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OperationStateInfo()
        {
            this.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userOperation"></param>
        public void SetAction(IUserOperation userOperation)
        {
            // Mouse
            this.MouseState.LeftCallback = userOperation.LeftClickAction;
            this.MouseState.RightCallback= userOperation.RightClickAction;
            this.MouseState.MiddleCallback = userOperation.MiddleClickAction;

            // Touch
            this.TouchState.BeganCallback = userOperation.BeganTouchAction;
            this.TouchState.CanceledCallback = userOperation.CanceledTouchAction;
            this.TouchState.EndedCallback= userOperation.EndedTouchAction;
            this.TouchState.MovedCallback= userOperation.MovedTouchAction;
            this.TouchState.StationaryCallback= userOperation.StationaryTouchAction;
        }
    }
}
