using System.Reflection.Emit;
using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky;

public abstract class Drawer
{
    public abstract void PushHeader(string msg);

    public abstract void PushDrawRequest(DrawData drawRequest);

    /// <summary>
    /// Draws all draw requests in a manner according to the drawer
    /// </summary>
    public abstract void PopAll();

    /// <summary>
    /// Batch = draw request. This number indicates on which draw request we are
    /// </summary>
    protected ulong BatchNumber { get; private set; }
    
    /// <summary>
    /// On what game are we now
    /// </summary>
    protected ulong GameNumber { get; private set; }

    protected void ShiftBatchNumber()
        => BatchNumber++;
    
    public void NewGame()
    {
        GameNumber++;
        IndicateNewGame();
    }

    protected abstract void IndicateNewGame();
}
