using Niias.Application.DomainExtentions;
using Niias.Domain;

namespace Niias.Application;

//TODO: Сделать соответствующий интерфейс
public static class ParkFillingService
{
    /// <summary>
    /// Возвращает вершины фигуры, описывающей все точки парка
    /// </summary>
    /// <param name="railwayPark"></param>
    /// <returns></returns>
    public static List<RailwayPoint> FillPark(RailwayPark railwayPark)
    {
        //Тут используем словарь потому что одна и таже точка может быть частью
        //нескольких путей, поэтому обезопасим себя добавляя только уникальные точки
        Dictionary<Guid, RailwayPoint> parkPoints = new();

        //Сначала найдём все самые "восточные" точки.
        double eastermostPointX = double.NegativeInfinity;
        Dictionary<Guid, RailwayPoint> eastermostPoints = new();

        //Собираем все точки в парке.
        //Параллельно составляем список самых восточных из них
        foreach (var route in railwayPark.Routes)
        {
            foreach (var section in route.RailwaySections)
            {
                foreach (var point in section.Points)
                {
                    if (!parkPoints.ContainsKey(point.Id))
                    {
                        parkPoints.Add(point.Id, point);
                    }

                    //Ещё одна из наиболее восточных точек
                    if(point.X == eastermostPointX)
                    {
                        if (!eastermostPoints.ContainsKey(point.Id))
                        {
                            eastermostPoints.Add(point.Id, point);
                        }
                    }
                    //Если это новая самая восточная точка
                    else if(point.X > eastermostPointX)
                    {
                        eastermostPoints.Clear();
                        eastermostPoints.Add(point.Id, point);
                        eastermostPointX = point.X;
                    }
                }
            }
        }

        //если наивосточнейших точек несколько - берём из них самую наиюжную.
        //Так как задача приближена к реальной будем считать, что в массиве самых восточных
        //точек точек будет немного (скорее всего вообще только одна), поэтому используем
        //встроенную сортировку
        RailwayPoint startPoint = eastermostPoints.Values.OrderBy(p => p.Y).First();
        //список уникальных точек, входящих в парк:
        var uniqueParkPoints = parkPoints.Values.ToList();

        RailwayPoint currentPoint = startPoint;
        RailwayPoint? prevPoint = null;
        RailwayPoint? nextPoint = null;
        List<RailwayPoint> result = [];

        do 
        {
            //находим следующую точку
            nextPoint = GetNearestPoint(currentPoint, prevPoint, uniqueParkPoints);
            //добвляем в список результатов
            result.Add(nextPoint);
            prevPoint = currentPoint;
            currentPoint = nextPoint;

            uniqueParkPoints.Remove(nextPoint);
            //пока следующая точка не окажется стартовой точкой - т.е. мы замкнули круг
        } while (nextPoint != startPoint);

        return result;
    }

    private static RailwayPoint GetNearestPoint(RailwayPoint currentPoint, RailwayPoint? previousPoint, List<RailwayPoint> points)
    {
        //Идея такова:
        //Берём текущую точку и выпускаем из неё луч.
        //Для стартовой точки луч выпускаем параллельно оси X, для всех остальных -
        //параллельно линии "предыдущая точка - текущая точка".
        //Начинаем вращать этот луч по часовой стрелке пока он не коснётся какой-либо точки
        //из пула точек. Это и есть наша следующая точка.

        double startAngle = 0;
        if(previousPoint != null)
        {
            startAngle = GetAngleToPoint(previousPoint, currentPoint);
        }

        //Найдём точки из пула у которых угол между этим лучом и лучом "текущая точка - проверяемая точка"
        //будет минимальным.
        //Если нессколько точек лежат на одном луче - то точек c мин.углом может быть несколько
        double minAngleDiff = double.PositiveInfinity;
        List<RailwayPoint> currentNextPoint = [];

        foreach(RailwayPoint point in points)
        {
            if(currentPoint.Id == point.Id)
            {
                continue;
            }

            double currentAngle = GetAngleToPoint(currentPoint, point);
            double currentAngleDifference = GetPositiveClockwiseAngleDifference(startAngle, currentAngle);

            if(currentAngleDifference < minAngleDiff)
            {
                currentNextPoint.Clear();
                currentNextPoint.Add(point);
                minAngleDiff = currentAngleDifference;
            }
            else if(currentAngleDifference == minAngleDiff)
            {
                currentNextPoint.Add(point);
            }
        }

        //На этом этапе у нас есть ряд точек, которые лежат на искомой стороне
        //фигуры, описывающей заливку.
        //Из этих точек мы можем выбрать самую  близкую - тогда алгоритм будет 
        //работать дольше, но получит все точки. Или самую дальнюю - тогда алгоритм будет
        //работать быстрее, и набор точек будет минимальным.
        //В принципе, можно вообще любую выбрать, заданию это будет соответствовать.

        //Найдём, всё-таки, самую дальнюю:
        return currentNextPoint.OrderBy(p => p.GetDistance(currentPoint)).Last();
    }

    /// <summary>
    /// Возвращает угол между осью Х и лучом "currentPoint - nextPoint"
    /// От 0 до 360 градусов
    /// </summary>
    /// <param name="currentPoint"></param>
    /// <param name="nextPoint"></param>
    /// <returns></returns>
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
    /// Возвращает положительную разницу двух углов.
    /// При значении параметров from = 350, to = 10 вернёт 20
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private static double GetPositiveClockwiseAngleDifference(double from, double to)
    {
        //331 127
        double result = to - from;
        return (result < 0) ? (360 + result) : result;
    }
}
