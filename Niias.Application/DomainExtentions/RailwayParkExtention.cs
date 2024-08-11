using Niias.Domain;

namespace Niias.Application.DomainExtentions;

public static class RailwayParkExtention
{
    public static List<RailwayPoint> GetAllPoints(this RailwayPark park)
    {
        //Тут используем словарь потому что одна и таже точка может быть частью
        //нескольких путей, поэтому обезопасим себя добавляя только уникальные точки
        Dictionary<Guid, RailwayPoint> parkPoints = new();

        //Собираем все точки в парке.
        //Параллельно составляем список самых восточных из них
        foreach (var route in park.Routes)
        {
            foreach (var section in route.RailwaySections)
            {
                foreach (var point in section.Points)
                {
                    if (!parkPoints.ContainsKey(point.Id))
                    {
                        parkPoints.Add(point.Id, point);
                    }
                }
            }
        }

        return parkPoints.Values.ToList();
    }
}
