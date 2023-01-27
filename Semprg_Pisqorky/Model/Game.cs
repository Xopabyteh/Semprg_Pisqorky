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
    /// Players established at the start of the game, list doesn't change
    /// </summary>
    private readonly IReadOnlyList<Player> participants;

    /// <summary>
    /// Players that are active. This list might change due to disqualification
    /// </summary>
    private List<Player> activePlayers;

    public Game(Drawer drawer, Board board, IReadOnlyList<Player> players)
    {
        this.drawer = drawer;
        this.board = board;

        this.participants = PlayersInRandomOrder(players);
        activePlayers = new List<Player>(participants);
    }

    private IReadOnlyList<Player> PlayersInRandomOrder(IReadOnlyList<Player> playersToShuffle)
    {
        var random = new Random();
        return playersToShuffle.OrderBy(x => random.Next()).ToList();
    }

    private Player? winner;

    /// <summary></summary>
    /// <returns>The winner of the simulation. Can return <b>null</b>, if all players are disqualified</returns>
    public Player? SimulateGame()
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
        return winner;
    }
    /// <summary>
    /// </summary>
    /// <returns><b>True</b> - game should continue <b>False</b> - game should end</returns>
    private bool Loop()
    {
        drawer.PushHeader();

        var playersToDisqualify = new List<Player>(activePlayers.Count);
        foreach (var player in activePlayers)
        {
            var gameView = new GameView(board, activePlayers);

            var playerMove = player.PlayerStrategy.GetPlayerMove(gameView);
            var isMoveLegal = PlayPlayerMove(player, playerMove);
            
            board.Draw(drawer);

            //Disqualify for making an illegal move:
            if (!isMoveLegal)
            {
                playersToDisqualify.Add(player);
                continue;
            }

            if (CheckForWin())
            {
                winner = player;
                drawer.PushWinMessage($"Winning move: {playerMove.Position} by {player.Nickname}");
                return false;
            }

        }

        foreach (var player in playersToDisqualify)
        {
            activePlayers.Remove(player);
        }

        if (activePlayers.Count < 2)
        {
            winner = activePlayers.FirstOrDefault();
            return false;
        }

        return true;
    }

    /// <summary>
    /// Attempts to perform the move retrieved from the player. If the player decides to use a tile position that has no tile to it yet, it will create a new <b>TraditionalTile</b>
    /// </summary>
    /// <returns><b>True</b> - move was successful <b>False</b> - move was illegal </returns>
    private bool PlayPlayerMove(Player player, PlayerMove playerMove)
    {
        //Todo: optimize with dictionary marshal operations
        if (board.TileSet.TryGetValue(playerMove.Position, out var tile))
        {
            //Tile exists

            //Player tried to replace an occupied tile
            if(tile is TraditionalTile)
                return false;

        }

        //Tile doesn't yet exist
        board.TileSet.Add(playerMove.Position, new TraditionalTile(player, playerMove.Position));
        return true;
    }
}