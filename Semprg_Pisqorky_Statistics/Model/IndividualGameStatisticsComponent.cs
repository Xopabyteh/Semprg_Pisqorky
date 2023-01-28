using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky_Statistics.Model;

internal readonly struct IndividualGameStatisticsComponent
{
    public Player? Winner { get; init; }

    public TimeSpan GameLength { get; init; }
}