using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.Boards;

internal class TraditionalBoard : Board
{
    public sealed override IDictionary<Int2D, Tile> TileSet { get; set; }
    
    public TraditionalBoard()
    {
        TileSet = new Dictionary<Int2D, Tile>();
    }
}