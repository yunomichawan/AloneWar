using AloneWar.Common;
using AloneWar.DataObject.Sqlite.SqliteAttributes;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;

namespace AloneWar.DataObject.Sqlite.SqliteObject.Transaction
{
    /// <summary>
    /// コンフィグ
    /// </summary>
    [DataAccess("GameConfigData", AloneWarConst.SqliteDataBaseName.TransactionDb)]
    public class GameConfigData : SqliteBaseData
    {
        [SqliteProperty]
        public float Volume { get; set; }

        [SqliteProperty]
        public bool AutoControllFlg { get; set; }

        [SqliteProperty]
        public float GameSpeed { get; set; }

        [SqliteProperty]
        public float TextSpeed { get; set; }
    }
}
