
namespace Niias.Domain;
/// <summary>
/// Схема станции. Содержит в себе все данные о точках, участках, путях и парках
/// </summary>
public class RailwayScheme
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public List<RailwayPark> RailwayParks { get; init; }

    public List<RailwayPoint> RailwayPoints { get; init; }

    public List<RailwaySection> RailwaySections { get; init; }

    public RailwayScheme(Guid id, string name, List<RailwayPark> railwayParks, List<RailwayPoint> railwayPoints, List<RailwaySection> railwaySections)
    {
        Id = id;
        Name = name;
        RailwayParks = railwayParks;
        RailwayPoints = railwayPoints;
        RailwaySections = railwaySections;
    }
}
