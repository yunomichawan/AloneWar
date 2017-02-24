using AloneWar.Common.Bind;
using AloneWar.DataObject.Sqlite.Helper;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.DataObject.Sqlite.SqliteObject.Base;
using AloneWar.Unit.Status;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AloneWar.DataObject.Sqlite.Service
{
    /// <summary>
    /// UnitStatusをDBから取得するクラス
    /// </summary>
    /// <typeparam name="T">UnitMainStatusData or UnitSubStatusData</typeparam>
    public class UnitStatusService
    {
        /// <summary>
        /// まとめ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class UnitMaterialSummary
        {
            public List<UnitAssetData> UnitAssetDataList { get; set; }
            public List<UnitBaseStatusData> UnitBaseStatusList { get; set; }
            public List<UnitStageStatusData> UnitStageStatusList { get; set; }
            public Dictionary<int, List<IForeignKey>> UnitItemDataList { get; set; }
            public Dictionary<int, List<IForeignKey>> UnitSkillDataList { get; set; }
        }

        /// <summary>
        ///  get enemy
        /// </summary>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public List<UnitSubStatus> GetUnitSubObejctStatusListOnStage(int stageId, bool isStage)
        {
            SqliteQueryBuilder builder = this.CreateQueryForUnitOnStage(stageId, isStage, typeof(UnitSubStatus));
            builder.AddJoinTable(typeof(UnitStageStatusData), typeof(UnitAIStageStatusData), "Id", "UnitStageId");
            DataTable dataTable = SqliteHelper.Instance.GetSqliteObjectTable(builder);

            return this.SummaryUnitSubStatusList(dataTable); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stageId"></param>
        /// <param name="unitStageIdArray"></param>
        /// <returns></returns>
        public List<UnitSubStatus> GetUnitSubEventObjectList(int stageId, params int[] unitStageIdArray)
        {
            SqliteQueryBuilder builder = this.CreateQueryForUnitOnStage(stageId, true, typeof(UnitSubStatusData));
            builder.AddJoinTable(typeof(UnitStageStatusData), typeof(UnitAIStageStatusData), "Id", "UnitStageId");
            builder.AddInCondition("Id", unitStageIdArray.Select<int, string>(u => u.ToString()).ToArray(), typeof(UnitStageStatusData), true);
            DataTable dataTable = SqliteHelper.Instance.GetSqliteObjectTable(builder);

            return this.SummaryUnitSubStatusList(dataTable);
        }

        /// <summary>
        /// サブをまとめる
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private List<UnitSubStatus> SummaryUnitSubStatusList(DataTable dataTable)
        {
            UnitMaterialSummary unitSqliteSummary = this.SetUnitSummary<UnitStageItemData, UnitStageSkillData>(dataTable, this.GetItemQueryBuider<UnitStageItemData>(), GetSkillQueryBuider<UnitStageSkillData>());
            List<UnitSubStatusData> unitSubStatusDataList = DataBinding<UnitSubStatusData>.DataTableToObjectList(dataTable);
            List<UnitAIStageStatusData> unitAIStageStatusDataList = DataBinding<UnitAIStageStatusData>.DataTableToObjectList(dataTable);
            List<UnitSubStatus> unitSubStatusList = new List<UnitSubStatus>();
            for (int i = 0; i < unitSubStatusDataList.Count; i++)
            {
                UnitSubStatus unitSubStatus = new UnitSubStatus();
                unitSubStatus.StageStatus = unitSqliteSummary.UnitStageStatusList[i];
                unitSubStatus.BaseStatus = unitSqliteSummary.UnitBaseStatusList[i];
                unitSubStatus.UnitAssetData = unitSqliteSummary.UnitAssetDataList[i];
                unitSubStatus.UnitItemDataList = new ForeignKeyObject<ItemData>(unitSqliteSummary.UnitItemDataList[i]);
                unitSubStatus.UnitSkillDataList = new ForeignKeyObject<SkillData>(unitSqliteSummary.UnitSkillDataList[i]);

                unitSubStatus.UnitSubStatusData = unitSubStatusDataList[i];
                unitSubStatus.UnitAIStageStatusData = unitAIStageStatusDataList[i];
                unitSubStatusList.Add(unitSubStatus);
            }

            return unitSubStatusList;
        }

        /// <summary>
        /// 共通部分のクエリを作成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stageId"></param>
        /// <returns></returns>
        private SqliteQueryBuilder CreateQueryForUnitOnStage(int stageId,bool isStage,Type type)
        {
            SqliteQueryBuilder builder = new SqliteQueryBuilder(typeof(UnitBaseStatusData));
            builder.AddJoinTable(typeof(UnitBaseStatusData), type, "Id", "UnitId");
            builder.AddJoinTable(typeof(UnitBaseStatusData), typeof(UnitStageStatusData), "Id", "UnitId");
            builder.AddJoinTable(typeof(UnitBaseStatusData), typeof(UnitAssetData), "Id", "UnitId");
            builder.AddCondition("StageId", stageId, typeof(UnitStageStatusData), true);
            if (!isStage)
                builder.AddCondition("IsEvent", "1", typeof(UnitStageStatusData), true);
            builder.AddOrderByColumns("SortNo", typeof(UnitStageStatusData), true);
            return builder;
        }

        /// <summary>
        /// まとめる
        /// </summary>
        /// <typeparam name="ItemT"></typeparam>
        /// <typeparam name="SkillT"></typeparam>
        /// <param name="dataTable"></param>
        /// <param name="getItemQueryCallback"></param>
        /// <param name="getSkillQueryCallback"></param>
        /// <returns></returns>
        private UnitMaterialSummary SetUnitSummary<ItemT, SkillT>(DataTable dataTable, Func<UnitStageStatusData, SqliteQueryBuilder> getItemQueryCallback, Func<UnitStageStatusData, SqliteQueryBuilder> getSkillQueryCallback)
            where ItemT : SqliteBaseData, IForeignKey
            where SkillT : SqliteBaseData, IForeignKey
        {
            UnitMaterialSummary unitSummary = new UnitMaterialSummary();
            
            unitSummary.UnitBaseStatusList = DataBinding<UnitBaseStatusData>.DataTableToObjectList(dataTable);
            unitSummary.UnitStageStatusList = DataBinding<UnitStageStatusData>.DataTableToObjectList(dataTable);
            unitSummary.UnitAssetDataList = DataBinding<UnitAssetData>.DataTableToObjectList(dataTable);
            // Item,Skill Select
            for (int i = 0; i < unitSummary.UnitStageStatusList.Count; i++)
            {
                // Main
                UnitStageStatusData unitStageStatusData = unitSummary.UnitStageStatusList[i];

                // Item Select
                SqliteQueryBuilder sqliteQueryBuilder = getItemQueryCallback(unitStageStatusData);
                List<ItemT> itemList = SqliteHelper.Instance.GetSqliteObjectList<ItemT>(sqliteQueryBuilder);
                unitSummary.UnitItemDataList.Add(i, itemList.ConvertAll<IForeignKey>(item => { return (IForeignKey)item; }));

                // SkillSelect
                sqliteQueryBuilder = getSkillQueryCallback(unitStageStatusData);
                List<SkillT> skillList = SqliteHelper.Instance.GetSqliteObjectList<SkillT>(sqliteQueryBuilder);
                unitSummary.UnitSkillDataList.Add(i, skillList.ConvertAll<IForeignKey>(skill => { return (IForeignKey)skill; }));

            }

            return unitSummary;
        }

        /// <summary>
        /// 型に応じたクエリを作成する関数を取得
        /// </summary>
        /// <typeparam name="ItemT">T is UnitItemData or UnitStageItemDatas</typeparam>
        /// <returns></returns>
        private Func<UnitStageStatusData, SqliteQueryBuilder> GetItemQueryBuider<ItemT>() where ItemT : SqliteBaseData, IForeignKey
        {
            if (typeof(ItemT).Equals(typeof(UnitItemData)))
            {
                return (u) => {
                    SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder(typeof(ItemT));
                    sqliteQueryBuilder.AddCondition("UnitId", u.UnitId, typeof(ItemT), true);
                    return sqliteQueryBuilder;
                };
            }
            else if (typeof(ItemT).Equals(typeof(UnitStageItemData)))
            {
                return (u) =>
                {
                    SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder(typeof(ItemT));
                    sqliteQueryBuilder.AddCondition("UnitId", u.UnitId, typeof(ItemT), true);
                    sqliteQueryBuilder.AddCondition("StageId", u.StageId, typeof(ItemT), true);
                    return sqliteQueryBuilder;
                };
            }

            return null;
        }

        /// <summary>
        /// 型に応じたクエリを作成する関数を取得
        /// </summary>
        /// <typeparam name="SkillT">T is UnitSkillData or UnitStageSkillData</typeparam>
        /// <returns></returns>
        private Func<UnitStageStatusData, SqliteQueryBuilder> GetSkillQueryBuider<SkillT>() where SkillT : SqliteBaseData, IForeignKey
        {
            if (typeof(SkillT).Equals(typeof(UnitSkillData)))
            {
                return (s) =>
                {
                    SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder(typeof(SkillT));
                    sqliteQueryBuilder.AddCondition("UnitId", s.UnitId, typeof(SkillT), true);
                    return sqliteQueryBuilder;
                };
            }
            else if (typeof(SkillT).Equals(typeof(UnitStageSkillData)))
            {
                return (s) =>
                {
                    SqliteQueryBuilder sqliteQueryBuilder = new SqliteQueryBuilder(typeof(SkillT));
                    sqliteQueryBuilder.AddCondition("UnitId", s.UnitId, typeof(SkillT), true);
                    sqliteQueryBuilder.AddCondition("StageId", s.StageId, typeof(SkillT), true);
                    return sqliteQueryBuilder;
                };
            }

            return null;
        }
    }
}
