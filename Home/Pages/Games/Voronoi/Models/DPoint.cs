internal readonly struct DPoint(double x, double y)
{
    public readonly double X = x;
    public readonly double Y = y;

    public static DPoint operator +(DPoint a, DPoint b)
        => new(a.X + b.X, a.Y + b.Y);

    public static DPoint operator -(DPoint a, DPoint b)
        => new(a.X - b.X, a.Y - b.Y);

    public static DPoint operator *(DPoint a, double s)
        => new(a.X * s, a.Y * s);
}
