namespace Niias.Domain;

/// <summary>
/// Точки, ограничивающие участки пути
/// </summary>
public class RailwayPoint
{
    public Guid Id { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
    public string Name { get; init; }

    public List<RailwaySection> RailwaySections { get; init; }

    public RailwayPoint(Guid id, string name, double x, double y, List<RailwaySection> railwaySections)
    {
        Id = id;
        Name = name;
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
