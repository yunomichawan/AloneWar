using AloneWar.Common;
using UnityEngine;

namespace AloneWar.UI.Common
{
    /// <summary>
    /// 座標を自動設定できる設定を保持
    /// </summary>
    public interface IChildPositionHandler
    {
        UIPivot UiPivot { get; }

        UIDirection UiDirection { get; }

        Vector2 Margin { get; set; }
    }
}