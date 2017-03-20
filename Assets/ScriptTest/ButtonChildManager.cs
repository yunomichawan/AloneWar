using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq.Expressions;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.Service;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.Stage.Component;
using AloneWar.Stage.FieldObject;

public class ButtonChildManager : MonoBehaviour {

    [SerializeField]
    public Button[] buttons;

    [SerializeField]
    public RectTransform rectTransform;

    // Use this for initialization
    void Start() {
        
        buttons[0].onClick.AddListener(new UnityAction(() =>
        {
            //SqliteHelper.Instance.CreateSqliteTable(typeof(UnitBaseStatusData));
            bool flg = SqliteHelper.Instance.SqliteTableChecker(typeof(MasterCodeData));
            Debug.Log(flg);
        }));

        buttons[1].onClick.AddListener(new UnityAction(() =>
        {
            TableSupportService tableSupportService = new TableSupportService();
            //tableSupportService.AllRemakeDb();
        }));
    }
}
