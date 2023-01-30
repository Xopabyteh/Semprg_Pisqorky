using Semprg_Pisqorky.Boards;
using Semprg_Pisqorky.GameVariants;
using Semprg_Pisqorky.Model;
using Semprg_Pisqorky.PlayerStrategies;
using Semprg_Pisqorky.Services;
using Semprg_Pisqorky_Statistics.Services;

var players = new List<Player>
{
    new ("Křížky", 'x', new RandomStrategy()),
    new ("Kolečka", 'o', new RandomStrategy()),
};

var strategyTester = new StrategyTesterAllVsAll(
    testAmount: 10, 
    participants: players, 
    drawer: new ConsoleDrawer());

var gameStatistics = strategyTester.TestStrategies<SingleSwapGame, TraditionalBoard>();
var statisticsProcessor = new GameStatisticsProcessor(gameStatistics);

statisticsProcessor.LogStatistics();
