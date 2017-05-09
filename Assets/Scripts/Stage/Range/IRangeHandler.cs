using AloneWar.Common;

namespace AloneWar.Stage.Range
{
    /// <summary>
    /// 範囲設定機能を実装
    /// </summary>
    public interface IRangeHandler
    {
        StageRange StageRange { get; set; }

        int PositionId { get; }

        /// <summary>
        /// 移動値(移動できない場合は0)
        /// </summary>
        int MainRange { get; }

        int SubRange { get; }

        int InvalidRange { get; }

        CommandCategory MainCommand { get; }

        CommandCategory SubCommand { get; }

        UnitSideCategory UnitSide { get; }
    }
}