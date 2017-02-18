using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AloneWar.Common.Extensions
{
    public static class UnityExtensions
    {
        
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
