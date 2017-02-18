using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Unit.Status;

namespace AloneWar.Battle
{
    public class PredictionBattleResult
    {
        public UnitBaseStatus AttackUnitBaseStatus { get; set; }

        public UnitBaseStatus DefenceUnitBaseStatus { get; set; }

        public PredictionBattleResult(UnitBaseStatus attackUnitBaseStatus, UnitBaseStatus defenceUnitBaseStatus)
        {
            //this.AttackUnitBaseStatus = attackUnitBaseStatus;
            //this.DefenceUnitBaseStatus = defenceUnitBaseStatus;
        }

    }

    public class BattleResultInfo
    {
        #region property

        public int Damage { get; set; }

        public float HitPercent { get; set; }

        public float AvoidPercent { get; set; }

        public bool IsKill { get; set; }

        #endregion

        public BattleResultInfo(UnitBaseStatus attack, UnitBaseStatus defence)
        {
            this.Damage = defence.BaseStatus.Defence - attack.BaseStatus.Attack;
            
        }
    }
}
