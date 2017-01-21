using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.Common.TaskHelper;

namespace AloneWar.Common.Bind
{

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
            object lockObj = new object();
            AsyncTaskHelper helper = new AsyncTaskHelper(() => {
                foreach (DataRow row in table.Rows)
                {
                    T obj = (T)Activator.CreateInstance(typeof(T), new object[] { });
                    DataRowToObject(row, obj);
                    lock (lockObj)
                    {
                        objectList.Add(obj);
                    }
                }
            });
            helper.TaskRun();

            return objectList;
        }

        /// <summary>
        /// データ行からクラスに変換
        /// Conversion from the data line to the class
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="obj"></param>
        public static void DataRowToObject(DataRow dataRow, T obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] propertyInfoArray = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfoArray)
            {
                object dataObject = dataRow[propertyInfo.Name];
                SetPropertyValue(propertyInfo, obj, dataObject);
            }
        }

        /// <summary>
        /// 指定したプロパティに値をセットする
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue(PropertyInfo propertyInfo, T obj, object value)
        {
            try
            {
                if (value == null) return;
                Type propertyType = propertyInfo.PropertyType;
                if (propertyType.Equals(typeof(int)))
                {
                    int result;
                    BinderDelegate.SetValue<T, int> bind = BinderDelegate.CreateSetValue<T, int>(propertyInfo);
                    if (int.TryParse(value.ToString(), out result))
                    {
                        bind(obj, result);
                    }
                    else
                    {
                        bind(obj, 0);
                    }
                }
                else if (propertyType.Equals(typeof(float)))
                {
                    float result;
                    BinderDelegate.SetValue<T, float> bind = BinderDelegate.CreateSetValue<T, float>(propertyInfo);
                    if (float.TryParse(value.ToString(), out result))
                    {
                        bind(obj, result);
                    }
                    else
                    {
                        bind(obj, 0);
                    }
                }
                else if (propertyType.Equals(typeof(double)))
                {
                    double result;
                    BinderDelegate.SetValue<T, double> bind = BinderDelegate.CreateSetValue<T, double>(propertyInfo);
                    if (double.TryParse(value.ToString(), out result))
                    {
                        bind(obj, result);
                    }
                    else
                    {
                        bind(obj, 0);
                    }
                }
                else if (propertyType.Equals(typeof(bool)))
                {
                    BinderDelegate.SetValue<T, bool> bind = BinderDelegate.CreateSetValue<T, bool>(propertyInfo);
                    if (value.GetType().Equals(typeof(bool)))
                    {
                        bind(obj, (bool)value);
                    }
                    else
                    {
                        bind(obj, value.ToString().Equals("1"));
                    }
                }
                else if (propertyType.Equals(typeof(DateTime)))
                {
                    DateTime result;
                    BinderDelegate.SetValue<T, DateTime> bind = BinderDelegate.CreateSetValue<T, DateTime>(propertyInfo);
                    if (DateTime.TryParse(value.ToString(), out result))
                    {
                        bind(obj, result);
                    }
                }
                else if (propertyType.Equals(typeof(string)))
                {
                    BinderDelegate.SetValue<T, string> bind = BinderDelegate.CreateSetValue<T, string>(propertyInfo);
                    bind(obj, value.ToString());
                }
            }
            catch (NullReferenceException ex)
            {
                Debug.Log(ex.Message + propertyInfo.Name);
            }
        }
    }
}