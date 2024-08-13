using Niias.Application.Interfaces;
using Niias.Domain;

namespace Niias.Infrastructure.HardcodedParkProvider;

public class HardcodedRailwaySchemeProvider : IRailwaySchemeProvider
{
    public RailwayScheme GetRailwayScheme()
    {
        //Карту можно найти по пути Images/RailwayScheme.png
        //Обратите внимание, что на карте ось Y направлена вниз чтобы координаты
        //станций совпадали с координатами пикселей

        List<RailwayPoint> railwayPoints =
            [
            //Mostly first red route
            new RailwayPoint(Guid.NewGuid(), "0", 120, 577, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "1", 102, 370, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "2", 410, 370, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "3" ,412, 201, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "4", 574, 323, new List<RailwaySection>()),
            //Mostly second red route
            new RailwayPoint(Guid.NewGuid(), "5", 720, 566, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "6", 882, 540, new List<RailwaySection>()),
            //Mostly blue route
            new RailwayPoint(Guid.NewGuid(), "7", 315, 475, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "8", 490, 537, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "9", 660, 84, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "10", 190, 214, new List<RailwaySection>()),
            //Mostly yellow route
            new RailwayPoint(Guid.NewGuid(), "11", 820, 384, new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "12", 901, 197,  new List<RailwaySection>()),
            //Vertical and horizontal points
            new RailwayPoint(Guid.NewGuid(), "13", 70, 623,  new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "14", 70, 674,  new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "15", 147, 35,  new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "16", 100, 35,  new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "17", 70, 712,  new List<RailwaySection>()),
            new RailwayPoint(Guid.NewGuid(), "18", 50, 35,  new List<RailwaySection>()),
            ];

        List<RailwaySection> railwaySections =
            [
                //First red route
                new RailwaySection(Guid.NewGuid(), "0", railwayPoints[0], railwayPoints[1]),
                new RailwaySection(Guid.NewGuid(), "1", railwayPoints[1], railwayPoints[2]),
                new RailwaySection(Guid.NewGuid(), "2", railwayPoints[2], railwayPoints[3]),
                new RailwaySection(Guid.NewGuid(), "3", railwayPoints[3], railwayPoints[4]),
                //Second red route
                new RailwaySection(Guid.NewGuid(), "4", railwayPoints[5], railwayPoints[6]),
                //Blue route
                new RailwaySection(Guid.NewGuid(), "5", railwayPoints[7], railwayPoints[8]),
                new RailwaySection(Guid.NewGuid(), "6", railwayPoints[8], railwayPoints[4]),
                new RailwaySection(Guid.NewGuid(), "7", railwayPoints[4], railwayPoints[9]),
                new RailwaySection(Guid.NewGuid(), "8", railwayPoints[9], railwayPoints[10]),
                //First yellow route
                new RailwaySection(Guid.NewGuid(), "9", railwayPoints[11], railwayPoints[12]),
                //Second yellow route
                new RailwaySection(Guid.NewGuid(), "10", railwayPoints[1], railwayPoints[7]),
                //Routless section
                new RailwaySection(Guid.NewGuid(), "11", railwayPoints[4], railwayPoints[11]),

                ////First red route
                new RailwaySection(Guid.NewGuid(), "12", railwayPoints[0], railwayPoints[13]),
                new RailwaySection(Guid.NewGuid(), "13", railwayPoints[13], railwayPoints[14]),
                //Blue route
                new RailwaySection(Guid.NewGuid(), "14", railwayPoints[10], railwayPoints[15]),
                new RailwaySection(Guid.NewGuid(), "15", railwayPoints[15], railwayPoints[16]),
                new RailwaySection(Guid.NewGuid(), "16", railwayPoints[16], railwayPoints[18]),
                ////First red route
                new RailwaySection(Guid.NewGuid(), "17", railwayPoints[14], railwayPoints[17]),
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
