using AloneWar.Stage;
using AloneWar.Stage.Controller;
using AloneWar.Unit.Component;
using UnityEngine;

namespace AloneWar.UI.Common.Parts.Command
{
    public class CommandTurnEndButton : BaseCommandButton
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        public override void OperationInitBefore(UnitBaseComponent unit)
        {
            StageManager.Instance.TaskQueue.Enqueue(unit.UnitCommandController.WaitTask);
            StageUserOperation.Instance.SetFreeCommand();
        }

        public override void RightClick(GameObject obj, UnitBaseComponent unit)
        {
        }
    }
}
