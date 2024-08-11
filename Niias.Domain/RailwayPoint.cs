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

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) 
        { 
            return false; 
        }
        if (obj is RailwayPoint) 
        { 
            return Equals(Id, ((RailwayPoint)obj).Id); 
        }
        return false;
    }
}
