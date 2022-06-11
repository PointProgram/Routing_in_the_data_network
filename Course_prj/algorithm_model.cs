using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_prj
{
    public class algorithm_model
    {
        private List<int> nodes;
        private List<Node> ls;
        private List<Connection> relations;
        List<Path> res = new List<Path>();
        private List<int> visited;

        public algorithm_model(List<Connection> relations, List<Node> nodes)
        {
            this.relations = relations;
            this.nodes = new List<int>();
            this.ls = nodes;
            for (int i = 0; i < nodes.Count; i++)
                this.nodes.Add(i);
        }

        //find weight value between nodes
        public float Weight(int from, int to)
        {
            for (int i = 0; i < relations.Count; i++)
                if (((relations[i].from == from) && (relations[i].to == to)) || ((relations[i].from == to) && (relations[i].to == from)))
                {
                    float weight = 0;
                    if (relations[i].type == ConnectType.duplex)
                    {
                        weight = (float)relations[i].weight;
                    }
                    else
                    {
                        weight = (float)relations[i].weight;
                    }
                    return weight;
                }
            return -1;
        }

        
        public int get_num(int num, List<int> ds)
        {
            for(int i = 0; i < ds.Count; i++) 
            {
                if(ds[i] == ds[num]) 
                {
                    return i;
                }
            }
            return -1;
        }

        public List<Path> LeastWeight(int point, List<int> dis)
        {
            visited = new List<int>();
            res = new List<Path>();
            for (int i = 0; i < nodes.Count; i++)
            {
                Path tmp = new Path();
                tmp.weight = -1;
                tmp.path = new List<int>();
                res.Add(tmp);
            }
            Path t = new Path();
            t.weight = 0;
            t.path = new List<int>();
            t.path.Add(point);
            res[point] = t;
            while ((visited.Count + dis.Count) != nodes.Count)
            {
                //Получаем список все непосещённых вершин
                List<int> unvisited = nonVisited(visited, dis);
                //Находим вершину среди непосещённых, путь из которой к искомой минимальный
                float min = res[unvisited[0]].weight;
                int v = 0;
                for (int i = 1; i < unvisited.Count; i++)
                    if (((res[unvisited[i]].weight < min) || (min == -1)) && (res[unvisited[i]].weight != -1))
                    {
                        v = i;
                        min = res[unvisited[i]].weight;
                    }
                if (min != -1)
                    for (int i = 0; i < unvisited.Count; i++)
                    {
                        if (Weight(unvisited[i], unvisited[v]) != -1)
                        {
                            if ((res[unvisited[i]].weight > (res[unvisited[v]].weight + Weight(unvisited[i], unvisited[v]))) || (res[unvisited[i]].weight == -1))
                            {
                                Path tmp = res[unvisited[i]];
                                tmp.weight = (res[unvisited[v]].weight + Weight(unvisited[i], unvisited[v]));
                                tmp.path = new List<int>();
                                tmp.path.AddRange(res[unvisited[v]].path);
                                tmp.path.Add(unvisited[i]);
                                res[unvisited[i]] = tmp;

                            }
                        }
                    }
                visited.Add(unvisited[v]);
            }
            return res;
        }

        private List<int> nonVisited(List<int> visited, List<int> disabled)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < nodes.Count; i++)
            {
                if (!disabled.Contains(nodes[i]))
                    res.Add(i);
            }
            for (int i = 0; i < visited.Count; i++)
                res.Remove(visited[i]);
            return res;
        }
        


        public List<Path> LeastCount(int point)
        {
            visited = new List<int>();
            List<int> stations = new List<int>();
            res = new List<Path>();
            for (int i = 0; i < nodes.Count; i++)
            {
                Path tmp = new Path();
                tmp.weight = -1;
                tmp.path = new List<int>();
                res.Add(tmp);
            }
            Path t = new Path();
            t.weight = 0;
            t.path = new List<int>();
            t.path.Add(point);
            res[point] = t;

            while (visited.Count != nodes.Count)
            {

                List<int> unvisited = nonVisited(visited, new List<int>());

                float min = res[unvisited[0]].weight;
                int v = 0;
                for (int i = 1; i < unvisited.Count; i++)
                    if (((res[unvisited[i]].weight < min) || (min == -1)) && (res[unvisited[i]].weight != -1))
                    {
                        v = i;
                        min = res[unvisited[i]].weight;
                    }
                if (min != -1)
                    for (int i = 0; i < unvisited.Count; i++)
                    {
                        if (Weight(unvisited[i], unvisited[v]) != -1)
                        {
                            if ((res[unvisited[i]].path.Count > (res[unvisited[v]].path.Count + 1)) || (res[unvisited[i]].weight == -1))
                            {
                                Path tmp = res[unvisited[i]];
                                tmp.weight = (res[unvisited[v]].weight + Weight(unvisited[i], unvisited[v]));
                                tmp.path = new List<int>();
                                tmp.path.AddRange(res[unvisited[v]].path);
                                tmp.path.Add(unvisited[i]);
                                res[unvisited[i]] = tmp;

                            }
                        }
                    }
                visited.Add(unvisited[v]);
            }
            return res;
        }

        public RouteRes Route(int from, int to, int count)
        {
            int querys = 0;
            int info = 0;
            int time = 0;
            List<int> visited = new List<int>();
            List<int> query = new List<int>();
            query.Add(from);

            while (query.Count > 0)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if ((Weight(query[0], i) != -1) && (!visited.Contains(i)))
                    {
                        querys += 2;
                        time += 2 * (int)Weight(query[0], i);
                        visited.Add(i);
                        query.Add(i);
                    }
                }
                query.RemoveAt(0);
            }
            List<Path> res = LeastWeight(from, new List<int>());
            querys += (res[to].path.Count - 1) * 4;
            time += (res[to].path.Count - 1) * 4 * (int)res[to].weight;
            time += (res[to].path.Count - 1) * count * (int)res[to].weight;
            info = (res[to].path.Count - 1) * count;
            RouteRes ret = new RouteRes();
            ret.info = info;
            ret.querys = querys;
            ret.time = time;
            return ret;
        }

        public RouteRes RouteDataGram(int from, int to, int count)
        {
            int querys = 0;
            int info = 0;
            int time = 0;
            List<int> visited = new List<int>();
            List<int> query = new List<int>();
            query.Add(from);

            while (query.Count > 0)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if ((Weight(query[0], i) != -1) && (!visited.Contains(i)))
                    {
                        querys += 2;
                        time += 2 * (int)Weight(query[0], i);
                        visited.Add(i);
                        query.Add(i);
                    }
                }
                query.RemoveAt(0);
            }
            List<Path> res = LeastWeight(from, new List<int>());
            //querys += (res[to].path.Count - 1) * 4;
            //time += (res[to].path.Count - 1) * 4 * (int)res[to].weight;

            int min = 10000;
            int j = -1;
            for (int i = 0; i < (int)Math.Round((double)count / 5); i++)
            {
                if ((i != from) && (Weight(i, from) != -1) && (LeastWeight(i, new List<int>())[to].weight <= min))
                {
                    //if ((!LeastWeight(i)[to].path.Contains(from)) &&  (!LeastWeight(from)[to].path.Contains(i)) && (LeastWeight(i)[to].weight > 0))
                    //{
                    //    min = (int)Weight(i, from);
                    //    j = i;
                    //}
                    if (LeastWeight(i, new List<int>())[to].weight <= LeastWeight(from, new List<int>())[to].weight)
                    {
                        min = (int)LeastWeight(i, new List<int>())[to].weight;
                        j = i;
                    }
                }
            }
            if (j < 0)
                return Route(from, to, count);
            List<Path> res1 = LeastWeight(from, new List<int>());
            List<Path> res2 = LeastWeight(j, new List<int>());
            int time1;
            int time2;
            time1 = time = 0;
            time1 = (res1[to].path.Count - 1) * 4 * (int)res1[to].weight + (res1[to].path.Count - 1) * (int)(Math.Round((double)count / 2)) * (int)res1[to].weight;
            time2 = (res2[to].path.Count - 1) * 4 * (int)res2[to].weight + (res2[to].path.Count - 1) * (int)(Math.Round((double)count / 2)) * (int)res2[to].weight;
            querys += (res1[to].path.Count - 1) * 4 + (res2[to].path.Count - 1) * 4;
            time += Math.Max(time1, time2);
            info = (res1[to].path.Count - 1) * (int)(Math.Round((double)count / 2)) + (res2[to].path.Count - 1) * (int)(Math.Round((double)count / 2));


            RouteRes ret = new RouteRes();
            ret.info = info;
            ret.querys = querys;
            ret.time = time;
            return ret;
        }

        public Info logical(Path path, int packets, ulong packetSize, int serviceSize, bool noService, bool scan)
        {
            int querys = 0;
            double t = 0;
            List<int> visited = new List<int>();
            List<int> query = new List<int>();
            Info inf = new Info();

            double speed;
            inf.connect = (ulong)(path.path.Count - 1) * 2;
            inf.disconnect = (ulong)(path.path.Count - 1) * 2;
            inf.information = (ulong)(path.path.Count - 1) * (ulong)packets;
            inf.ack = inf.information;
            inf.path = new Path();
            inf.path.path = new List<int>();
            if (path.path.Count > 0)
            {
                if (scan)
                {
                    query.Add(path.path[0]);

                    while (query.Count > 0)
                    {
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            if ((Weight(query[0], i) != -1) && (!visited.Contains(i)))
                            {
                                querys += 2;
                                t += 2 * (double)Weight(query[0], i) / 1024.0 * (double)serviceSize;
                                visited.Add(i);
                                query.Add(i);
                            }
                        }
                        query.RemoveAt(0);
                    }
                    inf.discover = (ulong)querys;
                }
                speed = path.weight / 1024.0;
                ulong time = 0;
                time = (ulong)Math.Round((double)(speed * inf.ack * serviceSize));
                if (!noService)
                    for (int i = 1; i < path.path.Count; i++)
                        time += (ulong)Math.Round((double)(Weight(path.path[i - 1], path.path[i]) / 1024.0 * serviceSize * 4));
                inf.servTime = time;
                if (path.path.Count > 1)
                {
                    float max = Weight(path.path[0], path.path[1]);
                    for (int j = 2; j < path.path.Count; j++)
                        if (Weight(path.path[j - 1], path.path[j]) > max)
                            max = Weight(path.path[j - 1], path.path[j]);
                    time += (ulong)Math.Round((double)(max / 1024.0 * (packets - 1) * (double)packetSize));
                    time += (ulong)Math.Round((double)(speed * (double)packetSize));
                    inf.time = time;
                    inf.time += (ulong)Math.Round(t);
                    inf.servTime += (ulong)Math.Round(t);
                }
                else
                    inf.time = 0;
            }
            else
                inf.time = 0;
            return inf;
        }

        public Info DataGramm(Path path, int packets, ulong packetSize, int serviceSize, bool scan)
        {
            Info res, r1, r2;
            int querys = 0;
            double t = 0;
            List<int> visited = new List<int>();
            List<int> query = new List<int>();
            res = new Info();
            if (path.path.Count == 0)
                return res;
            if (scan)
            {
                query.Add(path.path[0]);

                while (query.Count > 0)
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        if ((Weight(query[0], i) != -1) && (!visited.Contains(i)))
                        {
                            querys += 2;
                            t += 2 * (double)Weight(query[0], i) / 1024.0 * (double)serviceSize;
                            visited.Add(i);
                            query.Add(i);
                        }
                    }
                    query.RemoveAt(0);
                }
            }
            List<int> disabled = new List<int>();
            for (int i = 1; i < path.path.Count - 1; i++)
                disabled.Add(path.path[i]);
            Path route1, route2;
            route1 = LeastWeight(path.path[0], new List<int>())[path.path[path.path.Count - 1]];
            route2 = LeastWeight(path.path[0], disabled)[path.path[path.path.Count - 1]];
            int packets1 = (int)Math.Round(packets / 2.0);
            int packets2 = packets - packets1;
            r1 = logical(route1, packets1, packetSize, serviceSize, true, scan);
            r2 = logical(route2, packets2, packetSize, serviceSize, true, scan);
            res.time = Math.Max(r1.time, r2.time) + r1.servTime + r2.servTime;
            res.information = r1.information + r2.information;
            res.ack = r1.ack + r2.ack;
            res.servTime = r1.servTime + r2.servTime;
            res.path = route2;
            res.time += (ulong)t;
            res.servTime += (ulong)t;
            res.discover = (ulong)querys;
            if ((res.time > logical(path, packets, packetSize, serviceSize, true, scan).time) || (path.path.Count == 1) || (path.path.Count == 2))
                return logical(path, packets, packetSize, serviceSize, true, scan);
            return res;
        }
    }

}
