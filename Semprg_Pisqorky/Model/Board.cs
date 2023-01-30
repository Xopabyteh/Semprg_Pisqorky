namespace Semprg_Pisqorky.Model;

public abstract class Board
{
    protected const int WinningLine = 5;

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

    public abstract bool IsMoveValid(Int2D pos);

    public virtual GameState GetGameState()
    {
        if (CheckForWin())
            return GameState.Winner;
        //Check for draw?
            //return Draw

        return GameState.Ongoing;
    }

    #region WinChecks
    /// <summary>
    /// </summary>
    /// <returns><b>True</b> - someone won <b>False</b> - noone won yet</returns>
    protected virtual bool CheckForWin()
    {
        //Todo: optimize this

        foreach (var rootTilePair in TileSet)
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

    protected virtual bool CheckForRow(Tile rootTile)
    {
        var isWin = true;
        for (int i = 1; i < WinningLine; i++)
        {
            //No tile
            if (!TileSet.TryGetValue(new Int2D(rootTile.Position.X + i, rootTile.Position.Y), out var nextTile))
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

    protected virtual bool CheckForColumn(Tile rootTile)
    {
        var isWin = true;
        for (int i = 1; i < WinningLine; i++)
        {
            //No tile
            if (!TileSet.TryGetValue(new Int2D(rootTile.Position.X, rootTile.Position.Y + i), out var nextTile))
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

    protected virtual bool CheckForDiagonals(Tile rootTile)
    {
        var isWin = true;
        //Positive diagonal
        for (int i = 1; i < WinningLine; i++)
        {
            //No tile
            if (!TileSet.TryGetValue(new Int2D(rootTile.Position.X + i, rootTile.Position.Y + i), out var nextTile))
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
        for (int i = 1; i < WinningLine; i++)
        {
            //No tile
            if (!TileSet.TryGetValue(new Int2D(rootTile.Position.X - i, rootTile.Position.Y - i), out var nextTile))
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
    #endregion
}