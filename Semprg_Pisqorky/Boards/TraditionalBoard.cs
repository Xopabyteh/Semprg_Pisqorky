using Semprg_Pisqorky.Model;
using Semprg_Pisqorky.Tiles;

namespace Semprg_Pisqorky.Boards;

/// <summary>
/// Has a size of 15 x 15, you can only place from 1 (inclusive) to 15 (inclusive)
/// </summary>
public class TraditionalBoard : Board
{
    public sealed override IDictionary<Int2D, Tile> TileSet { get; set; }
    private const int BOARD_SIZE = 15;


    public TraditionalBoard()
    {
        TileSet = new Dictionary<Int2D, Tile>(BOARD_SIZE * BOARD_SIZE);
    }
    /// <summary>
    /// The board has a size of 15 x 15, you can only place from 1 (inclusive) to 15 (inclusive)
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public override bool IsMoveValid(Int2D pos)
    {
        return
            pos.X is >= 1 and <= BOARD_SIZE 
            && pos.Y is >= 1 and <= BOARD_SIZE;
    }

    public override GameState GetGameState()
    {
        //There is no place to go
        if (TileSet.Values.Count(t => t is TraditionalTile) == BOARD_SIZE * BOARD_SIZE)
        {
            return GameState.Draw;
        }
        return base.GetGameState();
    }
}