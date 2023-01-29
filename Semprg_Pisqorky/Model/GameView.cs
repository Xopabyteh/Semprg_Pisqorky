namespace Semprg_Pisqorky.Model;

public readonly struct GameView
{
    public Board Board { get; init; }
    public IReadOnlyList<Player> ActivePlayers { get; init; }
    /// <summary>
    /// If this is <b>true</b> the player should pick which swap he wants to take
    /// </summary>
    public bool SwapPrompt { get; init; }
    public GameView(Board board, IReadOnlyList<Player> activePlayers, bool swapPrompt = false)
    {
        Board = board;
        ActivePlayers = activePlayers;
        SwapPrompt = swapPrompt;
    }
}