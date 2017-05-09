using AloneWar.DataObject.Sqlite.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace AloneWar.Common.Bind
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertyInfoCache
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public MethodInfo SetMethod { get; set; }

        public PropertyInfoCache(Type declaringType, PropertyInfo propertyInfo)
        {
            this.Type = propertyInfo.PropertyType;
            this.SetMethod = propertyInfo.GetSetMethod();
            this.Name = string.Format("{0}.{1}", declaringType.Name, propertyInfo.Name);
        }

        public static Dictionary<string, PropertyInfoCache> CreatePropertyCache(Type type)
        {
            PropertyInfo[] propertyInfoArray = type.GetProperties();
            Dictionary<string, PropertyInfoCache> propertyInfoCacheList = new Dictionary<string, PropertyInfoCache>();
            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                PropertyInfoCache propertyInfoCache = new PropertyInfoCache(type, propertyInfo);
                propertyInfoCacheList.Add(propertyInfoCache.Name, propertyInfoCache);
            }

            return propertyInfoCacheList;
        }
    }

    public class DataBinding<T>
    {
        /// <summary>
        /// データテーブルからリストに変換
        /// Conversion from the data table to list
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> DataTableToObjectList(DataTable table)
        {
            List<T> objectList = new List<T>();
            Dictionary<string, PropertyInfoCache> propertyInfoCacheList = PropertyInfoCache.CreatePropertyCache(typeof(T));
            table.Rows.ForEach(d =>
            {
                T obj = (T)Activator.CreateInstance(typeof(T), new object[] { });
                DataRowToObject(d, obj, propertyInfoCacheList);
                objectList.Add(obj);
            });

            return objectList;
        }

        /// <summary>
        /// データ行からクラスに変換
        /// Conversion from the data line to the class
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="obj"></param>
        public static void DataRowToObject(DataRow dataRow, T obj, Dictionary<string, PropertyInfoCache> propertySetMethod)
        {
            propertySetMethod.ToList().ForEach(pair =>
            {
                object dataObject = dataRow[pair.Key];
                SetPropertyValue(pair.Value.SetMethod, pair.Value.Type, obj, dataObject);
            });
        }
        
        /// <summary>
        /// 指定したプロパティに値をセットする
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(MethodInfo methodInfo, Type propertyType, T obj, object value)
        {
            try
            {
                if (value == null) return;
                if (propertyType.Equals(typeof(int)))
                {
                    int result;
                    if (int.TryParse(value.ToString(), out result))
                    {
                        methodInfo.Invoke(obj, new object[] { result });
                    }
                    else
                    {
                        methodInfo.Invoke(obj, new object[] { 0 });
                    }
                }
                else if (propertyType.Equals(typeof(float)))
                {
                    float result;
                    if (float.TryParse(value.ToString(), out result))
                    {
                        methodInfo.Invoke(obj, new object[] { result });
                    }
                    else
                    {
                        methodInfo.Invoke(obj, new object[] { 0 });
                    }
                }
                else if (propertyType.Equals(typeof(double)))
                {
                    double result;
                    if (double.TryParse(value.ToString(), out result))
                    {
                        methodInfo.Invoke(obj, new object[] { result });
                    }
                    else
                    {
                        methodInfo.Invoke(obj, new object[] { 0 });
                    }
                }
                else if (propertyType.Equals(typeof(bool)))
                {
                    if (value.GetType().Equals(typeof(bool)))
                    {
                        methodInfo.Invoke(obj, new object[] { (bool)value });
                    }
                    else
                    {
                        methodInfo.Invoke(obj, new object[] { value.ToString().Equals("1") });
                    }
                }
                else if (propertyType.Equals(typeof(DateTime)))
                {
                    DateTime result;
                    if (DateTime.TryParse(value.ToString(), out result))
                    {
                        methodInfo.Invoke(obj, new object[] { result });
                    }
                }
                else if (propertyType.Equals(typeof(string)))
                {
                    methodInfo.Invoke(obj, new object[] { value.ToString() });
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.Log(ex.Message + methodInfo.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(PropertyInfo propertyInfo, T obj, object value)
        {
            SetPropertyValue(propertyInfo.GetSetMethod(), propertyInfo.PropertyType, obj, value);
        }

    }

    /// <summary>
    /// 匿名リストバインダー
    /// </summary>
    public class DataAnonymousBinding
    {
        public static IList DataTableToObjectList(DataTable table, Type type)
        {
            Type listType = typeof(List<>);
            Type tListType = listType.MakeGenericType(type);
            IList objectList = (IList)Activator.CreateInstance(tListType, new object[] { });
            Dictionary<string, PropertyInfoCache> propertyInfoCacheList = PropertyInfoCache.CreatePropertyCache(type);
            foreach (DataRow row in table.Rows)
            {
                object obj = Activator.CreateInstance(type, new object[] { });
                DataBinding<object>.DataRowToObject(row, obj, propertyInfoCacheList);
                objectList.Add(obj);
            }

            return objectList;
        }
    }
}