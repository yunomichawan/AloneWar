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

                
            }
        }
        private bool isClose = false;
    }
}
