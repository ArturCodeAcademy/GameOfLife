using UnityEngine;

public class Point2D
{ 
    public int X;
    public int Y;

    public Point2D() : this(0, 0) { }

    public Point2D(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2Int ToVector2Int()
    {
        return new Vector2Int(X, Y);
    }
}
