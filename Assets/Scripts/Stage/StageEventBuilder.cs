using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common.TaskHelper;
using AloneWar.DataObject.Sqlite.SqliteObject.Master;
using AloneWar.DataObject.Sqlite.SqliteObject.Transaction;
using AloneWar.Common;
using AloneWar.Stage.Event.Sender;
using UnityEngine;

namespace AloneWar.Stage
{
    public class StageEventBuilder : TaskCoroutineBeahavior
    {
        public IEnumerator EventDataTaskRun(EventData eventTableData)
        {
            EventCategory eventCategory = (EventCategory)eventTableData.EventCategory;
            switch (eventCategory)
            {
                case EventCategory.GetUnit:
                    break;
                case EventCategory.GetItem:
                    break;
                case EventCategory.GetSkill:
                    break;
                case EventCategory.AiChange:
                    break;
                case EventCategory.SideChange:
                    break;
                case EventCategory.UnitDamage:
                    break;
                case EventCategory.UnitKill:
                    break;
                case EventCategory.UnitSummon:
                    break;
                case EventCategory.PositionClose:
                    break;
                case EventCategory.Talk:
                    break;
            }

            yield return null;
        }

        #region EventList

        private IEnumerator EventGetUnit()
        {
            yield return null;
        }

        private IEnumerator EventGetItem()
        {
            yield return null;
        }

        private IEnumerator EventGetSkill()
        {
            yield return null;
        }

        private IEnumerator EventAiChange()
        {
            yield return null;
        }

        #endregion
    }
}
