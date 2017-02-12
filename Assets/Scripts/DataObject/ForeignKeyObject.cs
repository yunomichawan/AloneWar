using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Common.Component;

namespace AloneWar.DataObject
{
    /// <summary>
    /// 関連付け項目をIndexで管理する
    /// </summary>
    /// <typeparam name="MainT">SceneCommonComponentで管理されているクラス、且つ関連付けクラスの親</typeparam>
    public class ForeignKeyObject<MainT> where MainT : SqliteBaseData
    {

        /// <summary>
        /// 関連付けキーリスト
        /// </summary>
        private List<IForeignKey> ForeignKeyList { get { return this.foreignKeyList; } set { this.foreignKeyList = value; } }
        /// <summary>
        /// 関連付けキーリスト
        /// </summary>
        private List<IForeignKey> foreignKeyList = new List<IForeignKey>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="foreignObjectList"></param>
        public ForeignKeyObject(List<IForeignKey> foreignObjectList)
        {
            this.ForeignKeyList = foreignObjectList;
        }

        /// <summary>
        /// メイン項目の取得(id指定)
        /// </summary>
        /// <param name="id">メイン項目のプライマリキー</param>
        /// <returns></returns>
        public MainT GetMainObject(int id)
        {
            return SceneCommonComponent.Instance.GetMasterCommonObject<Dictionary<int,MainT>>()[id];
        }

        /// <summary>
        /// メイン項目の取得(関連項目指定)
        /// </summary>
        /// <param name="foreignObject"></param>
        /// <returns></returns>
        public MainT GetMainObject(IForeignKey foreignObject)
        {
            return SceneCommonComponent.Instance.GetMasterCommonObject<Dictionary<int, MainT>>()[foreignObject.ForeignKey];
        }

        /// <summary>
        /// 関連付け項目に紐付く、メイン項目を全て取得
        /// </summary>
        /// <returns></returns>
        public List<MainT> GetMainForeignObjectList()
        {
            List<MainT> mainObjectList = new List<MainT>(foreignKeyList.Count);
            foreach (IForeignKey fk in foreignKeyList)
            {
                mainObjectList.Add(SceneCommonComponent.Instance.GetMasterCommonObject<Dictionary<int, MainT>>()[fk.ForeignKey]);
            }

            return mainObjectList;
        }
    }
}
