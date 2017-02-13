using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AloneWar.Stage
{
    public interface IStagePosition
    {
        int X { get; set; }

        int Y { get; set; }

        int PositionId { get; set; }
    }
}
