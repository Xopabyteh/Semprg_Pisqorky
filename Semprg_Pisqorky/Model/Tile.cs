namespace Semprg_Pisqorky.Model;

public abstract class Tile
{
    public abstract Player? Occupant { get; set; }
    public abstract Int2D Position { get; init; }

    public virtual void Draw(Drawer drawer)
    {
        var drawData = new DrawData(
            Position,
            msg: Occupant?.Shape.ToString() ?? "?"
        );
        drawer.PushDrawRequest(drawData);
    }
}