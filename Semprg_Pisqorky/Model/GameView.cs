namespace Semprg_Pisqorky.Model;

public struct GameView
{
    public Board Board { get; init; }
    public IReadOnlyList<Player> ActivePlayers { get; init; }

    public GameView(Board board, IReadOnlyList<Player> activePlayers)
    {
        Board = board;
        ActivePlayers = activePlayers; 
    }
}