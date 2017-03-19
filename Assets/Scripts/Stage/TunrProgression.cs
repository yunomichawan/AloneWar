using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AloneWar.Common.Extensions;
using AloneWar.Common.TaskHelper;
using AloneWar.Common;
using UnityEngine;
using UnityEngine.UI;
using AloneWar.Stage.Component;
using AloneWar.Unit.Component;

namespace AloneWar.Stage
{
    public class TunrProgression : MonoBehaviour
    {

        public void TurnStart(TurnCategory turnCategory)
        {
            UnitSummaryComponent unitSummaryComponent = new UnitSummaryComponent();

            switch (turnCategory)
            {
                case TurnCategory.Player:
                    unitSummaryComponent = StageManager.Instance.StageInformation.SearchUnit(UnitSideCategory.Player);
                    break;
                case TurnCategory.PlayerUnit: //
                    break;
                case TurnCategory.Enemy:
                    unitSummaryComponent = StageManager.Instance.StageInformation.SearchUnit(UnitSideCategory.Enemy);
                    break;
                case TurnCategory.EnemyUnit: //
                    break;
                case TurnCategory.Another: //
                    break;
            }
        }

        private void UnitInitWhenTurnStartEnd(List<BaseStageObject> baseStageObjectList)
        {
                
        }

        private void UnitInitWhenTurnEnd()
        {

        }
    }
}
