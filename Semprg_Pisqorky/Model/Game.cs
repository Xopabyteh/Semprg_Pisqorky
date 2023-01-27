using System.Diagnostics;
using Semprg_Pisqorky.Tiles;

namespace Semprg_Pisqorky.Model;

public partial class Game
{
    /// <summary>
    /// How many tiles must be in a line for it to be considered a win
    /// </summary>
    private const uint WINNING_LINE = 5;
    private readonly Drawer drawer;
    private readonly Board board;

    /// <summary>
    /// Players in random order
    /// </summary>
    private readonly IReadOnlyList<Player> players;

    public Game(Drawer drawer, Board board, IReadOnlyList<Player> players)
    {
        this.drawer = drawer;
        this.board = board;

        this.players = PlayersInRandomOrder(players);
    }

    private IReadOnlyList<Player> PlayersInRandomOrder(IReadOnlyList<Player> players)
    {
        var random = new Random();
        return players.OrderBy(x => random.Next()).ToList();
    }

    private Player Winner;

    public void SimulateGame()
    {
        //Before loop
        drawer.ShiftBatchNumber();

        while (Loop())
        {
            //After loop
            drawer.PopAll();

            drawer.ShiftBatchNumber();
        }
        
        //Game ended
        drawer.PopAll();
        Console.ReadLine();
    }
    /// <summary>
    /// </summary>
    /// <returns><b>True</b> - game should continue <b>False</b> - game should end</returns>
    private bool Loop()
    {
        drawer.PushHeader();

        foreach (var player in players)
        {
            var gameView = new GameView(board);

            var playerMove = player.PlayerStrategy.GetPlayerMove(gameView);
            var isMoveLegal = PlayPlayerMove(player, playerMove);
            //Todo: work with move legality

            board.Draw(drawer);
            if (CheckForWin())
            {
                drawer.PushWinMessage($"Winning move: {playerMove.Position} by {player.Nickname}");
                return false;
            }

        }

        return true;
    }

    /// <summary></summary>
    /// <returns><b>True</b> - move was successful <b>False</b> - move was illegal </returns>
    private bool PlayPlayerMove(Player player, PlayerMove playerMove)
    {
        //Todo: optimize with dictionary marshal operations
        if (board.TileSet.TryGetValue(playerMove.Position, out var tile))
        {
            //Tile exists

            return false;
        }

        //Tile doesn't yet exist
        board.TileSet.Add(playerMove.Position, new TraditionalTile(player, playerMove.Position));
        return true;
    }
}