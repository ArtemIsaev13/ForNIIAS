namespace Niias.Domain;

/// <summary>
/// Сущность "Парк"
/// </summary>
public class RailwayPark //TODO: Проверить перевод
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<RailwayRoute> Routes { get; init; }

    public RailwayPark(Guid id, string name, List<RailwayRoute> routes)
    {
        Id = id;
        Name = name;
        Routes = routes;
    }
}
