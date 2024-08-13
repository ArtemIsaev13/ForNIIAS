using Niias.Application;
using Niias.Domain;
using Niias.Infrastructure.HardcodedParkProvider;

namespace Task2_ShortestWayFinding;

internal class Program
{
    public static void Main()
    {
        RailwayScheme railwayScheme = (new HardcodedRailwaySchemeProvider()).GetRailwayPark();

        Console.WriteLine($"There are {railwayScheme.RailwaySections.Count} sections in scheme:");

        for(int i = 0; i < railwayScheme.RailwaySections.Count; i++)
        {
            WriteSection(i, railwayScheme.RailwaySections[i]);
        }

        Console.WriteLine();
        int from = -1; 
        int to = -1;
        Console.WriteLine("Let's find shortest way!");
        Console.WriteLine("Enter the start section number:");
        string? fromStr = Console.ReadLine();
        if(fromStr is null || !int.TryParse(fromStr, out from) || from < 1 || from > railwayScheme.RailwaySections.Count)
        {
            Console.WriteLine("Wrong number! Press any key to exit...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Enter the destination section number:");
        string? toStr = Console.ReadLine();
        if (toStr is null || !int.TryParse(toStr, out to) || to < 1 || to > railwayScheme.RailwaySections.Count)
        {
            Console.WriteLine("Wrong number! Press any key to exit...");
            Console.ReadKey();
            return;
        }

        var shortestWay = 
            PathFinderService
            .GetShortestPath(railwayScheme, railwayScheme.RailwaySections[from], railwayScheme.RailwaySections[to]);

        if (shortestWay.Count == 0)
        {
            Console.WriteLine("There is no way.");
        }
        else
        {
            Console.WriteLine($"The shortest way contains {shortestWay.Count} sections:");
            for (int i = 0; i < shortestWay.Count; i++)
            {
                WriteSection(i, shortestWay[i]);
            }
        }

        WriteSeparator();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }

    private static void WriteSeparator()
    {
        Console.WriteLine(new string('-', 100));
    }

    private static void WriteSection(int num, RailwaySection railwaySection)
    {
        Console
            .WriteLine($"Section #[{num}]: " +
            $"Name = {railwaySection.Name}, " +
            $"Id = {railwaySection.Guid}");
    }
}
