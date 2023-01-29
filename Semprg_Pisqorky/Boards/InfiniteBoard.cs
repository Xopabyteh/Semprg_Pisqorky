using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.Boards;

public class InfiniteBoard : Board
{
    public sealed override IDictionary<Int2D, Tile> TileSet { get; set; }
    
    public override bool IsMoveValid(Int2D pos)
    {
        return true;
    }

    public InfiniteBoard()
    {
        TileSet = new Dictionary<Int2D, Tile>();
    }
}