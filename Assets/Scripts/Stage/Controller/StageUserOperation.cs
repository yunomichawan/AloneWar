using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common.Component.Operation;
using UnityEngine;

namespace AloneWar.Stage.Controller
{
    public class StageUserOperation : IUserOperation
    {
        /// <summary>
        /// 操作周りを初期化
        /// </summary>
        public void InitOperation()
        {
            OperationComponent.Instance.Operation.SetAction(this);
        }

        public void LeftClickAction(GameObject clickObject)
        {

        }

        public void RightClickAction(GameObject clickObject)
        {

        }

        public void MiddleClickAction(GameObject clickObject)
        {

        }

        public void BeganTouchAction(GameObject clickObject)
        {

        }

        public void CanceledTouchAction(GameObject clickObject)
        {
        }

        public void EndedTouchAction(GameObject clickObject)
        {
        }

        public void MovedTouchAction(GameObject clickObject)
        {
        }

        public void StationaryTouchAction(GameObject clickObject)
        {
        }
    }
}
