﻿using AloneWar.Common.Bind;
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
    public class UnitStatusService<T> where T : SqliteBaseData
    {
        /// <summary>
        /// データを集めて変換するクラス
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class UnitSqliteSummary
        {
            public List<T> UnitStatusList { get; set; }
            public List<UnitBaseStatusData> UnitBaseStatusList { get; set; }
            public List<UnitStageStatusData> UnitStageStatusList { get; set; }
            public Dictionary<int, List<IForeignKey>> UnitItemDataList { get; set; }
            public Dictionary<int, List<IForeignKey>> UnitSkillDataList { get; set; }

            public List<UnitObjectStatus<T>> GetUnitObjectStatusList()
            {
                List<UnitObjectStatus<T>> unitObjectStatusList = new List<UnitObjectStatus<T>>();
                for (int i = 0; i < this.UnitStatusList.Count; i++)
                {
                    UnitObjectStatus<T> unitObjectStatus = new UnitObjectStatus<T>();
                    unitObjectStatus.UnitStatus = this.UnitStatusList[i];
                    unitObjectStatus.BaseStatus = this.UnitBaseStatusList[i];
                    unitObjectStatus.StageStatus = this.UnitStageStatusList[i];
                    unitObjectStatusList.Add(unitObjectStatus);
                    unitObjectStatus.UnitItemDataList = new ForeignKeyObject<ItemData>(this.UnitItemDataList[i]);
                    unitObjectStatus.UnitSkillDataList = new ForeignKeyObject<SkillData>(this.UnitSkillDataList[i]);
                }

                return unitObjectStatusList;
            }
        }

        /// <summary>
        /// メモ：毎度DBにアクセスする必要はあるか？
        /// メリット：常にDBのデータを最新にするように組む必要が出てくる
        /// Get Unique Unit Status
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public UnitObjectStatus<T> GetUnitMainObejctStatusOnStage(int unitId, int stageId)
        {
            SqliteQueryBuilder builder = this.CreateQueryForUnitOnStage(stageId, false);
            builder.AddCondition("Id", unitId, typeof(UnitBaseStatusData), true);
            
            DataTable dataTable = SqliteHelper.Instance.GetSqliteObjectTable(builder);

            UnitSqliteSummary unitSqliteSummary = this.SetUnitStageSummary<UnitItemData, UnitSkillData>(dataTable, this.GetItemQueryBuider<UnitStageItemData>(), GetSkillQueryBuider<UnitStageSkillData>());

            return unitSqliteSummary.GetUnitObjectStatusList().First();
        }

        /// <summary>
        ///  get enemy
        /// </summary>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public List<UnitObjectStatus<T>> GetUnitSubObejctStatusListOnStage(int stageId, bool isStage)
        {
            SqliteQueryBuilder builder = this.CreateQueryForUnitOnStage(stageId, isStage);
            DataTable dataTable = SqliteHelper.Instance.GetSqliteObjectTable(builder);
            
            UnitSqliteSummary unitSqliteSummary = this.SetUnitStageSummary<UnitStageItemData, UnitStageSkillData>(dataTable, this.GetItemQueryBuider<UnitStageItemData>(), GetSkillQueryBuider<UnitStageSkillData>());

            return unitSqliteSummary.GetUnitObjectStatusList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stageId"></param>
        /// <param name="unitStageIdArray"></param>
        /// <returns></returns>
        public List<UnitObjectStatus<T>> GetUnitSubEventObjectList(int stageId, params int[] unitStageIdArray)
        {
            SqliteQueryBuilder builder = this.CreateQueryForUnitOnStage(stageId, true);
            builder.AddInCondition("Id", unitStageIdArray.Select<int, string>(u => u.ToString()).ToArray(), typeof(UnitStageStatusData), true);
            DataTable dataTable = SqliteHelper.Instance.GetSqliteObjectTable(builder);

            UnitSqliteSummary unitSqliteSummary = this.SetUnitStageSummary<UnitStageItemData, UnitStageSkillData>(dataTable, this.GetItemQueryBuider<UnitStageItemData>(), GetSkillQueryBuider<UnitStageSkillData>());

            return unitSqliteSummary.GetUnitObjectStatusList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stageId"></param>
        /// <returns></returns>
        private SqliteQueryBuilder CreateQueryForUnitOnStage(int stageId,bool isStage)
        {
            SqliteQueryBuilder builder = new SqliteQueryBuilder(typeof(UnitBaseStatusData));
            builder.AddJoinTable(typeof(UnitBaseStatusData), typeof(T), "Id", "UnitId");
            builder.AddJoinTable(typeof(UnitBaseStatusData), typeof(UnitStageStatusData), "Id", "UnitId");
            builder.AddCondition("StageId", stageId, typeof(UnitStageStatusData), true);
            if (isStage)
                builder.AddCondition("IsEvent", "1", typeof(UnitStageStatusData), true);
            builder.AddOrderByColumns("SortNo", typeof(UnitStageStatusData), true);
            return builder;
        }

        /// <summary>
        /// UnitBaseStatusに値を設定する
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private UnitSqliteSummary SetUnitStageSummary<ItemT, SkillT>(DataTable dataTable, Func<UnitStageStatusData, SqliteQueryBuilder> getItemQueryCallback, Func<UnitStageStatusData, SqliteQueryBuilder> getSkillQueryCallback)
            where ItemT : SqliteBaseData, IForeignKey
            where SkillT : SqliteBaseData, IForeignKey
        {
            UnitSqliteSummary unitSummary = new UnitSqliteSummary();
            unitSummary.UnitStatusList = DataBinding<T>.DataTableToObjectList(dataTable);
            unitSummary.UnitBaseStatusList = DataBinding<UnitBaseStatusData>.DataTableToObjectList(dataTable);
            unitSummary.UnitStageStatusList = DataBinding<UnitStageStatusData>.DataTableToObjectList(dataTable);
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
