using System.Data;
using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.Services;

public class ConsoleDrawer : Drawer
{
    private readonly List<DrawData> drawRequests;
    private Int2D bodyOffset;
    
    private int previousWindowWidth;
    private int previousWindowHeight;

    private string lastHeaderMsg = string.Empty;

    public ConsoleDrawer()
    {
        drawRequests = new List<DrawData>();
        //Make room for header
        bodyOffset = new Int2D(0, 1);
        previousWindowWidth = Console.WindowWidth;
        previousWindowHeight = Console.WindowHeight;
    }

    public override void PushHeader(string msg)
    {
        lastHeaderMsg = msg;
        Console.SetCursorPosition(0, 0);
        var headerText = $"G{GameNumber} B{BatchNumber} {msg}";
        Console.Write($"{headerText}{new string(' ',Console.WindowWidth - headerText.Length)}");
    }

    public override void PushDrawRequest(DrawData drawRequest)
    {
        drawRequests.Add(drawRequest);
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

        
        if (!offset.Equals(previousOffset) || (previousWindowWidth != Console.WindowWidth || previousWindowHeight != Console.WindowHeight))
        {
            Console.Clear();
            PushHeader(lastHeaderMsg);
            previousWindowWidth = Console.WindowWidth;
            previousWindowHeight = Console.WindowHeight;
        }

        foreach (var drawRequest in drawRequests)
        {
            var drawPos = drawRequest.Position + offset + bodyOffset;
            Console.SetCursorPosition(drawPos.X*2, drawPos.Y);
            Console.Write(drawRequest.Msg);
        }

        previousOffset = offset;
        drawRequests.Clear();
        ShiftBatchNumber();

        Console.ReadLine();
    }

    protected override void IndicateNewGame()
    {
        Console.Clear();
    }
}