namespace Niias.Domain;

/// <summary>
/// Путь - сущность, состоящая из некоторого количества участков пути
/// </summary>
public class RailwayRoute
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<RailwaySection> RailwaySections { get; init; }

    public RailwayRoute(Guid id, string name, List<RailwaySection> railwaySections)
    {
        Id = id;
        Name = name;
        RailwaySections = railwaySections;
    }
}
