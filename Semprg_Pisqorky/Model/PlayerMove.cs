namespace Semprg_Pisqorky.Model;

public readonly struct PlayerMove
{
    public Int2D Position { get; init; }

    /// <summary>
    /// If prompted to take a swap, you can choose which players swap you want to take
    /// </summary>
    public Player? SwapPlayer { get; init; }
}