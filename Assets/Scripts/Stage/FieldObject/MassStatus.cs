using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using AloneWar.Stage.Event.EventObject;

namespace AloneWar.Stage.FieldObject
{
    /// <summary>
    /// 
    /// </summary>
    public class MassStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Area { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsClose
        {
            get
            {
                return this.isClose;
            }
            set
            {
                this.isClose = value;

                if (this.isClose)
                {
                    this.MassEventList.ForEach(m => {
                        if (m.VaildFlg)
                        {
                            StageManager.Instance.stageEventBuilder.TaskQueue.Enqueue(m.EventTask);
                        }
                    });
                }
            }
        }
        private bool isClose = false;

        public List<MassEvent> MassEventList { get { return this.massEventList; } set { this.massEventList = value; } }
        private List<MassEvent> massEventList = new List<MassEvent>();
    }
}
