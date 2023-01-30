namespace Semprg_Pisqorky.Model;

public readonly struct GameResult
{
    public GameState FinalState { get; init; }
    public Player? Winner { get; init; }
    public IReadOnlyCollection<Player> DisqualifiedPlayers { get; init; }
}