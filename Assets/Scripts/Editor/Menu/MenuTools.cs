using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.Service;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;

public class ManuTools : MonoBehaviour {
    
    [MenuItem("Tools/Sqlite/TableCheck")]
    static void SqliteTableCheck()
    {
        TableSupportService tableSupportService = new TableSupportService();
        bool flg = SqliteHelper.Instance.SqliteTableChecker(typeof(MasterCodeData));
        Debug.Log(flg);
    }

}
