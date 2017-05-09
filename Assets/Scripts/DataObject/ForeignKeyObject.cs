using AloneWar.Common.Component;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using System.Collections.Generic;
using System.Linq;

namespace AloneWar.DataObject
{
    /// <summary>
    /// 関連付け項目をIndexで管理する
    /// </summary>
    /// <typeparam name="MainT">SceneCommonComponentで管理されているクラス、且つ関連付けクラスの親</typeparam>
    public class ForeignKeyObject<MainT> where MainT : SqliteBaseData
    {

        /// <summary>
        /// 
        /// </summary>
        public List<MainT> ForeignObjectList { get; set; }

        /// <summary>
        /// 関連付けキーリスト
        /// </summary>
        private List<IForeignKey> ForeignKeyList { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="foreignKeyList"></param>
        public ForeignKeyObject(List<IForeignKey> foreignKeyList)
        {
            this.ForeignKeyList = foreignKeyList;
            List<int> keyList = this.ForeignKeyList.ConvertAll<int>(f => f.ForeignKey);
            this.ForeignObjectList = TransactionComponent.Get<MainT>().Where(l => keyList.Contains(l.Id)).ToList();
        }

        /// <summary>
        /// メイン項目の取得(id指定)
        /// </summary>
        /// <param name="id">メイン項目のプライマリキー</param>
        /// <returns></returns>
        public MainT GetMainObject(int id)
        {
            return this.ForeignObjectList.FirstOrDefault(f => f.Id.Equals(id));
        }

        /// <summary>
        /// メイン項目の取得(関連項目指定)
        /// </summary>
        /// <param name="foreignObject"></param>
        /// <returns></returns>
        public MainT GetMainObject(IForeignKey foreignObject)
        {
            return this.ForeignObjectList.FirstOrDefault(f => f.Id.Equals(foreignObject.ForeignKey));
        }

    }
}
