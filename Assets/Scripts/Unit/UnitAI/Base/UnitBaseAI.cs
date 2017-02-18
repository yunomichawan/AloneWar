using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Unit.Component;
using AloneWar.Stage;

namespace AloneWar.Unit.UnitAI.Base
{
    public abstract class UnitBaseAI
    {
        public int TargetMovePosition { get; set; }

        public int TargetUnitPosition { get; set; }

        public UnitSubComponent UnitSubComponent { get; set; }

        public UnitBaseAI(UnitSubComponent unitSubComponent)
        {
            this.UnitSubComponent = unitSubComponent;
        }

        public abstract void SetAITask();

        /// <summary>
        /// 
        /// </summary>
        public void SearchUnitInRange()
        {
            UnitSummaryComponent unitSummaryComponent = this.UnitSubComponent.UnitRange.SearchRange();
            // メインが存在する場合は最優先
            if (unitSummaryComponent.UnitMainComponent != null)
            {
                this.TargetUnitPosition = unitSummaryComponent.UnitMainComponent.PositionId;
            }
            else
            {
                int outRangeTarget = -1;
                int killTarget = -1;
                int lifeTarget = -1;
                foreach(UnitSubComponent unitSubComponent in unitSummaryComponent.UnitSubComponentList)
                {
                    //  kill
                    int atk = this.UnitSubComponent.UnitObjectStatus.BaseStatus.Attack - unitSubComponent.UnitObjectStatus.BaseStatus.Defence;
                    if (unitSubComponent.UnitObjectStatus.DamageHp - atk <= 0)
                    {
                        killTarget = unitSubComponent.PositionId;
                        break;
                    }
                    // out range
                    int atkRange = this.UnitSubComponent.UnitObjectStatus.BaseStatus.Range - unitSubComponent.UnitObjectStatus.BaseStatus.Range;
                    if (atkRange > 0)
                    {
                        outRangeTarget = unitSubComponent.PositionId;
                        break;
                    }


                }
                // 予測される結果を用いて最後に比較を行うような処理を実装したい(願望)
                // PredictionBattleResult
            }
        }
    }
}
