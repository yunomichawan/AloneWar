using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AloneWar.Common;

namespace AloneWar.Stage.Controller.Unit
{
    public class UnitCommandHistory
    {
        public CommandCategory CommandCategory { get; set; }

        public UnitCommandHistory(CommandCategory commandCategory)
        {
            this.CommandCategory = commandCategory;
        }
    }
}
