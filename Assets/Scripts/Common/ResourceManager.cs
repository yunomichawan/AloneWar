using UnityEngine;

namespace AloneWar.Common
{
    public class ResourceManager
    {
        public static T Load<T>(string path, ResourceCategory resourceCategory) where T : Object
        {
            switch (resourceCategory)
            {
                case ResourceCategory.UnitPrefab:
                    return Resources.Load<T>(AloneWarConst.ResourcePath.UnitPrefab + path);
                case ResourceCategory.None:
                default:
                    return default(T);
            }
        }
    }
}
