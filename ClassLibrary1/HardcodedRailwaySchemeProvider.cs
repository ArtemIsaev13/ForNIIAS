using Niias.Application.Interfaces;
using Niias.Domain;

namespace Niias.Infrastructure.HardcodedParkProvider;

public class HardcodedRailwaySchemeProvider : IRailwaySchemeProvider
{
    public RailwayScheme GetRailwayPark()
    {
        List<RailwayPoint> railwayPoints =
            [
            //Mostly first red route
            new RailwayPoint(Guid.NewGuid(), 120, 577, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 102, 370, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 410, 370, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 412, 201, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 574, 323, new List<RailwaySection>()),//red-blue intersection
            //Mostly second red route
            new RailwayPoint(Guid.NewGuid(), 720, 566, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 882, 540, new List<RailwaySection>()),
            //Mostly blue route
            new RailwayPoint(Guid.NewGuid(), 315, 475, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 490, 357, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 660, 84, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 190, 214, new List<RailwaySection>()),
            //Mostly yellow route
            new RailwayPoint(Guid.NewGuid(), 820, 384, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 901, 197,  new List<RailwaySection>()),
            ];

        List<RailwaySection> railwaySections =
            [
                //First red route
                new RailwaySection(Guid.NewGuid(), railwayPoints[0], railwayPoints[1]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[1], railwayPoints[2]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[2], railwayPoints[3]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[3], railwayPoints[4]),
                //Second red route
                new RailwaySection(Guid.NewGuid(), railwayPoints[5], railwayPoints[6]),
                //Blue route
                new RailwaySection(Guid.NewGuid(), railwayPoints[7], railwayPoints[8]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[8], railwayPoints[4]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[4], railwayPoints[9]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[9], railwayPoints[10]),
                //First yellow route
                new RailwaySection(Guid.NewGuid(), railwayPoints[11], railwayPoints[12]),
                //Second yellow route
                new RailwaySection(Guid.NewGuid(), railwayPoints[1], railwayPoints[7]),
                //Routless section
                new RailwaySection(Guid.NewGuid(), railwayPoints[4], railwayPoints[11])
            ];

        List<RailwayRoute> routes =
            [
                new RailwayRoute(Guid.NewGuid(), "First red route", [
                    railwaySections[0], railwaySections[1], railwaySections[2], railwaySections[3]]),
                    new RailwayRoute(Guid.NewGuid(), "Second red route", [railwaySections[4]]),
                    new RailwayRoute(Guid.NewGuid(), "Blue route", [railwaySections[5], railwaySections[6], railwaySections[7], railwaySections[8]]),
                    new RailwayRoute(Guid.NewGuid(), "First yellow route", [railwaySections[9]]),
                    new RailwayRoute(Guid.NewGuid(), "Second yellow route", [railwaySections[10]]),
                    new RailwayRoute(Guid.NewGuid(), "Parkless route", [railwaySections[11]]),
            ];

        List<RailwayPark> railwayParks =
            [
            new RailwayPark(Guid.NewGuid(), "Red park", [routes[0], routes[1]]),
            new RailwayPark(Guid.NewGuid(), "Blue park", [routes[2]]),
            new RailwayPark(Guid.NewGuid(), "Yellow park", [routes[3], routes[4]])
            ];

        return new RailwayScheme(Guid.NewGuid(), "Hardcoded railway scheme", railwayParks, railwayPoints, railwaySections);
    }
}
