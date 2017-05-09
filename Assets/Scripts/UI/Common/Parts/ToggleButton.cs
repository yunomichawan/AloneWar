using AloneWar.Common.Extensions;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AloneWar.UI.Common.Parts
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField]
        private Toggle toggle;

        public void AddEventListener(Action callback)
        {
            this.toggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    // ボタンの画像を切替、切替以外の方法が存在するのであればそれを優先
                    // toggle.image
                    callback.SafeCall();
                }
                else
                {
                    // ボタンの画像を切替、切替以外の方法が存在するのであればそれを優先
                }
            });
        }
    }
}
