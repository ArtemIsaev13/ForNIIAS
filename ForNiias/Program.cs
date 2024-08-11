using Niias.Application;
using Niias.Domain;
using Niias.Infrastructure.HardcodedParkProvider;

namespace ForNiias;

internal class Program
{
    static void Main(string[] args)
    {
        RailwayScheme railwayScheme =  (new HardcodedRailwaySchemeProvider()).GetRailwayPark();

        foreach(var park in railwayScheme.RailwayParks)
        {
            WriteSeparator();
            Console.WriteLine($"Filling for \"{park.Name}\" park");

            List<RailwayPoint> points = ParkFillingService.FillPark(park);

            foreach(var point in points)
            {
                Console.WriteLine($"Point {point.Id}, X = {point.X:f2}, Y = {point.Y:f2}");
            }
        }

        WriteSeparator();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    private static void WriteSeparator()
    {
        Console.WriteLine(new String('-', 100));
    }
}
