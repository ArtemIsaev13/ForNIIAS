using Niias.Domain;

namespace Niias.Infrastructure.HardcodedRailwaySchemeProvider.Tests;

public class HardcodedRailwaySchemeProvider_Tests
{
    [Fact]
    public void HardcodedRailwaySchemeProvider_GetRailwayPark_ShouldReturnAllPointsUsedInSections() {
        RailwayScheme railwayScheme
            = (new HardcodedParkProvider.HardcodedRailwaySchemeProvider()).GetRailwayScheme();

        var sections = railwayScheme.RailwaySections;
        var points = railwayScheme.RailwayPoints;

        foreach (var section in sections) 
        {
            foreach (var pointFromSection in section.Points) 
            {
                Assert.Contains(pointFromSection, points);
            }
        }
    }
}