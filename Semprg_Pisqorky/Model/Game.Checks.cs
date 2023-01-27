namespace Semprg_Pisqorky.Model;

public partial class Game
{
    /// <summary>
    /// </summary>
    /// <returns><b>True</b> - someone won <b>False</b> - noone won yet</returns>
    private bool CheckForWin()
    {
        //Todo: optimize this

        foreach (var rootTilePair in board.TileSet)
        {
            var rootTile = rootTilePair.Value;

            if (CheckForRow(rootTile))
                return true;
            if (CheckForColumn(rootTile))
                return true;
            if (CheckForDiagonals(rootTile))
                return true;
        }

        return false;
    }

    private bool CheckForRow(Tile rootTile)
    {
        var isWin = true;
        for (int i = 1; i < WINNING_LINE; i++)
        {
            //No tile
            if (!board.TileSet.TryGetValue(new Int2D(rootTile.Position.X + i, rootTile.Position.Y), out var nextTile))
            {
                isWin = false;
                break;
            }

            //Different occupants
            if (nextTile.Occupant != rootTile.Occupant)
            {
                isWin = false;
                break;
            }
        }

        return isWin;
    }

    private bool CheckForColumn(Tile rootTile)
    {
        var isWin = true;
        for (int i = 1; i < WINNING_LINE; i++)
        {
            //No tile
            if (!board.TileSet.TryGetValue(new Int2D(rootTile.Position.X, rootTile.Position.Y + i), out var nextTile))
            {
                isWin = false;
                break;
            }

            //Different occupants
            if (nextTile.Occupant != rootTile.Occupant)
            {
                isWin = false;
                break;
            }
        }

        return isWin;
    }

    private bool CheckForDiagonals(Tile rootTile)
    {
        var isWin = true;
        //Positive diagonal
        for (int i = 1; i < WINNING_LINE; i++)
        {
            //No tile
            if (!board.TileSet.TryGetValue(new Int2D(rootTile.Position.X + i, rootTile.Position.Y + i), out var nextTile))
            {
                isWin = false;
                break;
            }

            //Different occupants
            if (nextTile.Occupant != rootTile.Occupant)
            {
                isWin = false;
                break;
            }
        }

        //Negative diagonal
        for (int i = 1; i < WINNING_LINE; i++)
        {
            //No tile
            if (!board.TileSet.TryGetValue(new Int2D(rootTile.Position.X - i, rootTile.Position.Y - i), out var nextTile))
            {
                isWin = false;
                break;
            }

            //Different occupants
            if (nextTile.Occupant != rootTile.Occupant)
            {
                isWin = false;
                break;
            }
        }

        return isWin;
    }
}