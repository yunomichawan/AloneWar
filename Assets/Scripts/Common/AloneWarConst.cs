using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AloneWar.Common
{
    public class AloneWarConst
    {
        public const int ErrorPositionId = -1;

        public const float MaxPercent = 100;

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
            public const string Master = "Master";

            public const string Transaction = "Transaction";

            private const string Extension = ".db";

            public const string MasterDb = Master + Extension;

            public const string TransactionDb = Transaction + Extension;
        }
    }
}
