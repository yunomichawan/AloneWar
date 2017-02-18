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
            List<UnitSubComponent> unitSubComponentList = new List<UnitSubComponent>();

            switch (turnCategory)
            {
                case TurnCategory.Player:
                    unitSubComponentList = StageManager.Instance.StageInformation.UnitSubComponentList.Values.Where(u => u.UnitObjectStatus.StageStatus.UnitSide.Equals(UnitSideCategory.Player)).ToList();
                    break;
                case TurnCategory.PlayerUnit: //
                    break;
                case TurnCategory.Enemy:
                    unitSubComponentList = StageManager.Instance.StageInformation.UnitSubComponentList.Values.Where(u => u.UnitObjectStatus.StageStatus.UnitSide.Equals(UnitSideCategory.Enemy)).ToList();
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
