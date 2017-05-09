using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AloneWar.Common.Component.Operation
{
    /// <summary>
    /// 操作用のインターフェースを実装
    /// </summary>
    public interface IUserOperation
    {
        #region Mouse

        void LeftClickAction(GameObject clickObject);

        void RightClickAction(GameObject clickObject);

        void MiddleClickAction(GameObject clickObject);

        void MouseOverAction(GameObject overObject);

        #endregion

        #region Touch

        void BeganTouchAction(GameObject clickObject);

        void CanceledTouchAction(GameObject clickObject);

        void EndedTouchAction(GameObject clickObject);

        void MovedTouchAction(GameObject clickObject);

        void StationaryTouchAction(GameObject clickObject);

        #endregion
    }
}
