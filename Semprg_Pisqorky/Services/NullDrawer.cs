using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.Services;

public class NullDrawer : Drawer
{
    public override void PushHeader()
    {
    }

    public override void PushDrawRequest(DrawData drawRequest)
    {
    }

    public override void PushWinMessage(string msg)
    {
    }

    public override void PopAll()
    {
    }
}