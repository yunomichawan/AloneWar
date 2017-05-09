using AloneWar.Stage.Controller;
using AloneWar.UI.Common.ListMenu;
using AloneWar.UI.Common.Parts.Command;
using UnityEngine;

namespace AloneWar.UI.Stage
{
    /// <summary>
    /// ユニットに関連するボタンイベントを設定する
    /// </summary>
    public class UIUnitCommandButton : ButtonListMenu
    {
        [SerializeField]
        private BaseCommandButton[] commandButtons;

        public override void SetButtonsEvent()
        {
            foreach (BaseCommandButton commandButton in this.commandButtons)
            {
                this.SetCommandButtonEvent(commandButton);
            }
        }

        /// <summary>
        /// ボタンにクリックイベント等を設定する
        /// </summary>
        /// <param name="commandButton"></param>
        private void SetCommandButtonEvent(BaseCommandButton commandButton)
        {
            commandButton.button.onClick.AddListener(() =>
            {
                StageUserOperation.Instance.InitFromCommand(commandButton, false);
            });
        }
    }
}
