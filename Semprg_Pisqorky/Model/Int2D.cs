using System;

namespace Semprg_Pisqorky.Model;

public struct Int2D
{
    public int X { get; set; }
    public int Y { get; set; }

    public Int2D(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Int2D Zero => new(0, 0);

    public static Int2D operator +(Int2D a, Int2D b)
        => new(a.X+b.X,a.Y+b.Y);

    public override string ToString()
        => $"(X: {X}, Y: {Y})";

    public bool Equals(Int2D other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Int2D other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    /// <summary>
    /// Generates a random UInt2D between min (inclusive) and max (inclusive).
    /// Note: Might not be precise
    /// </summary>
    /// <param name="min">Inclusive</param>
    /// <param name="max">Inclusive</param>
    /// <returns></returns>
    public static Int2D RandomOnRectangle(Int2D min, Int2D max)
    {
        Random random = new Random();
        int x = random.Next(min.X, max.X + 1);
        int y = random.Next(min.Y, max.Y + 1);
        return new Int2D(x, y);
    }
}