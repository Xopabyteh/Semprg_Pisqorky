using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky_Statistics.Model;

internal readonly struct GameStatistics
{

    public IReadOnlyList<IndividualGameStatisticsComponent> IndividualGamesStatistics { get; init; }
    public IReadOnlyCollection<Player> Participants { get; init; }

    /// <summary>
    /// <u>You should cache this</u>
    /// The length of all games together
    /// </summary>
    public TimeSpan TotalGamesLength
        => IndividualGamesStatistics.Aggregate(TimeSpan.Zero, (prev, cur) => prev + cur.GameLength);

    public uint PlayedGamesCount { get; init; }

    /// <summary></summary>
    /// <param name="player"></param>
    /// <returns>The win percentage of a player from the statistics</returns>
    /// <exception cref="ArgumentException"></exception>
    public double GetWinRate(Player player)
    {
        var wonGames = IndividualGamesStatistics.Count(g => g.GameResult.Winner == player);
        var winRate = (double)wonGames / PlayedGamesCount * 100.0d;
        return winRate;
    }
}