using AloneWar.UI.Common;
using UnityEngine;

namespace AloneWar.UI.Component
{
    /// <summary>
    /// 子要素をくくるクラスが継承すること
    /// </summary>
    public abstract class UIParentBehavior : MonoBehaviour
    {
        #region inspector proprty

        public RectTransform rectTransform;

        #endregion

        public virtual void Awake()
        {
            this.SetPosition();
        }

        /// <summary>
        /// 座標設定
        /// virtaulもしくはabstractで実装すべき
        /// </summary>
        private void SetPosition()
        {
            // IChildPositionHandlerを継承している場合
            if (this is IChildPositionHandler)
            {
                IChildPositionHandler childPositionHandler = (IChildPositionHandler)this;
                UIPositionHandler uiPositionHandler = new UIPositionHandler(this.rectTransform, childPositionHandler);
                uiPositionHandler.SetChildrenPosition();
                rectTransform.anchoredPosition = uiPositionHandler.GetPositionFromDisplay(childPositionHandler.UiPivot);
            }
        }
    }
}

