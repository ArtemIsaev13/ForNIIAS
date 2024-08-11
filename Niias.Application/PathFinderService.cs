using Niias.Application.DomainExtentions;
using Niias.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niias.Application;

public static class PathFinderService
{
    /// <summary>
    /// Возвращает кратчайший путь в виде списка 
    /// </summary>
    /// <param name="railwayScheme"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static List<RailwayPoint> GetShortestPath(RailwayScheme railwayScheme, RailwayPoint from, RailwayPoint to)
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
            currentUnit.Path.Add(currentUnit.Point);
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
                    nextPathUnit.Path = currentUnit.Path.ToList();
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

        return pathUnits[to.Id].Path;
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
        public List<RailwayPoint> Path = [];
        public RailwayPoint Point { get; init; }
        public bool IsChecked = false;

        public PathUnit(RailwayPoint destination)
        {
            Point = destination;
        }
    }
}
