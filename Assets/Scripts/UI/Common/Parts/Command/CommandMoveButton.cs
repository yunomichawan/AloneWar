using AloneWar.Common;
using AloneWar.Common.Extensions;
using AloneWar.Stage.Component;
using AloneWar.Stage.Controller;
using AloneWar.Unit.Component;
using UnityEngine;

namespace AloneWar.UI.Common.Parts.Command
{
    public class CommandMoveButton : BaseCommandButton
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
                MassComponent massComponent = obj.GetComponent<MassComponent>();
                if (massComponent != null)
                {
                    unit.UnitCommandController.UnitRoot.SetNewRoot(massComponent.PositionId);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">mouse over object</param>
        public override void MouseOver(GameObject obj)
        {
            if (obj != null)
            {
                BaseStageObject baseStageObject = obj.GetComponent<BaseStageObject>();
                EditorDebug.DebugAlert("検証：継承元のクラスでGetComponentできるか？");
                if (baseStageObject != null)
                {
                    StageUserOperation.Instance.SelectUnit.UnitCommandController.UnitRoot.AddRoot(baseStageObject.PositionId);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        public override void OperationInitBefore(UnitBaseComponent unit)
        {
            unit.StageRange.SetRange(CommandCategory.Move, unit.SubCommand, unit.UnitBaseStatus.BaseStatus.Move, unit.SubRange, 0, unit.InvalidRange);
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
