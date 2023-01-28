using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky_Statistics.Model;

internal readonly struct GameStatistics
{

    public IReadOnlyList<IndividualGameStatisticsComponent> PerformedGamesStatistics { get; init; }
    public IReadOnlyList<Player> Participants { get; init; }

    /// <summary>
    /// The amount of games, where there was no winner, due to everyone being disqualified <u>You should cache this variable</u>
    /// </summary>
    public uint NoWinnerGamesCount
        => (uint)PerformedGamesStatistics.Count(g => g.Winner is null);

    /// <summary>
    /// The length of all games together
    /// </summary>
    public TimeSpan TotalGamesLength
        => PerformedGamesStatistics.Aggregate(TimeSpan.Zero, (prev, cur) => prev + cur.GameLength);

    public uint PlayedGamesCount { get; init; }

    /// <summary></summary>
    /// <param name="player"></param>
    /// <returns>The win percentage of a player from the statistics</returns>
    /// <exception cref="ArgumentException"></exception>
    public double GetWinRate(Player player)
    {
        var wonGames = PerformedGamesStatistics.Count(g => g.Winner == player);
        var winRate = (double)wonGames / PlayedGamesCount * 100.0d;
        return winRate;
    }
}