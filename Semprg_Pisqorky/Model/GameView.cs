namespace Semprg_Pisqorky.Model;

public readonly struct GameView
{
    public Board Board { get; init; }
    public IReadOnlyList<Player> ActivePlayers { get; init; }
    public RequiredActionType RequiredActionType { get; init; }
    public GameView(Board board, IReadOnlyList<Player> activePlayers, RequiredActionType requiredActionType)
    {
        Board = board;
        ActivePlayers = activePlayers;
        RequiredActionType = requiredActionType;
    }
}