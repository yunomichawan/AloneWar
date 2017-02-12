using System;
using System.Reflection;
using UnityEngine;

// 拡張メソッドを実装
namespace AloneWar.Common.Extensions
{
    public static class CommonExtensions
    {
        public static void SafeCall(this Action callback)
        {
            if (callback != null)
            {
                callback();
            }
        }

        /// <summary>
        /// コールバックにメモ付きで実行(デバッグ用)
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="title"></param>
        public static void CallMemo(this Action callback,string title)
        {
            callback.SafeCall();
            Debug.Log(string.Format("Callback_Memo:{0}", title));
        }
    }


    public static class AttributeExtensions
    {
        public static T GetAttribute<T>(this Type type)
        {
            object attribute = Attribute.GetCustomAttribute(type, typeof(T));
            if (attribute == null)  
                return default(T); 
            return (T)attribute;
        }

        public static T GetAttribute<T>(this PropertyInfo propertyInfo)
        {
            object attribute = Attribute.GetCustomAttribute(propertyInfo, typeof(T));
            if (attribute == null)
                return default(T); 
            return (T)attribute;
        }
    }
}
