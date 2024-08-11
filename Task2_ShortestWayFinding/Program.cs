using Niias.Application;
using Niias.Domain;
using Niias.Infrastructure.HardcodedParkProvider;

namespace Task2_ShortestWayFinding;

internal class Program
{
    static void Main(string[] args)
    {
        RailwayScheme railwayScheme = (new HardcodedRailwaySchemeProvider()).GetRailwayPark();

        Console.WriteLine($"There are {railwayScheme.RailwaySections.Count} sections in scheme:");

        for(int i = 0; i < railwayScheme.RailwaySections.Count; i++)
        {
            Console.WriteLine($"Section #[{i + 1}] Guid = {railwayScheme.RailwaySections[i].Guid}");
        }

        Console.WriteLine();
        int from = -1; 
        int to = -1;
        Console.WriteLine("Let's find shortest way!");
        Console.WriteLine("Enter the start section number:");
        string fromStr = Console.ReadLine();
        if(!Int32.TryParse(fromStr, out from) || from < 1 || from > railwayScheme.RailwaySections.Count)
        {
            Console.WriteLine("Wrong number! Press any key to exit...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Enter the destination section number:");
        string toStr = Console.ReadLine();
        if (!Int32.TryParse(toStr, out to) || to < 1 || to > railwayScheme.RailwaySections.Count)
        {
            Console.WriteLine("Wrong number! Press any key to exit...");
            Console.ReadKey();
            return;
        }

        var shortestWay = PathFinderService.GetShortestPath(railwayScheme, railwayScheme.RailwaySections[from], railwayScheme.RailwaySections[to]);

        if (shortestWay.Count == 0)
        {
            Console.WriteLine("There is no way.");
        }
        else
        {
            Console.WriteLine($"The shortest way contains {shortestWay.Count} sections:");
            for (int i = 0; i < shortestWay.Count; i++)
            {
                Console.WriteLine($"Section #{i}: Id - {shortestWay[i].Guid}");
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
