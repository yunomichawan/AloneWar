using AloneWar.Common.TaskHelper;
using System;
using UnityEngine;

namespace AloneWar.Common.Component
{
    public class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = (T)FindObjectOfType(t);
                    if (instance == null)
                    {
                        Debug.LogError(t);
                    }
                }

                return instance;
            }
        }

        virtual protected void Awake()
        {
            if (this != Instance)
            {
                Destroy(this);
                return;
            }
        }
    }
}
