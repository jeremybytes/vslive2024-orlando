namespace Polygons.Library;

public class ConcreteRegularPolygon
{
    //private int _numberOfSides;
    //public int NumberOfSide
    //{
    //    get { return _numberOfSides; }
    //    set { _numberOfSides = value; }
    //}

    public int NumberOfSides { get; set; }
    public int SideLength { get; set; }

    public ConcreteRegularPolygon(int sides, int length)
    {
        NumberOfSides = sides;
        SideLength = length;
    }

    public double GetPerimeter() => NumberOfSides * SideLength;

    public virtual double GetArea() => throw new NotImplementedException();
}
