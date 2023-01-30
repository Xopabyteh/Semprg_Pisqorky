using System.Diagnostics;
using System.Text;
using Semprg_Pisqorky.GameVariants;
using Semprg_Pisqorky.Model;
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
        var statisticsStringBuilder = new StringBuilder();

        //General data
        statisticsStringBuilder.AppendLine($"Played - {gameStatistics.PlayedGamesCount} games");
        statisticsStringBuilder.AppendLine($"Games with a draw - {gameStatistics.IndividualGamesStatistics.Count(g=>g.GameResult.FinalState == GameState.Draw)}");
        statisticsStringBuilder.AppendLine($"Games with all players disqualified - {gameStatistics.IndividualGamesStatistics.Count(g=>g.GameResult.FinalState == GameState.AllDisqualified)}");
        
        //Game time
        var totalGamesLength = gameStatistics.TotalGamesLength;
        statisticsStringBuilder.AppendLine($"All games together took {totalGamesLength} with an average of {totalGamesLength / gameStatistics.PlayedGamesCount} per game");

        //Win rates
        statisticsStringBuilder.AppendLine("\n---\nPlayer winrates:");
        foreach (var player in gameStatistics.Participants)
        {
            statisticsStringBuilder.AppendLine($"{player.Nickname} - {player.Shape}: {gameStatistics.GetWinRate(player)}%");
        }

        statisticsStringBuilder.AppendLine("\n---\nIndividual games");
        
        //Individual games
        for (var i = 0; i < gameStatistics.IndividualGamesStatistics.Count; i++)
        {
            statisticsStringBuilder.AppendLine("|");
            
            var performedGame = gameStatistics.IndividualGamesStatistics[i];
            
            //Determine game state
            string resultMsg;
            switch (performedGame.GameResult.FinalState)
            {
                case GameState.Draw:
                    resultMsg = "ended in a draw";
                    break;
                case GameState.Winner:
                    Debug.Assert(performedGame.GameResult.Winner != null, "performedGame.GameResult.Winner != null");
                    resultMsg = $"winner was {performedGame.GameResult.Winner.Nickname}";
                    break;
                case GameState.AllDisqualified:
                    resultMsg = $"everyone was disqualified";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            //General
            //i+1 -> index from 1
            statisticsStringBuilder.AppendLine($"|-[{performedGame.GameResult.FinalState}] Game #{i+1} took {performedGame.GameLength}, {resultMsg}");
            
            //Disqualifications
            foreach (var disqualifiedPlayer in performedGame.GameResult.DisqualifiedPlayers)
            {
                statisticsStringBuilder.AppendLine($"|---{disqualifiedPlayer.Nickname} was disqualified");
            }
        }

        Console.WriteLine(statisticsStringBuilder);
    }
}