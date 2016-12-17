using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeliveryPizzaLib.Driver;

namespace ServerApp
{
    public class Map
    {
        public const int Size = 10;
        private Dictionary<int, DejkstraAlgorim> calculatedDejksters = new Dictionary<int,DejkstraAlgorim>();

        public Map()
        {
            //for (int id = 0; id < Size * Size; id++)
            //{
            //    Console.WriteLine(getName(id));
            //}
        }

        private void printDejkster(DejkstraAlgorim da)
        {
            foreach (string s in PrintGraph.PrintAllMinPaths(da))
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            foreach (string s in PrintGraph.PrintAllPoints(da))
            {
                Console.WriteLine(s);
            }
        }

        private DejkstraAlgorim startNewDejkster(int pointId)
        {
            Point[] points = new Point[Size * Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    points[i * Size + j] = new Point(9999, false,
                        "[" + i.ToString() + "," + j.ToString() + "]", i * Size + j);
                }
            }
            int halfEdgeSize = (Size - 1) * Size;
            Edge[] edges = new Edge[halfEdgeSize * 2];
            Random rand = new Random(0);

            for (int i = 0; i < (Size - 1); i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    edges[i * (Size) + j] = new Edge(points[i * Size + j],
                        points[(i + 1) * Size + j], rand.Next(20));
                    edges[halfEdgeSize + i * (Size) + j] = new Edge(points[j * (Size) + i],
                        points[j * (Size) + i + 1], rand.Next(20));
                }
            }

