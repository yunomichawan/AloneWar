using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AloneWar.Common
{
    public class AloneWarConst
    {
        public struct SceneName
        {
            public const string Title = "Title";

            public const string Home = "Home";

            public const string MainStage = "MainStage";

            public const string CacheDataScene = "CacheDataScene";
        }

        public struct ResourcePath
        {
            public const string Prefab = "Prefab/";

            public const string UnitPrefab = Prefab + "Unit/";
        }

        public struct SqliteDataBaseName
        {
            public const string Master = "Master.db";

            public const string Transaction = "Transaction.db";
        }
    }
}
