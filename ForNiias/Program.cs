using Niias.Application;
using Niias.Application.DomainExtentions;
using Niias.Domain;
using Niias.Infrastructure.HardcodedParkProvider;

namespace ForNiias;

internal class Program
{
    public static void Main()
    {
        RailwayScheme railwayScheme =  (new HardcodedRailwaySchemeProvider()).GetRailwayPark();

        foreach(var park in railwayScheme.RailwayParks)
        {
            WriteSeparator();
            var allParkPoints = park.GetAllPoints();
            Console.WriteLine($"Park name: \"{park.Name}\". There are {allParkPoints.Count} points in the park:");

            for(int i = 0; i < allParkPoints.Count; i++)
            {
                WritePoint(allParkPoints[i], i);
            }

            List<RailwayPoint> pointsFromFilling = ParkFillingService.FillPark(park);
            Console.WriteLine($"\nFilling for \"{park.Name}\" park (contains {pointsFromFilling.Count} points):");
            for(int i = 0; i <pointsFromFilling.Count; i++)
            {
                WritePoint(pointsFromFilling[i], i);
            }
        }

        WriteSeparator();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    private static void WritePoint(RailwayPoint point, int num)
    {
        Console.WriteLine($"Point #{num}: Name = {point.Name}, X = {point.X:f2}, Y = {point.Y:f2}");
    }

    private static void WriteSeparator()
    {
        Console.WriteLine(new string('-', 100));
    }
}
