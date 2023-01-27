using Semprg_Pisqorky.Model;
using Semprg_Pisqorky.PlayerStrategies;
using Semprg_Pisqorky_Statistics;

var players = new List<Player>
{
    new ("Křížky", 'x', new RandomStrategy()),
    new ("Kolečka", 'o', new RandomStrategy())
};

var strategyTester = new StrategyTesterAllVsAll(10, players);
var result = strategyTester.TestStrategies();
foreach (var player in players)
{
    Console.WriteLine($"{player.Nickname} - {player.Shape}: {result.GetWinRate(player)}%");
}