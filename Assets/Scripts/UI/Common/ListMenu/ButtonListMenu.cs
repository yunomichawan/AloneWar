using AloneWar.Common;
using UnityEngine;

namespace AloneWar.UI.Common.ListMenu
{
    public abstract class ButtonListMenu : BaseListMenu
    {
        #region inspector property

        public override UIDirection UiDirection
        {
            get
            {
                return this.uiDirection;
            }
        }
        [SerializeField]
        private UIDirection uiDirection;

        public override UIPivot UiPivot
        {
            get
            {
                return this.uiPivot;
            }
        }
        [SerializeField]
        private UIPivot uiPivot;

        #endregion

        public abstract void SetButtonsEvent();

    }
}
