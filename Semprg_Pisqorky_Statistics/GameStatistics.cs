using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky_Statistics;

internal struct GameStatistics
{
    /// <summary>
    /// <b>Key</b>: Player, <b>Value</b>: Amount of wins
    /// </summary>
    public Dictionary<Player, uint> WinStatistics { get; init; }

    /// <summary>
    /// The amount of games, where there was no winner, due to everyone being disqualified
    /// </summary>
    public uint NoWinnerGamesCount { get; init; }

    public uint PlayedGamesCount { get; init; }  

    public GameStatistics(Dictionary<Player, uint> winStatistics, uint noWinnerGamesCount, uint playedGamesCount)
    {
        WinStatistics = winStatistics;
        NoWinnerGamesCount = noWinnerGamesCount;
        PlayedGamesCount = playedGamesCount;
    }

    /// <summary></summary>
    /// <param name="player"></param>
    /// <returns>The win percentage of a player from the statistics</returns>
    /// <exception cref="ArgumentException"></exception>
    public double GetWinRate(Player player)
    {
        //Todo: optimize with dictionary marshal operations
        if (WinStatistics.TryGetValue(player, out var winsCount))
        {
            return (double) winsCount / PlayedGamesCount * 100d;
        }

        throw new ArgumentException($"{nameof(WinStatistics)} does not contain player given", nameof(player));
    }
}