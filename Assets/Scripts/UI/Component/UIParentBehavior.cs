using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using AloneWar.UI.Common;
using AloneWar.Common;

namespace AloneWar.UI.Component
{
    public abstract class UIParentBehavior : MonoBehaviour
    {
        public RectTransform rectTransform;

        public virtual void Awake()
        {
            this.SetPosition();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetPosition()
        {
            // IChildPositionHandlerを継承している場合
            if (this is IChildPositionHandler)
            {
                IChildPositionHandler childPositionHandler = (IChildPositionHandler)this;
                this.SetChildrenPosition(childPositionHandler);
                rectTransform.anchoredPosition = this.GetPositionFromDisplay(childPositionHandler.UiPivot);
            }
        }

        /// <summary>
        /// 未実装
        /// </summary>
        /// <param name="pivot"></param>
        /// <returns></returns>
        private Vector3 GetPositionFromDisplay(UIPivot pivot)
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
                    position.x = width / 2;
                    position.y = height / 2;
                    break;
                case UIPivot.Right:
                    position.x = width / 2;
                    break;
                case UIPivot.BottomRight:
                    position.x = width / 2;
                    position.y = height / 2 * -1;
                    break;
                case UIPivot.Bottom:
                    position.y = height / 2 * -1;
                    break;
                case UIPivot.BottomLeft:
                    position.x = width / 2 * -1;
                    position.y = height / 2 * -1;
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
        /// 
        /// </summary>
        /// <param name="childPositionHandler"></param>
        private void SetChildrenPosition(IChildPositionHandler childPositionHandler)
        {
            Vector2 position = Vector2.zero;
            Func<Vector2, Vector2, Vector2> getPosition = this.GetPositionFromDirection(childPositionHandler);
            int index = 0;
            RectTransform beforeRectTransform = null;
            foreach (RectTransform tran in transform)
            {
                tran.pivot = new Vector2(0, 1);
                if (index > 0)
                {
                    position = getPosition(position, beforeRectTransform.sizeDelta);
                }
                Debug.Log(position.x.ToString() + "," + position.y.ToString());
                tran.anchoredPosition = position;
                beforeRectTransform = tran;
                index++;
            }
        }

        private Func<Vector2, Vector2, Vector2> GetPositionFromDirection(IChildPositionHandler childPositionHandler)
        {
            Vector2 margin = childPositionHandler.Margin;
            switch (childPositionHandler.UiDirection)
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

