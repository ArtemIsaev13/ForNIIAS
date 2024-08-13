using Niias.Domain;

namespace Niias.Application;

public static class PathFinderService
{
    public static List<RailwaySection> GetShortestPath(RailwayScheme railwayScheme, RailwaySection from, RailwaySection to)
    {
        List<PathUnit> pathUnits = [];

        //с каждого конца секции найдём путь до каждого конца второй секции - итого 4 пути
        pathUnits.AddRange(GetShortestPath(railwayScheme, from.PointA, to.PointA, to.PointB));
        pathUnits.AddRange(GetShortestPath(railwayScheme, from.PointB, to.PointA, to.PointB));

        var bestUnit = pathUnits.OrderBy(u => u.Length).First();

        return bestUnit.PathBySections;
    }

    /// <summary>
    /// Возвращает кратчайший путь в виде списка 
    /// </summary>
    /// <param name="railwayScheme"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private static List<PathUnit> GetShortestPath(RailwayScheme railwayScheme, RailwayPoint from, RailwayPoint to1, RailwayPoint to2)
    {
        //Дейкстра
        Dictionary<Guid, PathUnit> pathUnits = [];

        //Создаём удобные контейнеры для поиска
        foreach(var point in railwayScheme.RailwayPoints)
        {
            var newUnit = new PathUnit(point);
            if(point.Id == from.Id)
            {
                newUnit.Length = 0;
            }

            pathUnits.Add(point.Id, newUnit);
        }

        var currentUnit = pathUnits[from.Id];
        //Алгоритм поиска
        while(true)
        {
            currentUnit.PathByPoints.Add(currentUnit.Point);
            //Для всех соседних точек обновляем веса
            foreach(var section in currentUnit.Point.RailwaySections)
            {
                var nextPointId = 
                    (section.Points[0].Id == currentUnit.Point.Id) 
                    ? section.Points[1].Id 
                    : section.Points[0].Id;
                var sectionLength = section.GetLength();

                var nextPathUnit = pathUnits[nextPointId];

                if(currentUnit.Length + sectionLength < nextPathUnit.Length)
                {
                    nextPathUnit.Length = currentUnit.Length + sectionLength;
                    nextPathUnit.PathByPoints = currentUnit.PathByPoints.ToList();
                    nextPathUnit.PathBySections = currentUnit.PathBySections.ToList();
                    nextPathUnit.PathBySections.Add(section);
                }
            }
            currentUnit.IsChecked = true;

            Guid? nextUnitId = GetSmollestUnchecheckedUnitId(pathUnits.Values.ToList());

            if(nextUnitId is null)
            {
                break;
            }
            else
            {
                currentUnit = pathUnits[nextUnitId.Value];
            }
        }

        return [pathUnits[to1.Id], pathUnits[to2.Id]];
    }

    private static Guid? GetSmollestUnchecheckedUnitId(List<PathUnit> pathUnits)
    {
        var smallestLenght = double.PositiveInfinity;
        Guid? guid = null;

        foreach(var unit in pathUnits)
        {
            if(!unit.IsChecked && unit.Length < smallestLenght)
            {
                smallestLenght = unit.Length;
                guid = unit.Point.Id;
            }
        }

        return guid;
    }

    private class PathUnit
    {
        public double Length = double.PositiveInfinity;
        public List<RailwayPoint> PathByPoints = [];
        public List<RailwaySection> PathBySections = [];
        public RailwayPoint Point { get; init; }
        public bool IsChecked = false;

        public PathUnit(RailwayPoint destination)
        {
            Point = destination;
        }
    }
}
