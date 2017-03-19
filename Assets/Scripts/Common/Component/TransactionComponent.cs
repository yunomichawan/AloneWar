using System;
using System.Collections;
using System.Collections.Generic;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AloneWar.Common.Component
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionComponent : SingletonComponent<TransactionComponent>
    {
        public static List<T> Get<T>()
        {
            return Instance.GetObject<T>();
        }

        public static void Set(Type type, IList obj)
        {
            Instance.SetObject(type, obj);
        }

        public static void Add<T>(T obj) where T : SqliteBaseData
        {
            // Get,Setと同じように実装すること

        }

        public static void Remove(SqliteBaseData sqliteBaseData)
        {
            sqliteBaseData.dbObjectStatus = DbObjectStatus.Delete;
        }

        public Dictionary<Type, IList> TransactionData { get { return this.transactionData; } set { this.transactionData = value; } }
        private Dictionary<Type, IList> transactionData = new Dictionary<Type, IList>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        private void SetObject(Type type, IList obj)
        {
            Type key = type;
            if (this.TransactionData.ContainsKey(key))
            {
                this.TransactionData[key] = obj;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private List<T> GetObject<T>()
        {
            Type key = typeof(T);
            if (this.TransactionData.ContainsKey(key))
            {
                return (List<T>)this.TransactionData[key];
            }
            else
            {
                Debug.LogError(key + " object is not managed");
                return default(List<T>);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        private void AddObject<T>(T obj) where T : SqliteBaseData
        {
            Type key = typeof(T);
            if (this.TransactionData.ContainsKey(key))
            {
                this.TransactionData[key].Add(obj);
            }
        }
    }
}
