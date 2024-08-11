using Niias.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niias.Application;

public static class ParkFillingService
{
    public static List<RailwayPoint> FillPark(RailwayPark railwayPark)
    {
        Dictionary<Guid, RailwayPoint> parkPoints = new();

        //самая северная и самая южная точки - они точно будут лежать на границе
        //выпуклого многогранника, потому что у северной точки нет никого севернее
        //её, чтобы мочь охватить её с севера. Аналогично для южной, восточной и западной
        RailwayPoint southernmost = null; //точка с наименьшим Y
        RailwayPoint northernmost = null; //точка с наибольшим Y
        RailwayPoint eastermost = null; //точка с наибольшим X
        RailwayPoint westermost = null; //точка с наименьшим X

        //Собираем все точки в парке
        foreach (var route in railwayPark.Routes)
        {
            foreach (var section in route.RailwaySections)
            {
                foreach(var point in section.Points)
                {
                    if (!parkPoints.ContainsKey(point.Id))
                    {
                        if(southernmost == null || point.Y < southernmost.Y)
                        {
                            southernmost = point;
                        }
                        if(northernmost == null || point.Y > northernmost.Y)
                        {
                            northernmost= point;
                        }                        
                        if(eastermost == null || point.X > eastermost.X)
                        {
                            eastermost = point;
                        }
                        if(westermost == null || point.X < westermost.X)
                        {
                            westermost = point;
                        }
                        parkPoints.Add(point.Id, point);
                    }
                }
            }
        }

        //Если точек 3 и меньше, то все они лежат на границе области заливки.
        //Просто возвращаем их
        if(parkPoints.Count <= 3)
        {
            return parkPoints.Values.ToList();
        }

        //Если самая южная и самая северная точка на самом деле лежат на одной широте
        //то значит вообще все точки парка лежат на одной широте, а значит мы, вероятно, могли не найти
        //крайние точки, а взяли какие-то случайные точки.
        //В этом случае в качестве крайних точек возьмём восточную и западную.
        RailwayPoint firstPoint = (southernmost!.Y == northernmost!.Y) ? southernmost! : eastermost!;
        RailwayPoint secondPoint = (southernmost!.Y == northernmost!.Y) ? northernmost! : westermost!;

        //уберём эти две точки из основного словаря и перенесём в словарь для точек, которые точно прошли 
        //в точки заливки участка
        Dictionary<Guid, RailwayPoint> borderPoints = new();
        parkPoints.Remove(firstPoint.Id);
        parkPoints.Remove(secondPoint.Id);
        borderPoints.Add(firstPoint.Id, firstPoint);
        borderPoints.Add(secondPoint.Id, secondPoint);

        //Ищем самую удалённую от отрезка "северная точка - южная точка" точку
        //Так как она наиболее удалена от отрезка никто не может её охватить - 
        //поэтому она тоже будет лежать на границе заливки

        //Сначала вычислим коэффициенты уравнения прямой на плоскости:
        double lineParamA = secondPoint.Y - firstPoint.Y;
        double lineParamB = secondPoint.X - firstPoint.X;
        double lineParamC = firstPoint.Y * lineParamB - firstPoint.X * lineParamA;

        //теперь найдём самую удалённую точку. Это будет третья точка границы
        RailwayPoint thirdPoint = null;
        double currentMaxDistance = -1;
        foreach(var point in parkPoints)
        {
            double currentDistance = GetRelativeDistance(point.Value, lineParamA, lineParamB, lineParamC);
            if (currentDistance > currentMaxDistance)
            {
                currentMaxDistance = currentDistance;
                thirdPoint = point.Value;
            }
        }

        parkPoints.Remove(thirdPoint.Id);
        borderPoints.Add(thirdPoint.Id, thirdPoint);

        return null;
    }

    private static double GetAngleToPoint(RailwayPoint currentPoint, RailwayPoint nextPoint)
    {
        var y = nextPoint.Y - currentPoint.Y;
        var x = nextPoint.X - currentPoint.X;
        double angle = Math.Atan2(y, x) * 180 / Math.PI;
        //до 360 градусов:
        if(y < 0)
        {
            angle += 360;
        }
        return angle;
    }


    /// <summary>
    /// Относительное расстояние от точки до прямой.
    /// В формуле расстояния ещё есть знаменатель, но он тут упразднён потому что 
    /// для всех точек он будет одинаковым и на сравнение не повлияет
    /// </summary>
    /// <param name="point"></param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    private static double GetRelativeDistance(RailwayPoint point, double a, double b, double c)
    {
        return Math.Abs(a * point.X + b * point.Y + c);
    }

}
