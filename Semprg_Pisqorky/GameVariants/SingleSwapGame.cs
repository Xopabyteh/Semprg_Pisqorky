using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.GameVariants;

/// <summary>
/// <b>Only 2 players can play</b>.
/// This is the competetive game. The first player must commit to a swap by placing 2 of his pieces and 1 opponent piece.
/// Then the opponent gets to choose which pieces he wants. If he chooses the initiators pieces, the pieces on the board swap occupant
/// and the it's the initiators turn, otherwise, if the opponent chooses his pieces, the pieces on the board stay and its his turn
/// </summary>
public class SingleSwapGame : TraditionalGame
{
    public SingleSwapGame(Drawer drawer, Board board, IReadOnlyList<Player> participants) : base(drawer, board, participants)
    {
        if (participants.Count != 2)
            throw new ArgumentException("There must always be exactly 2 participants in this game");
    }

    public override Player? SimulateGame()
    {
        //Before starting the loop, perform a swap
        var swapper = activePlayers[0];
        var other = activePlayers[1];
        for (int i = 0; i < 3; i++)
        {
            drawer.PushHeader($"{swapper.Nickname} placed swap piece #{i}");
            var initialSwapMove = swapper.PlayerStrategy.GetPlayerMove(GameView);
            //0: swapper
            //1: other (Place the other players piece)
            //2: swapper
            var isMoveLegal = PlayPlayerMove(i == 1 ? other : swapper, initialSwapMove);
            
            board.Draw(drawer);
            drawer.PopAll();
            
            //If the swapper makes an illegal move, the other player wins
            if (!isMoveLegal)
                return other;
        }
        var swapPromptPlayerMove = other.PlayerStrategy.GetPlayerMove(new GameView(board, activePlayers, true));
        board.Draw(drawer);

        //If prompted player doesn't respond to the swap correctly, the swapper wins
        if (swapPromptPlayerMove.SwapPlayer is null || !activePlayers.Contains(swapPromptPlayerMove.SwapPlayer))
        {
            drawer.PushHeader($"{other.Nickname} failed to choose");
            drawer.PopAll();
            return swapper;
        }

        drawer.PushHeader($"{other.Nickname} chose {swapPromptPlayerMove.SwapPlayer.Shape}");
        drawer.PopAll();

        if (swapPromptPlayerMove.SwapPlayer == swapper)
        {
            //Switch shapes; keep queue
            foreach (var tile in board.TileSet.Values)
            {
                tile.Occupant = tile.Occupant == swapper ? other : swapper;
            }
        }
        else //swapMove.SwapPlayer == other
        {
            //Switch queue; keep shapes
            (activePlayers[0], activePlayers[1]) = (other, swapper);
        }
        return base.SimulateGame();
    }
}