using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.Boards;

/// <summary>
/// Has a size of 15 x 15, you can only place from 1 (inclusive) to 15 (inclusive)
/// </summary>
public class TraditionalBoard : Board
{
    public sealed override IDictionary<Int2D, Tile> TileSet { get; set; }

    public TraditionalBoard()
    {
        TileSet = new Dictionary<Int2D, Tile>(225);
    }
    /// <summary>
    /// The board has a size of 15 x 15, you can only place from 1 (inclusive) to 15 (inclusive)
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public override bool IsMoveValid(Int2D pos)
    {
        return
            pos.X is >= 1 and <= 15 
            && pos.Y is >= 1 and <= 15;
    }
}