using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AloneWar.Common
{
    /// <summary>
    /// 
    /// </summary>
    public enum RangeDirection
    {
        Top,
        Bottom,
        Right,
        Left,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum BindCategory
    {
        Sqlite,
        Server,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CommandCategory
    {
        Attack,
        Move,
        Battle,
        Summon,
        MpSummon,
        HpSummon,
        HalfSummon,
        Trash,
        None,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TurnCategory
    {
        Player,
        PlayerUnit,
        Enemy,
        EnemyUnit,
        Another,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum UnitSubSummonCategory
    {
        SummonHp,
        SummonMp,
        SummonHalf,
    }

    #region Sqlite Master Code

    /// <summary>
    /// 
    /// </summary>
    public enum ResourceCategory
    {
        UnitPrefab,
        Label,
        None,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum UnitMainCategory
    {
        /// <summary>
        /// 少数精鋭
        /// </summary>
        SelectFew,
        /// <summary>
        /// 大群
        /// </summary>
        Horge,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum UnitSideCategory
    {
        Player,
        Enemy,
        Another,
        None,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AiCategory
    {
        Wait,
        InRange,
        LowHp,
        TargetMain,
        Simple,
        Fool,
        /// <summary>
        /// 以下、初期開発では実装しない
        /// </summary>
        Cooperation,
        SacrificedPiece,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SkillCategory
    {
        /// <summary>
        /// 常駐型
        /// </summary>
        Resident,
        /// <summary>
        /// 発動
        /// </summary>
        Invocation,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EventTriggerCategory
    {
        TurnOver, // trigger set 
        PositionStop, // trigger set 
        PositionStopUniqueUnit, // trigger set 
        PositionClose, // trigger set 
        AreaStop, // trigger set 
        UnitKill, // trigger set 
        TargetUnitKill, // trigger set 
        UnitAdjacent, // trigger set 
        StageStart, // trigger set 
        StageEnd, // trigger set 
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EventCategory
    {
        GetUnit,
        GetItem,
        GetSkill,
        Talk,
        UnitDamage,
        UnitKill,
        /* 生成するユニットはどのように値を持つ？ */
        UnitSummon,
        PositionClose,
        AiChange,
        SideChange,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum StageClearCategory
    {
        TargetUnitKill,
    }

    #endregion

}
