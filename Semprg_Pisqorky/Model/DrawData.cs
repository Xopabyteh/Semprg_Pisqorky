namespace Semprg_Pisqorky.Model;

public struct DrawData
{
    public Int2D Position { get; init; }
    public string Msg { get; init; }

    public DrawData(Int2D position, string msg)
    {
        Position = position;
        Msg = msg;
    }
}