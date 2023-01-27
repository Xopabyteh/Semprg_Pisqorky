namespace Semprg_Pisqorky.Model;

public struct PlayerMove
{
    public Int2D Position { get; init; }

    public PlayerMove(Int2D position)
    {
        Position = position;
    }
}