using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky;

public abstract class Drawer
{
    public abstract void PushHeader();
    public abstract void PushDrawRequest(DrawData drawRequest);
    public abstract void PushWinMessage(string msg);
    public abstract void PopAll();

    public ulong BatchNumber { get; private set; }

    public void ShiftBatchNumber()
        => BatchNumber++;

}
