using Niias.Domain;

namespace Niias.Application.DomainExtentions;

public static class RailwayPointsExtension
{
    public static double GetDistance(this RailwayPoint thisPoint, RailwayPoint point)
    {
        return
            Math.Sqrt(
                Math.Pow(thisPoint.X - point.X, 2)
                + Math.Pow(thisPoint.Y - point.Y, 2));
    }
}
