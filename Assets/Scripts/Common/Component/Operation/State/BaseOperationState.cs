using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AloneWar.Common.Component.Operation.State
{
    /// <summary>
    /// 操作状態の基本クラス
    /// </summary>
    public abstract class BaseOperationState
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        public abstract void SetState(GameObject gameObject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="arg"></param>
        public void InvokeOperation(Action<GameObject> callback, GameObject arg)
        {
            if (callback != null)
            {
                callback(arg);
            }
        }
    }
}