            DejkstraAlgorim da = new DejkstraAlgorim(points, edges);
            da.AlgoritmRun(points[pointId]);
            calculatedDejksters.Add(pointId, da);
            return da;
        }

        private int[] pathToArray(List<Point> list)
        {
            int[] res = new int[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                res[i] = list[i].Id;
            }
            return res;
        }

        public string getName(int pointId) {
            return "[" + (pointId / Size) + "," + (pointId % Size) + "]";
        }

        public int getDist(int p1, int p2)
        {
            if (calculatedDejksters.ContainsKey(p1))
            {
                DejkstraAlgorim alg = calculatedDejksters[p1];
                return (int) alg.points[p2].ValueMetka;
            }
            else if (calculatedDejksters.ContainsKey(p2))
            {
                DejkstraAlgorim alg = calculatedDejksters[p2];
                return (int)alg.points[p1].ValueMetka;
            }
            else
            {
                DejkstraAlgorim alg = startNewDejkster(p1);
                return (int)alg.points[p2].ValueMetka;
            }
        }

        public Route getRoute(int p1, int p2)
        {
            int[] idxs;
            if (calculatedDejksters.ContainsKey(p1))
            {
                DejkstraAlgorim alg = calculatedDejksters[p1];
                idxs = pathToArray(alg.MinPath1(alg.points[p2]));
            }
            else if (calculatedDejksters.ContainsKey(p2))
            {
                DejkstraAlgorim alg = calculatedDejksters[p2];
                idxs = pathToArray(alg.MinPath1(alg.points[p1]));
            }
            else
            {
                DejkstraAlgorim alg = startNewDejkster(p1);
                idxs = pathToArray(alg.MinPath1(alg.points[p2]));
            }

            Route r = new Route(idxs);
            return r;
        }        
    }

    class DejkstraAlgorim
    {

        public Point[] points { get; private set; }
        public Edge[] rebra { get; private set; }
        public Point BeginPoint { get; private set; }

        public DejkstraAlgorim(Point[] pointsOfgrath, Edge[] rebraOfgrath)
        {
            points = pointsOfgrath;
            rebra = rebraOfgrath;
        }
        /// <summary>
        /// Запуск алгоритма расчета
        /// </summary>
        /// <param name="beginp"></param>
        public void AlgoritmRun(Point beginp)
        {
            if (this.points.Count() == 0 || this.rebra.Count() == 0)
            {
                throw new DekstraException("Массив вершин или ребер не задан!");
            }
            else
            {
                beginp.ValueMetka = 0;
                BeginPoint = beginp;
                OneStep(beginp);
                foreach (Point point in points)
                {
                    Point anotherP = GetAnotherUncheckedPoint();
                    if (anotherP != null)
                    {
                        OneStep(anotherP);
                    }
                    else
                    {
                        break;
                    }

                }
            }

        }
        /// <summary>
        /// Метод, делающий один шаг алгоритма. Принимает на вход вершину
        /// </summary>
        /// <param name="beginpoint"></param>
        public void OneStep(Point beginpoint)
        {
            foreach (Point nextp in Pred(beginpoint))
            {
                if (nextp.IsChecked == false)//не отмечена
                {
                    float newmetka = beginpoint.ValueMetka + GetMyRebro(nextp, beginpoint).Weight;
                    if (nextp.ValueMetka > newmetka)
                    {
                        nextp.ValueMetka = newmetka;
                        nextp.predPoint = beginpoint;
                    }
                    else
                    {

                    }
                }
            }
            beginpoint.IsChecked = true;//вычеркиваем
        }
        /// <summary>
        /// Поиск соседей для вершины. Для неориентированного графа ищутся все соседи.
        /// </summary>
        /// <param name="currpoint"></param>
        /// <returns></returns>
        private IEnumerable<Point> Pred(Point currpoint)
        {
            IEnumerable<Point> firstpoints = from ff in rebra where ff.FirstPoint == currpoint select ff.SecondPoint;
            IEnumerable<Point> secondpoints = from sp in rebra where sp.SecondPoint == currpoint select sp.FirstPoint;
            IEnumerable<Point> totalpoints = firstpoints.Concat<Point>(secondpoints);
            return totalpoints;
        }
        /// <summary>
        /// Получаем ребро, соединяющее 2 входные точки
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Edge GetMyRebro(Point a, Point b)
        {//ищем ребро по 2 точкам
            IEnumerable<Edge> myr = from reb in rebra 
                                    where (reb.FirstPoint == a & reb.SecondPoint == b) 
                                    || (reb.SecondPoint == a & reb.FirstPoint == b) 
                                    select reb;
            if (myr.Count() > 1 || myr.Count() == 0)
            {
                throw new DekstraException("Не найдено ребро между соседями!");
            }
            else
            {
                return myr.First();
            }
        }
        /// <summary>
        /// Получаем очередную неотмеченную вершину, "ближайшую" к заданной.
        /// </summary>
        /// <returns></returns>
        private Point GetAnotherUncheckedPoint()
        {
            IEnumerable<Point> pointsuncheck = from p in points where p.IsChecked == false select p;
            if (pointsuncheck.Count() != 0)
            {
                float minVal = pointsuncheck.First().ValueMetka;
                Point minPoint = pointsuncheck.First();
                foreach (Point p in pointsuncheck)
                {
                    if (p.ValueMetka < minVal)
                    {
                        minVal = p.ValueMetka;
                        minPoint = p;
                    }
                }
                return minPoint;
            }
            else
            {
                return null;
            }
        }

        public List<Point> MinPath1(Point end)
        {
            List<Point> listOfpoints = new List<Point>();
            Point tempp = new Point();
            tempp = end;
            while (tempp != this.BeginPoint)
            {
                listOfpoints.Add(tempp);
                tempp = tempp.predPoint;
            }

            return listOfpoints;
        }

    }

    class Edge
    {
        public Point FirstPoint { get; private set; }
        public Point SecondPoint { get; private set; }
        public float Weight { get; private set; }

        public Edge(Point first, Point second, float valueOfWeight)
        {
            FirstPoint = first;
            SecondPoint = second;
            Weight = valueOfWeight;
        }
    }

    class Point
    {
        public float ValueMetka { get; set; }
        public string Name { get; private set; }
        public bool IsChecked { get; set; }
        public Point predPoint { get; set; }
        public int Id { get; set; }
        public Point(int value, bool ischecked, string name, int id)
        {
            ValueMetka = value;
            IsChecked = ischecked;
            Name = name;
            Id = id;
            predPoint = new Point();
        }
        public Point()
        {
        }
    }

    static class PrintGraph
    {
        public static List<string> PrintAllPoints(DejkstraAlgorim da)
        {
            List<string> retListOfPoints = new List<string>();
            foreach (Point p in da.points)
            {
                retListOfPoints.Add(string.Format("point name={0}, point value={1}, predok={2}", p.Name, p.ValueMetka, p.predPoint.Name ?? "нет предка"));
            }
            return retListOfPoints;
        }
        public static List<string> PrintAllMinPaths(DejkstraAlgorim da)
        {
            List<string> retListOfPointsAndPaths = new List<string>();
            foreach (Point p in da.points)
            {

                if (p != da.BeginPoint)
                {
                    string s = string.Empty;
                    foreach (Point p1 in da.MinPath1(p))
                    {
                        s += string.Format("{0} ", p1.Name);
                    }
                    retListOfPointsAndPaths.Add(string.Format("Point ={0},MinPath from {1} = {2}", p.Name, da.BeginPoint.Name, s));
                }

            }
            return retListOfPointsAndPaths;

        }
    }

    class DekstraException : ApplicationException
    {
        public DekstraException(string message)
            : base(message)
        {

        }

    }
}
