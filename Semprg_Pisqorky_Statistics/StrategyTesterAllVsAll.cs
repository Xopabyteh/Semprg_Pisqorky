using Semprg_Pisqorky.Boards;
using Semprg_Pisqorky.Model;
using Semprg_Pisqorky.Services;

namespace Semprg_Pisqorky_Statistics;

internal class StrategyTesterAllVsAll
{
    private readonly uint testAmount;
    private readonly IReadOnlyList<Player> participants;

    public StrategyTesterAllVsAll(uint testAmount, IReadOnlyList<Player> participants)
    {
        this.testAmount = testAmount;
        this.participants = participants;
    }

    public GameStatistics TestStrategies()
    {
        //Todo: optimize with dictionary marshal operations
        var winStatistics = new Dictionary<Player, uint>();
        uint noWinnerGamesAmount = 0;
        
        foreach (var participant in participants)
        {
            winStatistics[participant] = 0;
        }

        //Play games
        for (int i = 0; i < testAmount; i++)
        {
            var consoleDrawer = new NullDrawer();
            var traditionalBoard = new TraditionalBoard();


            var game = new Game(consoleDrawer, traditionalBoard, participants);
            var winner = game.SimulateGame();
            if (winner is null)
                noWinnerGamesAmount++;
            else
                winStatistics[winner]++;
        }

        return new GameStatistics(winStatistics, noWinnerGamesAmount, testAmount);
    }
}