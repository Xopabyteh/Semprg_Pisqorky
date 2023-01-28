using Semprg_Pisqorky.Model;
using Semprg_Pisqorky.PlayerStrategies;
using Semprg_Pisqorky.Services;
using Semprg_Pisqorky_Statistics;
using Semprg_Pisqorky_Statistics.Services;

var players = new List<Player>
{
    new ("Křížky", 'x', new RandomStrategy()),
    new ("Kolečka", 'o', new RandomStrategy())
};

var strategyTester = new StrategyTesterAllVsAll(
    testAmount: 10, 
    participants: players, 
    drawer: new NullDrawer());

var gameStatistics = strategyTester.TestStrategies();
var statisticsProcessor = new GameStatisticsProcessor(gameStatistics);
statisticsProcessor.LogStatistics();