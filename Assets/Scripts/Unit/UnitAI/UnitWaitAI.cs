using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Unit.Component;
using AloneWar.Unit.UnitAI.Base;
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
            // 組み合わせるのみでタスクを作成する"予定"


            // Dubug Code
            StageManager.Instance.TaskQueue.Enqueue(null);
        }
    }
}
