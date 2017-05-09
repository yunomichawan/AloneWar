using UnityEngine;
using UnityEngine.UI;

namespace AloneWar.UI.Common.Component
{
    /*
     * エディター拡張でTextの値が変更できるようにする
     */

    [RequireComponent(typeof(Text))]
    public class LabelResourceText : MonoBehaviour
    {
        [SerializeField]
        public string labelResourceId;

        [SerializeField]
        public Text labelText;

    }
}
