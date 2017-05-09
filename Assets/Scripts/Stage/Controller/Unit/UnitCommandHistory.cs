using AloneWar.UI.Common.Parts.Command;

namespace AloneWar.Stage.Controller.Unit
{
    public class UnitCommandHistory
    {
        public BaseCommandButton CommandButton { get; set; }

        public UnitCommandHistory(BaseCommandButton commandButton)
        {
            this.CommandButton = commandButton;
        }
    }
}
