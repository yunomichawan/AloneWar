namespace AloneWar.Common
{
    public class AloneWarConst
    {
        public const int ErrorPositionId = -1;

        public const float MaxPercent = 100;

        /// <summary>
        /// ルート探索の最大範囲
        /// </summary>
        public const int SearchRootMaxDistance = 100;

        public struct DisplaySize
        {
            public const int X = 1280;

            public const int Y = 720;
        }

        public struct MaterialProperty
        {
            public const string Shadow = "Shadow";

            public const string MassColor = "MassColor";
        }

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
