namespace AccessModifiers.Private;

public class SquareDistanceCalculator : IDistanceCalculator
{
    public int CalculateEuclideanDistance((int, int) point1, (int, int) point2)
    {
        // Cannot access private interface member
        // from implementing class
        //int direct = this.DirectDistance(point1, point2);

        int diff1 = point1.Item1 - point1.Item1;
        int diff2 = point1.Item2 - point2.Item2;
        return (diff1 * diff1) + (diff2 * diff2);
    }

    public int CalculateManhattanDistance((int, int) point1, (int, int) point2)
    {
        int diff1 = point1.Item1 - point1.Item1;
        int diff2 = point1.Item2 - point2.Item2;
        return Math.Abs(diff1 + diff2);
    }
}