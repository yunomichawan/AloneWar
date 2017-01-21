using System;
using System.Reflection;

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
