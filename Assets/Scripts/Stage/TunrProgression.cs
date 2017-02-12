using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common.Extensions;
using AloneWar.Common.TaskHelper;
using AloneWar.Common;
using UnityEngine;
using UnityEngine.UI;

namespace AloneWar.Stage
{
    public class TunrProgression : TaskCoroutineBeahavior
    {
        [SerializeField]
        private Text text;

        public void TurnStart(TurnCategory turnCategory)
        {
            switch (turnCategory)
            {
                case TurnCategory.Player:
                    break;
                case TurnCategory.PlayerUnit:
                    break;
                case TurnCategory.Enemy:
                    break;
                case TurnCategory.EnemyUnit:
                    break;
                case TurnCategory.Another:
                    break;
            }
        }
    }
}
