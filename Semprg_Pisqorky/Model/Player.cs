namespace Semprg_Pisqorky.Model;

public class Player
{

    public string Nickname { get; init; }
    public char Shape { get; init; }
    public IPlayerStrategy PlayerStrategy { get; init; }
    
    public Player(string nickname, char shape, IPlayerStrategy playerStrategy)
    {
        Nickname = nickname;
        Shape = shape;
        PlayerStrategy = playerStrategy;
    }
}