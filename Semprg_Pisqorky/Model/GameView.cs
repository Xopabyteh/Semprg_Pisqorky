namespace Semprg_Pisqorky.Model;

public readonly struct GameView
{
    public Board Board { get; init; }
    public IReadOnlyList<Player> ActivePlayers { get; init; }
    public RequiredActionType RequiredActionType { get; init; }

    /// <summary>
    /// A Player strategy can use this to determine what pieces are their and which are foreign
    /// </summary>
    public Player PlayerOnTurn { get; init; }
    public GameView(Board board, IReadOnlyList<Player> activePlayers, RequiredActionType requiredActionType, Player playerOnTurn)
    {
        Board = board;
        ActivePlayers = activePlayers;
        RequiredActionType = requiredActionType;
        PlayerOnTurn = playerOnTurn;
    }
}