using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common.TaskHelper;
using UnityEngine;
using AloneWar.Unit.Component;
using AloneWar.DataObject.Sqlite.SqliteObject;
using AloneWar.Stage.FieldObject;
using AloneWar.Stage.Component;

namespace AloneWar.Stage
{
    public class StageObjectController : TaskCoroutineBeahavior
    {
        

        public IEnumerator Move()
        {
            yield return null;


        }

        public void UpdateObjectPosition(BaseStageObject baseStageObject, int positionId)
        {
            baseStageObject.BeforePositionId = baseStageObject.PositionId;
            baseStageObject.PositionId = positionId;

        }

    }
}
