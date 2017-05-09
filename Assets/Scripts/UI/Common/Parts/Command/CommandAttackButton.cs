using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.Unit.Component;
using UnityEngine;

namespace AloneWar.UI.Common.Parts.Command
{
    public class CommandAttackButton : BaseCommandButton
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">click object</param>
        /// <param name="unit">select unit</param>
        public override void LeftClick(GameObject obj, UnitBaseComponent unit)
        {
            if (obj != null)
            {
                EditorDebug.DebugAlert("target attack");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">mouse over object</param>
        public override void MouseOver(GameObject obj)
        {
           
        }

        public override void OperationInitBefore(UnitBaseComponent unit)
        {
            unit.StageRange.SetRange(CommandCategory.Attack, CommandCategory.None, 0, unit.UnitBaseStatus.BaseStatus.Range, 0, unit.InvalidRange);
            unit.StageRange.SetRangeColor(false);
        }

        public override void RightClick(GameObject obj, UnitBaseComponent unit)
        {
            unit.UnitCommandController.UnitRoot.ClearRoot();
            unit.StageRange.SetRangeColor(true);
            base.RightClick(obj, unit);
        }
    }
}
