﻿using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.Services;

public class NullDrawer : Drawer
{
    public override void PushHeader(string msg)
    {
    }

    public override void PushDrawRequest(DrawData drawRequest)
    {
    }

    public override void PopAll()
    {
    }

    protected override void IndicateNewGame()
    {
    }
}