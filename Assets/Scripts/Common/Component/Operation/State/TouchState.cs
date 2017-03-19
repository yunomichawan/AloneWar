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
    public class TouchState : BaseOperationState
    {
        public Touch Touch { get; set; }

        public Action<GameObject> BeganCallback { get; set; }
        public Action<GameObject> CanceledCallback { get; set; }
        public Action<GameObject> EndedCallback { get; set; }
        public Action<GameObject> MovedCallback { get; set; }
        public Action<GameObject> StationaryCallback { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        public override void SetState(GameObject gameObject)
        {
            this.Touch = Input.GetTouch(0);
            switch (this.Touch.phase)
            {
                case TouchPhase.Began:
                    this.InvokeOperation(this.BeganCallback, gameObject);
                    break;
                case TouchPhase.Canceled:
                    this.InvokeOperation(this.CanceledCallback, gameObject);
                    break;
                case TouchPhase.Ended:
                    this.InvokeOperation(this.EndedCallback, gameObject);
                    break;
                case TouchPhase.Moved:
                    this.InvokeOperation(this.MovedCallback, gameObject);
                    break;
                case TouchPhase.Stationary:
                    this.InvokeOperation(this.StationaryCallback, gameObject);
                    break;
            }
        }
    }
}
