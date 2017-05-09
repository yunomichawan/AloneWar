using AloneWar.Common;
using AloneWar.Stage.Component;
using AloneWar.Stage.Controller;
using AloneWar.Unit.Component;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AloneWar.UI.Common.Parts.Command
{
    /// <summary>
    /// inspector上で扱いたいため、abstractではなく実装
    /// </summary>
    [Serializable]
    public class BaseCommandButton
    {
        #region inspector property

        /// <summary>
        /// 分類(実装する必要性が薄い?)
        /// 何があるか確認のため置いておく
        /// </summary>
        public CommandCategory commandCategory;

        public Button button;

        #endregion

        #region 抽象

        /// <summary>
        /// 左クリック時の処理を実装
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="unit"></param>
        public virtual void LeftClick(GameObject obj, UnitBaseComponent unit) { }

        /// <summary>
        /// 右クリック時の処理を実装
        /// </summary>
        public virtual void RightClick(GameObject obj, UnitBaseComponent unit)
        {
            StageUserOperation.Instance.BackCommand();
        }

        /// <summary>
        /// マウスオーバー時の処理を実装
        /// </summary>
        /// <param name="obj"></param>
        public virtual void MouseOver(GameObject obj) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stageObject"></param>
        public virtual void MouseOut(BaseStageObject stageObject) { }

        /// <summary>
        /// 初期化前に行いたい処理を実装
        /// </summary>
        /// <param name="unit"></param>
        public virtual void OperationInitBefore(UnitBaseComponent unit) { }

        /// <summary>
        /// 初期化後に行いたい処理を実装
        /// </summary>
        /// <param name="unit"></param>
        public virtual void OperationInitAfter(UnitBaseComponent unit) { }

        #endregion

    }
}
