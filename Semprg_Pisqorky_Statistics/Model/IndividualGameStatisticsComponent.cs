using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky_Statistics.Model;

internal readonly struct IndividualGameStatisticsComponent
{
    public GameResult GameResult { get; init; }

    public TimeSpan GameLength { get; init; }
}