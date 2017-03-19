using System;
using System.Reflection;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        public static void Remove<T>(this List<T> list, Func<T, bool> func)
        {
            List<T> removeList = new List<T>();
            foreach (T item in list)
            {
                if (!func(item))
                {
                    removeList.Add(item);
                }
            }

            list = removeList;
        }

        /// <summary>
        /// 逆サイド取得
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        public static UnitSideCategory Reverse(this UnitSideCategory side)
        {
            switch (side)
            {
                case UnitSideCategory.Player:
                    return UnitSideCategory.Enemy;
                case UnitSideCategory.Enemy:
                    return UnitSideCategory.Player;
                case UnitSideCategory.Another:
                    return UnitSideCategory.Enemy;
                default:
                    return UnitSideCategory.None;
            }
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
