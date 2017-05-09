using System;
using System.Collections.Generic;

namespace AloneWar.Unit.Component
{
    public class UnitSummaryComponent
    {
        /// <summary>
        /// 存在Y/N
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.UnitMainComponent == null && this.UnitSubComponentList.Count.Equals(0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public UnitMainComponent UnitMainComponent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<UnitSubComponent> UnitSubComponentList { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnitSummaryComponent()
        {
            this.UnitSubComponentList = new List<UnitSubComponent>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        public void ForEach(Action<UnitBaseComponent> callback)
        {
            if (callback != null)
            {
                if (this.UnitMainComponent != null)
                {
                    callback(this.UnitMainComponent);
                }
                // .ForEach -> foreach
                this.UnitSubComponentList.ForEach(callback);
            }
        }
    }
}