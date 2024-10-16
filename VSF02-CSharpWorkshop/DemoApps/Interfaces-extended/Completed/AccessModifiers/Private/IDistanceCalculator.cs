namespace AccessModifiers.Private;

public interface IDistanceCalculator
{
    public int CalculateEuclideanDistance((int, int) point1, (int, int) point2);
    public int CalculateManhattanDistance((int, int) point1, (int, int) point2);

    public int CalculateMetadistance((int, int) point1, (int, int) point2)
    {
        var direct = DirectDistance(point1, point2);
        var cubed = CubeIt(direct);
        var result = FlipSign(cubed);
        return result;
    }

    // Private abstract members not allowed
    //private int ImplicitDistance((int, int) point1, (int, int) point2);

    // Private members must have default implementation
    private int DirectDistance((int, int) point1, (int, int) point2)
    {
        (int x1, int y1) = point1;
        (int x2, int y2) = point2;
        return (x1 - x2) + (y1 - y2);
    }
    private int CubeIt(int x) => x * x * x;
    private int FlipSign(int x) => x * -1;
}
