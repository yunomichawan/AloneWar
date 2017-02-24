using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AloneWar.Stage.Event.EventObject.Base;

namespace AloneWar.Stage.Component
{
    public abstract class BaseStageObject : MonoBehaviour
    {

        #region 座標

        /// <summary>
        /// 
        /// </summary>
        public abstract int PositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int X { get { return StageManager.Instance.StageInformation.GetPositionX(this.PositionId); } }
        /// <summary>
        /// 
        /// </summary>
        public int Y { get { return StageManager.Instance.StageInformation.GetPositionY(this.PositionId); } }

        /// <summary>
        /// 
        /// </summary>
        public abstract string Area { get; }

        #endregion

        /// <summary>
        /// オブジェクト名兼、認識ID
        /// </summary>
        public abstract int StageObjectId { get; }

    }
}
