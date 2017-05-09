using AloneWar.Common;
using AloneWar.UI.Component;
using UnityEngine;

namespace AloneWar.UI.Common.ListMenu
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TList"></typeparam>
    public abstract class BaseListMenu : UIParentBehavior, IChildPositionHandler
    {

        /// <summary>
        /// 
        /// </summary>
        public Vector2 Margin
        {
            get
            {
                return this.margin;
            }
            set
            {
                this.margin = value;
            }
        }
        public Vector2 margin;

        public abstract UIDirection UiDirection { get; }

        public abstract UIPivot UiPivot { get; }
    }
}
