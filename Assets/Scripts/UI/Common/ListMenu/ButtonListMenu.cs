using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common;
using UnityEngine;

namespace AloneWar.UI.Common.ListMenu
{
    public class ButtonListMenu : BaseListMenu
    {
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
    }
}
