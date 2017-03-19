using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common.Extensions;
using UnityEngine;

namespace AloneWar.Common.Component.Operation.State
{
    /// <summary>
    /// 
    /// </summary>
    public class MouseState : BaseOperationState
    {
        /*
         *リストでコールバックを持たせるべき？
         */

        /// <summary>
        /// 
        /// </summary>
        public Action<GameObject> LeftCallback { get; set; }
        public Action<GameObject> RightCallback { get; set; }
        public Action<GameObject> MiddleCallback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        public override void SetState(GameObject gameObject)
        {
            if (Input.GetMouseButtonUp(0))
            {
                this.InvokeOperation(this.LeftCallback, gameObject);
            }
            if (Input.GetMouseButtonUp(1))
            {
                this.InvokeOperation(this.RightCallback, gameObject);
            }
            if (Input.GetMouseButtonUp(2))
            {
                this.InvokeOperation(this.MiddleCallback, gameObject);
            }
        }
    }
}
