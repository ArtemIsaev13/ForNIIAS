using Niias.Domain;
using Niias.Infrastructure.HardcodedParkProvider;

namespace Niias.Application.Tests;

public class ParkFillingServiceTest
{
    //TODO: Я, очевидно, не гуру тестов: нужно изучить как это нужно было бы правильно сделать
    /// <summary>
    /// Коллекция "номер парка в тестовой выборке - список номеров точек в тестовой выборке, входящих в границы парка"
    /// </summary>
    private static readonly List<TestUnit> FillingTestData = [
        new TestUnit(0, [17, 13, 1, 3, 6]),
        new TestUnit(1, [4, 8, 7, 18, 15, 9]),
        new TestUnit(2, [11, 7, 1, 12])
        ];

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void ParkFillingService_FillPark_ShouldFillProperly(int datasetNumber) 
    {
        RailwayScheme railwayScheme
            = (new HardcodedRailwaySchemeProvider()).GetRailwayScheme();
        var fillingPark = railwayScheme.RailwayParks[FillingTestData[datasetNumber].ParkNumber];

        var filling = ParkFillingService.FillPark(fillingPark);

        Assert.Equal(filling.Count, FillingTestData[datasetNumber].BorderPoints.Count);

        foreach(var pointNumber in FillingTestData[datasetNumber].BorderPoints) 
        {
            Assert.Contains(railwayScheme.RailwayPoints[pointNumber], filling);
        }
    }

    private class TestUnit
    {
        public int ParkNumber { get; init; }
        public List<int> BorderPoints { get; init; } = [];

        public TestUnit(int parkNumber, List<int> borderPoints) {
            ParkNumber = parkNumber;
            BorderPoints = borderPoints;
        }
    }
}