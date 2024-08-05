namespace Niias.Domain;

/// <summary>
/// Точки, ограничивающие участки пути
/// </summary>
public class RailwayPoint
{
    public Guid Id { get; init; }
    public double X { get; init; }
    public double Y { get; init; }

    public List<RailwaySection> RailwaySections { get; init; }

    public RailwayPoint(Guid id, double x, double y, List<RailwaySection> railwaySections)
    {
        Id = id;
        X = x;
        Y = y;
        RailwaySections = railwaySections;
    }
}
