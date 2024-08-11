using Niias.Application.Interfaces;
using Niias.Domain;

namespace Niias.Infrastructure.HardcodedParkProvider;

public class HardcodedRailwaySchemeProvider : IRailwaySchemeProvider
{
    public RailwayScheme GetRailwayPark()
    {
        //Карту можно найти по пути Images/RailwayScheme.png
        //Обратите внимание, что на карте ось Y направлена вниз чтобы координаты
        //станций совпадали с координатами пикселей

        List<RailwayPoint> railwayPoints =
            [
            //Mostly first red route
            new RailwayPoint(Guid.NewGuid(), 120, 577, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 102, 370, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 410, 370, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 412, 201, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 574, 323, new List<RailwaySection>()),
            //Mostly second red route
            new RailwayPoint(Guid.NewGuid(), 720, 566, new List<RailwaySection>()), //5
            new RailwayPoint(Guid.NewGuid(), 882, 540, new List<RailwaySection>()),
            //Mostly blue route
            new RailwayPoint(Guid.NewGuid(), 315, 475, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 490, 537, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 660, 84, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 190, 214, new List<RailwaySection>()), //10
            //Mostly yellow route
            new RailwayPoint(Guid.NewGuid(), 820, 384, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 901, 197,  new List<RailwaySection>()),
            //Vertical and horizontal points
            new RailwayPoint(Guid.NewGuid(), 70, 623,  new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 70, 674,  new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 147, 35,  new List<RailwaySection>()), //15
            new RailwayPoint(Guid.NewGuid(), 100, 35,  new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 70, 712,  new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), 50, 35,  new List<RailwaySection>()),
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
                new RailwaySection(Guid.NewGuid(), railwayPoints[7], railwayPoints[8]), //5
                new RailwaySection(Guid.NewGuid(), railwayPoints[8], railwayPoints[4]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[4], railwayPoints[9]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[9], railwayPoints[10]),
                //First yellow route
                new RailwaySection(Guid.NewGuid(), railwayPoints[11], railwayPoints[12]),
                //Second yellow route
                new RailwaySection(Guid.NewGuid(), railwayPoints[1], railwayPoints[7]), //10
                //Routless section
                new RailwaySection(Guid.NewGuid(), railwayPoints[4], railwayPoints[11]),

                ////First red route
                new RailwaySection(Guid.NewGuid(), railwayPoints[0], railwayPoints[13]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[13], railwayPoints[14]),
                //Blue route
                new RailwaySection(Guid.NewGuid(), railwayPoints[10], railwayPoints[15]),
                new RailwaySection(Guid.NewGuid(), railwayPoints[15], railwayPoints[16]), //15
                new RailwaySection(Guid.NewGuid(), railwayPoints[16], railwayPoints[18]),
                ////First red route
                new RailwaySection(Guid.NewGuid(), railwayPoints[14], railwayPoints[17]),
            ];

        List<RailwayRoute> routes =
            [
                new RailwayRoute(Guid.NewGuid(), "First red route", [
                        railwaySections[0], railwaySections[1], railwaySections[2], 
                        railwaySections[3], railwaySections[12], railwaySections[13], 
                        railwaySections[17]]),
                    new RailwayRoute(Guid.NewGuid(), "Second red route", [railwaySections[4]]),
                    new RailwayRoute(Guid.NewGuid(), "Blue route", [
                        railwaySections[5], railwaySections[6], railwaySections[7], 
                        railwaySections[8], railwaySections[14], railwaySections[15],
                        railwaySections[16]]),
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
