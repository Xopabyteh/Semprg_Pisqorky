using System.Data;
using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.Services;

public class ConsoleDrawer : Drawer
{
    private readonly List<DrawData> drawRequests;
    private Int2D bodyOffset;

    public ConsoleDrawer()
    {
        drawRequests = new List<DrawData>();
        //Make room for header
        bodyOffset = new Int2D(0, 1);
    }

    public override void PushHeader()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(BatchNumber);
    }

    public override void PushDrawRequest(DrawData drawRequest)
    {
        drawRequests.Add(drawRequest);
    }

    public override void PushWinMessage(string msg)
    {
        //Make room for win message
        bodyOffset += new Int2D(0, 1);
        Console.Clear(); 
        PushHeader();
        Console.SetCursorPosition(0, 1);
        Console.WriteLine(msg);
    }

    private Int2D previousOffset;

    public override void PopAll()
    {
        if (drawRequests.Count == 0)
            return;

        //Calculate offsets based on the smallest position draw requests
        //This assures that the console never draws anything on a negative position
        var offset = new Int2D(0, 0);
        
        var smallestX = drawRequests.Min(d => d.Position.X);
        var smallestY = drawRequests.Min(d => d.Position.Y);

        if (smallestX < 0)
        {
            offset.X = -smallestX;
        }

        if (smallestY < 0 + bodyOffset.Y)
        {
            offset.Y = -smallestY;
        }

        if (!offset.Equals(previousOffset))
        {
            Console.Clear();
        }

        foreach (var drawRequest in drawRequests)
        {
            var drawPos = drawRequest.Position + offset + bodyOffset;
            Console.SetCursorPosition(drawPos.X, drawPos.Y);
            Console.Write(drawRequest.Msg);
        }

        previousOffset = offset;
        drawRequests.Clear();
    }
}