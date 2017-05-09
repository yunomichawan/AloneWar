using AloneWar.Common;
using System;
using UnityEngine;

namespace AloneWar.UI.Common
{
    public class UIObjectManager
    {

        
    }

    /// <summary>
    /// UI座標を設定するクラス
    /// </summary>
    public class UIPositionHandler
    {
        #region property

        private RectTransform RectTransform { get; set; }

        private IChildPositionHandler ChildPositionHandler { get; set; }

        private float ChildHeightSum { get; set; }

        private float ChildWidthSum { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="childPositionHandler"></param>
        public UIPositionHandler(RectTransform rectTransform, IChildPositionHandler childPositionHandler)
        {
            this.RectTransform = rectTransform;
            this.ChildPositionHandler = childPositionHandler;
            this.ChildHeightSum = 0;
            this.ChildWidthSum = 0;
        }

        /// <summary>
        /// 方角により配置する座標を取得
        /// </summary>
        /// <param name="pivot"></param>
        /// <returns></returns>
        public Vector3 GetPositionFromDisplay(UIPivot pivot)
        {
            float width = AloneWarConst.DisplaySize.X;
            float height = AloneWarConst.DisplaySize.Y;
            Vector3 position = Vector3.zero;
            switch (pivot)
            {
                case UIPivot.Top:
                    position.y = height / 2;
                    break;
                case UIPivot.TopRight:
                    position.x = width / 2 - this.ChildWidthSum;
                    position.y = height / 2;
                    break;
                case UIPivot.Right:
                    position.x = width / 2 - this.ChildWidthSum;
                    break;
                case UIPivot.BottomRight:
                    position.x = width / 2 - this.ChildWidthSum;
                    position.y = height / 2 * -1 - this.ChildHeightSum;
                    break;
                case UIPivot.Bottom:
                    position.y = height / 2 * -1 - this.ChildHeightSum; ;
                    break;
                case UIPivot.BottomLeft:
                    position.x = width / 2 * -1;
                    position.y = height / 2 * -1 - this.ChildHeightSum; ;
                    break;
                case UIPivot.Left:
                    position.x = width / 2 * -1;
                    break;
                case UIPivot.TopLeft:
                    position.x = width / 2 * -1;
                    position.y = height / 2;
                    break;
                case UIPivot.Center:
                case UIPivot.None:
                    break;
            }

            return position;
        }

        /// <summary>
        /// 子要素の座標を設定
        /// </summary>
        public void SetChildrenPosition()
        {
            Vector2 position = Vector2.zero;
            Func<Vector2, Vector2, Vector2> getPosition = this.GetPositionFromDirection();
            int index = 0;
            RectTransform beforeRectTransform = null;
            foreach (RectTransform tran in this.RectTransform)
            {
                tran.pivot = new Vector2(0, 1);
                if (index > 0)
                {
                    position = getPosition(position, beforeRectTransform.sizeDelta);
                }
                Debug.Log(position.x.ToString() + "," + position.y.ToString());
                this.ChildHeightSum += tran.sizeDelta.y + this.ChildPositionHandler.Margin.y;
                this.ChildWidthSum += tran.sizeDelta.x + this.ChildPositionHandler.Margin.x;
                tran.anchoredPosition = position;
                beforeRectTransform = tran;
                index++;
            }
        }

        /// <summary>
        /// 向きによる計算式を取得
        /// </summary>
        /// <returns></returns>
        private Func<Vector2, Vector2, Vector2> GetPositionFromDirection()
        {
            Vector2 margin = this.ChildPositionHandler.Margin;
            switch (this.ChildPositionHandler.UiDirection)
            {
                case UIDirection.Horizontal:
                    return (pos, size) =>
                    {
                        return new Vector2(pos.x + size.x + margin.x, 0);
                    };
                case UIDirection.Vertical:
                    return (pos, size) =>
                    {
                        return new Vector2(0, (pos.y + size.y + margin.y) * -1);
                    };
                default:
                    return null;
            }
        }
    }
}
