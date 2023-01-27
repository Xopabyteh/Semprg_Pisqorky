using Semprg_Pisqorky.Boards;
using Semprg_Pisqorky.Model;
using Semprg_Pisqorky.PlayerStrategies;
using Semprg_Pisqorky.Services;

var consoleDrawer = new ConsoleDrawer();
var traditionalBoard = new TraditionalBoard();
var players = new List<Player>
{
    new ("Křížky", 'x', new RandomStrategy()),
    new ("Kolečka", 'o', new RandomStrategy())
};

var game = new Game(consoleDrawer, traditionalBoard, players);
game.SimulateGame();