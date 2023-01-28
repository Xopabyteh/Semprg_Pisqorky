using Semprg_Pisqorky_Statistics.Model;

namespace Semprg_Pisqorky_Statistics.Services;

internal class GameStatisticsProcessor
{
    private readonly GameStatistics gameStatistics;

    public GameStatisticsProcessor(GameStatistics gameStatistics)
    {
        this.gameStatistics = gameStatistics;
    }


    public void LogStatistics()
    {
        Console.WriteLine($"Played - {gameStatistics.PlayedGamesCount} games");
        Console.WriteLine($"Amount of games with no winners - {gameStatistics.NoWinnerGamesCount}");

        Console.WriteLine("\n---\nPlayer winrates:");
        foreach (var player in gameStatistics.Participants)
        {
            Console.WriteLine($"{player.Nickname} - {player.Shape}: {gameStatistics.GetWinRate(player)}%");
        }

        Console.WriteLine("\n---\nGame lengths");
        Console.WriteLine($"All games together took {gameStatistics.TotalGamesLength}");
        for (var i = 0; i < gameStatistics.PerformedGamesStatistics.Count; i++)
        {
            var performedGame = gameStatistics.PerformedGamesStatistics[i];
            Console.WriteLine($"Game #{i} took {performedGame.GameLength}, {performedGame.Winner?.Nickname ?? "null"} won");
        }
    }
}