using System.Data;
using Semprg_Pisqorky.Model;

namespace Semprg_Pisqorky.Services;


public class ConsoleDrawer : Drawer
{
    private enum DrawDataType
    {
        Header,
        Body
    }

    private readonly IDictionary<DrawDataType, List<DrawData>> drawRequests;
    private Int2D bodyOffset;
    private Int2D previousBodyOffset;
    
    private int previousWindowWidth;
    private int previousWindowHeight;

    public ConsoleDrawer()
    {
        drawRequests = new Dictionary<DrawDataType, List<DrawData>>();

        foreach (DrawDataType type in Enum.GetValues(typeof(DrawDataType)))
        {
            drawRequests[type] = new List<DrawData>();
        }

        //Make room for header
        previousWindowWidth = Console.WindowWidth;
        previousWindowHeight = Console.WindowHeight;
        bodyOffset = Int2D.Zero;
    }

    public override void PushHeader(string msg)
    {
        var drawData = new DrawData(new Int2D(0,bodyOffset.Y),msg);

        drawRequests[DrawDataType.Header].Add(drawData);
        
        bodyOffset += new Int2D(0, 1);
    }

    public override void PushDrawRequest(DrawData drawRequest)
    {
        drawRequests[DrawDataType.Body].Add(drawRequest);
    }


    private Int2D previousOffset;

    public override void PopAll()
    {
        if (drawRequests.Values.Aggregate(0, (sum, cur) => sum + cur.Count) == 0)
            return;

        PushHeader($"Game: {GameNumber} Batch: {BatchNumber}");

        //Clear headers
        foreach (var drawRequest in drawRequests[DrawDataType.Header])
        {
            Console.SetCursorPosition(drawRequest.Position.X, drawRequest.Position.Y);
            Console.WriteLine(new string(' ', previousWindowWidth));
        }

        //Calculate offsets based on the smallest position draw requests
        //This assures that the console never draws anything on a negative position
        var offset = new Int2D(0, 0);

        if (drawRequests[DrawDataType.Body].Count > 0)
        {
            var smallestX = drawRequests[DrawDataType.Body].Min(d => d.Position.X);
            var smallestY = drawRequests[DrawDataType.Body].Min(d => d.Position.Y);

            if (smallestX < 0)
            {
                offset.X = -smallestX;
            }

            if (smallestY < 0 + bodyOffset.Y)
            {
                offset.Y = -smallestY;
            }
        }


        if ((!offset.Equals(previousOffset) || !previousBodyOffset.Equals(bodyOffset)) || (previousWindowWidth != Console.WindowWidth || previousWindowHeight != Console.WindowHeight))
        {
            //Clear body
            foreach (var drawRequest in drawRequests[DrawDataType.Body])
            {
                var drawPos = drawRequest.Position + offset + bodyOffset;
                Console.SetCursorPosition(drawPos.X, drawPos.Y);
                Console.WriteLine(new string(' ', previousWindowWidth));
            }

            previousWindowWidth = Console.WindowWidth;
            previousWindowHeight = Console.WindowHeight;
        }

        foreach (var drawRequest in drawRequests[DrawDataType.Header])
        {
            var drawPos = drawRequest.Position;
            Console.SetCursorPosition(drawPos.X, drawPos.Y);
            Console.Write(drawRequest.Msg);
        }

        foreach (var drawRequest in drawRequests[DrawDataType.Body])
        {
            var drawPos = drawRequest.Position + offset + bodyOffset;
            Console.SetCursorPosition(drawPos.X*2, drawPos.Y);
            Console.Write(drawRequest.Msg);
        }

        previousOffset = offset;
        previousBodyOffset = bodyOffset;
        bodyOffset = Int2D.Zero;

        //Clear all draw requests
        foreach (var request in drawRequests)
        {
            request.Value.Clear();
        }

        ShiftBatchNumber();

        Console.ReadLine();
    }

    protected override void IndicateNewGame()
    {
        Console.Clear();
    }
}