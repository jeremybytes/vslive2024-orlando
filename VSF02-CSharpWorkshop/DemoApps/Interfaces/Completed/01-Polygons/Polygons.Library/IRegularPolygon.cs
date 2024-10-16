namespace Polygons.Library;

public interface IRegularPolygon
{
    int NumberOfSides { get; init; }
    int SideLength { get; init; }

    double GetPerimeter();
    double GetArea();
}
