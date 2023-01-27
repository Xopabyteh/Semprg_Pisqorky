namespace Semprg_Pisqorky.Model;

public interface IPlayerStrategy
{
    public PlayerMove GetPlayerMove(GameView gameView);
}