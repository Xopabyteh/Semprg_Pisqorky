namespace Semprg_Pisqorky.Model;

public abstract class Board
{
    /// <summary>
    /// Key: Position on board
    /// Value: Tile
    /// </summary>
    public abstract IDictionary<Int2D,Tile> TileSet { get; set; }

    public virtual void Draw(Drawer drawer)
    {
        foreach (var tile in TileSet)
        {
            tile.Value.Draw(drawer);
        }
    }
}