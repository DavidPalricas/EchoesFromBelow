using System.Numerics;

public class GameGridCell
{
    private readonly int x, y;

    private readonly bool isWalkable;

    public GameGridCell(int x, int y, bool isWalkable)
    {
        this.x = x;
        this.y = y;
        this.isWalkable = isWalkable;

    }


    public bool GetIsWalkable()
    {
        return isWalkable;
    }


    public Vector2 GetPosition()
    {
        return new Vector2(x, y);
    }
}

