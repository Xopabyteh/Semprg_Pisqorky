using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Semprg_Pisqorky;
using Semprg_Pisqorky.Boards;
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

    public GameStatistics TestStrategies()
    {
        var performedGamesStatistics = new IndividualGameStatisticsComponent[testAmount];

        //Play games
        for (int i = 0; i < testAmount; i++)
        {
            //Initialize game
            var traditionalBoard = new TraditionalBoard();
            var game = new Game(drawer, traditionalBoard, participants);

            //Start stopwatch
            stopwatch.Restart();

            //Simulate game
            var winner = game.SimulateGame();

            //Stop stopwatch
            stopwatch.Stop();

            //Write to statistics
            performedGamesStatistics[i] = new IndividualGameStatisticsComponent()
            {
                GameLength = stopwatch.Elapsed,
                Winner = winner
            };
        }

        return new GameStatistics()
        {
            PlayedGamesCount = testAmount,
            PerformedGamesStatistics = performedGamesStatistics,
            Participants = participants
        };
    }
}