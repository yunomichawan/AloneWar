using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AloneWar.Common.Component
{
    /// <summary>
    /// 
    /// </summary>
    public class SceneCommonComponent : SingletonComponent<SceneCommonComponent>
    {
        public Dictionary<string, object> MasterCommonStatus { get { return this.masterCommonStatus; } set { this.masterCommonStatus = value; } }
        private Dictionary<string, object> masterCommonStatus = new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        public void SetMasterCommonObject(Type type, object obj)
        {
            string key = type.Name;
            if (this.MasterCommonStatus.ContainsKey(key))
            {
                this.MasterCommonStatus[key] = obj;
            }
            else
            {
                this.MasterCommonStatus.Add(key, obj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetMasterCommonObject<T>()
        {
            string key =typeof(T).Name;
            if (this.MasterCommonStatus.ContainsKey(key))
            {
                return (T)this.MasterCommonStatus[key];
            }
            else
            {
                Debug.LogError(key + " object is not managed");
                return default(T);
            }
        }
    }
}
