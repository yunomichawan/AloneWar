using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.Service;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using UnityEditor;
using UnityEngine;

public class ManuTools : MonoBehaviour {
    
    [MenuItem("Tools/Sqlite/TableCheck")]
    static void SqliteTableCheck()
    {
        TableSupportService tableSupportService = new TableSupportService();
        bool flg = SqliteHelper.Instance.SqliteTableChecker(typeof(MasterCodeData));
        Debug.Log(flg);
    }

}
