namespace Semprg_Pisqorky.Model;

public class GameView
{
    public Board Board { get; init; }

    public GameView(Board board)
    {
        Board = board;
    }
}