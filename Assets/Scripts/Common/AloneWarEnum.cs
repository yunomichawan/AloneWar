using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AloneWar.Common
{
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
    public enum AiCategory
    {
        Wait,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CommandCategory
    {
        Move,
        Battle,
        MpSummon,
        HpSummon,
        HalfSummon,
        Trash,
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
    public enum UnitMainCategory
    {

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
    public enum UnitSubSummonCategory
    {
        SummonHp,
        SummonMp,
        SummonHalf,
    }

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
        PositionStopSpecificUnit, // trigger set 
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

}
