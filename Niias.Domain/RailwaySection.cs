namespace Niias.Domain;

/// <summary>
/// Участки пути
/// </summary>
public class RailwaySection
{
    public Guid Guid { get; init; }

    public RailwayPoint[] Points { get; }

    public RailwaySection(Guid guid, RailwayPoint pointA, RailwayPoint pointB)
    {
        Guid = guid;
        Points = [pointA, pointB];
        if (!pointA.RailwaySections.Any(x => x.Guid == Guid))
        {
            pointA.RailwaySections.Add(this);
        }
        if (!pointA.RailwaySections.Any(x => x.Guid == Guid))
        {
            pointB.RailwaySections.Add(this);
        }
    }

    public double GetLength()
    {
        if (Points[0] == null || Points[1] == null)
        {
            throw new NullReferenceException("At least one of the points is null");
        }

        return Math.Sqrt(
            Math.Pow(Points[0].X - Points[1].X, 2) 
            + Math.Pow(Points[0].Y - Points[1].Y, 2));
    }
}
