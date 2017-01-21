using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.Common.Extensions;

namespace AloneWar.Common.Bind
{

    /// <summary>
    /// バインド用デリゲート
    /// </summary>
    public class BinderDelegate
    {
        public delegate ValueT GetValue<SourceT, ValueT>(SourceT source);

        public delegate void SetValue<SourceT, ValueT>(SourceT source, ValueT value);

        /// <summary>
        /// 型に応じたセットデリゲート作成
        /// </summary>
        public static SetValue<SourceT, ValueT> CreateSetValue<SourceT, ValueT>(PropertyInfo propertyInfo)
        {
            return (SetValue<SourceT, ValueT>)Delegate.CreateDelegate(typeof(SetValue<SourceT, ValueT>), propertyInfo.GetSetMethod());
        }

        /// <summary>
        /// 型に応じたゲットデリゲート作成
        /// </summary>
        /// <typeparam name="SourceT"></typeparam>
        /// <typeparam name="ValueT"></typeparam>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static GetValue<SourceT, ValueT> CreateGetValue<SourceT, ValueT>(PropertyInfo propertyInfo)
        {
            return (GetValue<SourceT, ValueT>)Delegate.CreateDelegate(typeof(GetValue<SourceT, ValueT>), propertyInfo.GetGetMethod());

        }
    }
}
