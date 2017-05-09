using System;
using System.Collections;
using UnityEngine;

namespace AloneWar.Common.Extensions
{
    public class UnityExtensions
    {

        public static IEnumerator Wait1Frame()
        {
            yield return new WaitForEndOfFrame();
        }

        public static void ShadowRender(Renderer renderer)
        {
            EditorDebug.DebugAlert("Shadow Material");
            if (renderer.material.HasProperty(AloneWarConst.MaterialProperty.Shadow))
            {
                renderer.material.SetColor(AloneWarConst.MaterialProperty.Shadow, Color.gray);
            }
        }

        /// <summary>
        /// 引数ありのIEnumeratorを引数なしに変換する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Func<IEnumerator> ConvertIEnumerator<T>(Func<T, IEnumerator> func, T arg)
        {
            return () => func(arg);
        }
    }

    public static class EditorDebug
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void DebugAlert(string msg)
        {
            Debug.LogError("未実装：" + msg);
        }
    }
}
