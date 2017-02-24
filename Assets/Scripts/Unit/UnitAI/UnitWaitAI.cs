using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Unit.Component;
using AloneWar.Unit.UnitAI.Base;
using AloneWar.Common;
using AloneWar.Stage;

namespace AloneWar.Unit.UnitAI
{
    public class UnitWaitAI : UnitBaseAI
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitSubComponent"></param>
        public UnitWaitAI(UnitSubComponent unitSubComponent)
            : base(unitSubComponent)
        {

        }

        public override void SetAITask()
        {
            this.UnitSubComponent.UnitRange.SetRange(CommandCategory.Attack, CommandCategory.None, this.UnitSubComponent.SubRange, 0, this.UnitSubComponent.InvalidRange, 0);
            this.SetTarget();
            // Dubug Code
            StageManager.Instance.TaskQueue.Enqueue(null);
        }

        /// <summary>
        ///  座標決定
        /// </summary>
        protected override void SearchUnitFromAI()
        {
            this.TargetMovePosition = this.UnitSubComponent.PositionId;
        }
    }
}
