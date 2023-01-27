using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.Tiles;

class TraditionalTile : Tile
{
    public sealed override Player? Occupant { get; init; }
    public sealed override Int2D Position { get; init; }

    public TraditionalTile(Player? occupant, Int2D position)
    {
        Occupant = occupant;
        Position = position;
    }
}