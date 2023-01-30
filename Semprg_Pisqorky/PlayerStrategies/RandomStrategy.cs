using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.PlayerStrategies;

public class RandomStrategy : IPlayerStrategy
{
    public PlayerMove GetPlayerMove(GameView gameView)
    {
        if(gameView.RequiredActionType == RequiredActionType.ChooseSwap)
            return new PlayerMove()
            {
                SwapPlayer = gameView.ActivePlayers[1]
                //SwapPlayer = gameView.ActivePlayers[^Random.Shared.Next(1,3)]
            };

        var tilePositions = gameView.Board.TileSet.Keys;

        var highestX = 14;
        var highestY = 14;
        var lowestX  = 7;
        var lowestY = 7;

        if (tilePositions.Count > 2)
        {
            highestX = tilePositions.Max(p => p.X);
            highestY = tilePositions.Max(p => p.Y);
            lowestX = tilePositions.Min(p => p.X);
            lowestY = tilePositions.Min(p => p.Y);
        }

        var move = Int2D.RandomOnRectangle(new Int2D(lowestX - 1, lowestY - 1), new Int2D(highestX + 1, highestY + 1));
        while (gameView.Board.TileSet.ContainsKey(move))
        {
            move = Int2D.RandomOnRectangle(new Int2D(lowestX - 1, lowestY - 1), new Int2D(highestX + 1, highestY + 1));
        }

        return new PlayerMove
        {
            Position = move
        };
    }
}