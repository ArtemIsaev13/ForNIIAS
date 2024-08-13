using Niias.Domain;
using Niias.Infrastructure.HardcodedParkProvider;

namespace Niias.Application.Tests;
public class PathFinderServiceTests
{
    private static readonly List<TestUnit> TestUnits = [
        new TestUnit(0, 4, [], false),
        new TestUnit(4, 8, [], false),
        new TestUnit(13, 9, [12, 0, 10, 5, 6, 11], true),
        new TestUnit(1, 2, [], true),
        new TestUnit(8, 0, [7, 6, 5, 10], true),
        ];


    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void PathFinderService_GetShortestPath_ShouldReturnProperPath(int testDataNumber) {
        RailwayScheme railwayScheme
            = (new HardcodedRailwaySchemeProvider()).GetRailwayScheme();
        var testUnit = TestUnits[testDataNumber];

        var shortestPath = 
            PathFinderService.GetShortestPath(railwayScheme, 
            railwayScheme.RailwaySections[testUnit.StartSection], 
            railwayScheme.RailwaySections[testUnit.EndSection]);

        Assert.Equal(testUnit.PathExists, shortestPath is not null);

        if(shortestPath is not null) {
            Assert.Equal(testUnit.Path.Count, shortestPath.Count);
            for(int i = 0; i < testUnit.Path.Count; i++) {
                Assert.Equal(railwayScheme.RailwaySections[testUnit.Path[i]].Guid, shortestPath[i].Guid);
            }
        }
    }

    private class TestUnit
    {
        public int StartSection { get; init; }
        public int EndSection { get; init; }
        public List<int> Path { get; init; } = [];
        public bool PathExists { get; init; }

        public TestUnit(int startSection, int endSection, List<int> path, bool pathExists) {
            StartSection = startSection;
            EndSection = endSection;
            Path = path;
            PathExists = pathExists;
        }
    }
}
