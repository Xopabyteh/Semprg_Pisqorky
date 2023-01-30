using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Semprg_Pisqorky;
using Semprg_Pisqorky.Boards;
using Semprg_Pisqorky.GameVariants;
using Semprg_Pisqorky.Model;
using Semprg_Pisqorky.Services;
using Semprg_Pisqorky_Statistics.Model;

namespace Semprg_Pisqorky_Statistics.Services;

internal class StrategyTesterAllVsAll
{
    private readonly uint testAmount;
    private readonly IReadOnlyList<Player> participants;
    private readonly Drawer drawer;
    private readonly Stopwatch stopwatch;

    public StrategyTesterAllVsAll(uint testAmount, IReadOnlyList<Player> participants, Drawer drawer)
    {
        this.testAmount = testAmount;
        this.participants = participants;
        this.drawer = drawer;
        stopwatch = new Stopwatch();
    }

    public GameStatistics TestStrategies<TGame, TBoard>() 
        where TGame : TraditionalGame 
        where TBoard : Board,new()
    {
        var performedGamesStatistics = new IndividualGameStatisticsComponent[testAmount];

        //Play games
        for (int i = 0; i < testAmount; i++)
        {
            //Initialize game
            //var game = new TG(drawer, board, participants);
            var board = new TBoard();
            var game = Activator.CreateInstance(typeof(TGame), drawer, board, participants) as TGame;
            Debug.Assert(game != null, nameof(game) + " != null");
            
            //Start stopwatch
            stopwatch.Restart();

            //Simulate game
            var gameResult = game.SimulateGame();

            //Stop stopwatch
            stopwatch.Stop();

            //Write to statistics
            performedGamesStatistics[i] = new IndividualGameStatisticsComponent()
            {
                GameLength = stopwatch.Elapsed,
                GameResult = gameResult
            };
        }

        return new GameStatistics()
        {
            PlayedGamesCount = testAmount,
            IndividualGamesStatistics = performedGamesStatistics,
            Participants = participants
        };
    }
}