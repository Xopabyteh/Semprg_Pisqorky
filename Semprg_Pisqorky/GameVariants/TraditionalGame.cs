using System.Data;
using Semprg_Pisqorky.Model;
using Semprg_Pisqorky.Tiles;

namespace Semprg_Pisqorky.GameVariants;

public class TraditionalGame
{
    /// <summary>
    /// How many tiles must be in a line for it to be considered a win
    /// </summary>
    protected readonly Drawer drawer;
    protected readonly Board board;

    /// <summary>
    /// Players established at the start of the game, list doesn't change
    /// </summary>
    private readonly IReadOnlyList<Player> participants;

    /// <summary>
    /// Players that are active. This list might change due to disqualification
    /// </summary>
    protected List<Player> activePlayers;

    public TraditionalGame(Drawer drawer, Board board, IReadOnlyList<Player> participants)
    {
        this.drawer = drawer;
        this.board = board;

        this.participants = PlayersInRandomOrder(participants);
        this.activePlayers = new List<Player>(this.participants);
        this.disqualifiedPlayers = new List<Player>(participants.Count);
    }

    private IReadOnlyList<Player> PlayersInRandomOrder(IReadOnlyList<Player> playersToShuffle)
    {
        var random = new Random();
        return playersToShuffle.OrderBy(x => random.Next()).ToList();
    }

    private Player? winner;
    private GameState gameState;
    private List<Player> disqualifiedPlayers;

    /// <summary></summary>
    /// <returns>The winner of the simulation. Can return <b>null</b>, if the game ends in a <b>draw</b> all players are <b>disqualified</b></returns>
    public virtual GameResult SimulateGame()
    {
        //Before loop
        while (Loop())
        {
            //After loop
        }

        //Game ended
        if (gameState == GameState.Ongoing)
            throw new ConstraintException("Game finished with an ongoing state");
        
        drawer.PopAll();
        drawer.NewGame();

        return new GameResult()
        {
            FinalState = gameState,
            Winner = winner,
            DisqualifiedPlayers = disqualifiedPlayers
        };
    }
    /// <summary>
    /// </summary>
    /// <returns><b>True</b> - game should continue <b>False</b> - game should end</returns>
    protected virtual bool Loop()
    {
        var playersToDisqualify = new List<Player>(activePlayers.Count);
        foreach (var player in activePlayers)
        {
            drawer.PushHeader($"{player.Nickname}s turn");
            
            var gameView = new GameView(board, activePlayers, RequiredActionType.GiveNextPosition);

            var playerMove = player.PlayerStrategy.GetPlayerMove(gameView);
            var isMoveLegal = PlayPlayerMove(player, playerMove);

            board.Draw(drawer);

            //Disqualify for making an illegal move:
            if (!isMoveLegal)
            {
                drawer.PushHeader($"Player {player.Nickname} made an illegal move: {playerMove.Position}");
                playersToDisqualify.Add(player);
                continue;
            }

            gameState = board.GetGameState();
            

            if (gameState == GameState.Winner)
            {
                winner = player;
                drawer.PushHeader($"Winning move: {playerMove.Position} by {player.Nickname}");
                return false;
            }
            if (gameState == GameState.Draw)
            {
                winner = null;
                drawer.PushHeader($"Game ended with a draw");
                return false;
            }

            drawer.PopAll();
        }

        //Disqualify players
        foreach (var player in playersToDisqualify)
        {
            activePlayers.Remove(player);
            disqualifiedPlayers.Add(player);
        }

        //Account for disqualifications
        if (activePlayers.Count == 0)
        {
            winner = null;
            gameState = GameState.AllDisqualified;
            return false;
        }

        if (activePlayers.Count == 1)
        {
            winner = activePlayers.First();
            gameState = GameState.Winner;
            return false;
        }

        return true;
    }

    /// <summary>
    /// Attempts to perform the move retrieved from the player. If the player decides to use a tile position that has no tile to it yet, it will create a new <b>TraditionalTile</b>
    /// </summary>
    /// <returns><b>True</b> - move was successful <b>False</b> - move was illegal </returns>
    protected bool PlayPlayerMove(Player player, PlayerMove playerMove)
    {
        //Todo: optimize with dictionary marshal operations

        //Check if the move is valid according to the board
        //e.g we have a 15 wide board and the player tries to place on the tile x:16
        if (!board.IsMoveValid(playerMove.Position))
            return false;

        if (board.TileSet.TryGetValue(playerMove.Position, out var tile))
        {
            //Tile exists

            //Player tried to replace an occupied tile
            if (tile is TraditionalTile)
                return false;
        }

        //Tile doesn't yet exist
        board.TileSet.Add(playerMove.Position, new TraditionalTile(player, playerMove.Position));
        return true;
    }
}