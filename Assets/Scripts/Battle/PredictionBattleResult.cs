using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common;
using AloneWar.Unit.Status;

namespace AloneWar.Battle
{
    /// <summary>
    /// 予測結果
    /// </summary>
    public class PredictionBattleResult
    {
        /// <summary>
        /// 結果期待値
        /// </summary>
        public float ResultExpectedValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// 仮座標ID
        /// </summary>
        public int TempPositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BattleResultInfo AttackBattleResultInfo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public BattleResultInfo DefenceBattleResultInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attackUnitBaseStatus"></param>
        /// <param name="defenceUnitBaseStatus"></param>
        public PredictionBattleResult(UnitBaseStatus attackUnitBaseStatus, UnitBaseStatus defenceUnitBaseStatus, int distance, int tempPositionId)
        {
            this.Distance = distance;
            this.TempPositionId = tempPositionId;
            this.AttackBattleResultInfo = new BattleResultInfo(attackUnitBaseStatus, defenceUnitBaseStatus, distance);
            this.DefenceBattleResultInfo = new BattleResultInfo(defenceUnitBaseStatus, attackUnitBaseStatus, distance);
            this.CountResultExpectedValue();
        }

        /// <summary>
        /// 結果期待値を計算
        /// </summary>
        private void CountResultExpectedValue()
        {
            float count = 0;
            if (this.DefenceBattleResultInfo.IsKill)
            {
                // 最大値が決まっていない為、仮の値
                count = 10000;
            }
            else
            {
                // +与
                count += this.AttackBattleResultInfo.HitPercent / 100 * this.AttackBattleResultInfo.Damage;
                count += this.AttackBattleResultInfo.AvoidPercent;

                if (this.DefenceBattleResultInfo.CanAttack)
                {
                    // -被
                    count -= AloneWarConst.MaxPercent - this.DefenceBattleResultInfo.HitPercent / 100 * this.DefenceBattleResultInfo.Damage;
                    count -= AloneWarConst.MaxPercent - this.DefenceBattleResultInfo.AvoidPercent;
                }
            }

            this.ResultExpectedValue = count;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BattleResultInfo
    {
        #region display property

        public int Hp { get { return this.UnitBaseStatus.DamageHp; } }

        public int ResultHp { get { return this.Hp - this.Damage; } }

        public int Damage { get; set; }

        public float HitPercent
        {
            get
            {
                return this.hitPercent;
            }
            set
            {
                this.hitPercent = value;
                this.hitPercent = hitPercent > AloneWarConst.MaxPercent ? AloneWarConst.MaxPercent : hitPercent;
            }
        }
        private float hitPercent = 0;

        public float AvoidPercent
        {
            get
            {
                return this.avoidPercent;
            }
            set
            {
                this.avoidPercent = value;
                this.avoidPercent = this.avoidPercent > AloneWarConst.MaxPercent ? AloneWarConst.MaxPercent : this.avoidPercent;
            }
        }
        private float avoidPercent = 0;

        public bool IsKill { get; set; }

        public bool CanAttack { get; set; }

        #endregion

        public UnitBaseStatus UnitBaseStatus { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="attack"></param>
        /// <param name="defence"></param>
        public BattleResultInfo(UnitBaseStatus attack, UnitBaseStatus defence, int distance)
        {
            // 有効範囲の場合
            if (attack.BaseStatus.Range >= distance && distance > attack.BaseStatus.InvalidRange)
            {
                this.Damage = defence.BaseStatus.Defence - attack.BaseStatus.Attack;
                this.CanAttack = true;
                this.HitPercent = attack.BaseStatus.Hit - defence.BaseStatus.Avoid;
                this.IsKill = defence.BaseStatus.Hp <= this.Damage + defence.StageStatus.Damage;
            }
            else
            {
                this.Damage = 0;
                this.CanAttack = false;
                this.IsKill = false;
            }
            this.AvoidPercent = defence.BaseStatus.Hit - attack.BaseStatus.Avoid;
            this.UnitBaseStatus = attack;
        }
    }
}
