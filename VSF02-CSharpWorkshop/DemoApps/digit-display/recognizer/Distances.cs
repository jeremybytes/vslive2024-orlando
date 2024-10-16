namespace digits;

public delegate int DistanceCalculation(int difference);

public static class Distances
{
    public static DistanceCalculation ManhattanDistance =
        diff => int.Abs(diff);
        //{
        //    if (diff < 0)
        //        return diff * -1;
        //    return diff;
        //};

    public static DistanceCalculation EuclideanDistance =
        diff => diff * diff;
}