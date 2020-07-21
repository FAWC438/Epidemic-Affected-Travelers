using System.Collections.Generic;

namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    class PathFinding
    {

        private readonly int _origin;   // 起始城市序号
        private readonly int _curTime;  // 当前时间（小时）
        private readonly int _originVehicle;    // 起始交通工具
        private readonly int _destination;  // 终点城市序号

        /// <summary>
        /// Dijkstra类构造函数
        /// <para>Dijkstra在本项目中仅计算风险最短路径</para>
        /// </summary>
        /// <param name="origin">起始站点</param>
        /// /// <param name="destination">目标站点</param>
        /// <param name="curTime">当前时间</param>
        /// <param name="originVehicle">起始站点交通工具</param>
        public PathFinding(int origin, int destination, int curTime, int originVehicle)
        {
            _origin = origin;
            _destination = destination;
            _curTime = curTime;
            _originVehicle = originVehicle;
        }

        /// <summary>
        /// 改进版Dijkstra算法求最小风险路径
        /// </summary>
        /// <param name="forbidCity">未使用Yen算法时，此参数为null即可，否则该参数表示不可到达的路径集合</param>
        /// <param name="risks">到达各个城市的最大风险</param>
        /// <returns>全路径矩阵</returns>
        public City[][] Dijkstra_Def(int[][] forbidCity, out double[] risks)
        {
            /*
             * 变量初始化
             */

            double[] risks_temp = new double[GlobleVariable.cityNumber];    // 储存始发站到对应城市的风险总值
            for (int i = 0; i < GlobleVariable.cityNumber; i++)
                risks_temp[i] = GlobleVariable.INF;

            int[] time_temp = new int[GlobleVariable.cityNumber];   // 储存始发站到对应城市的时间总值


            City[][] path = new City[GlobleVariable.cityNumber][];  // 存储路径
            for (int i = 0; i < GlobleVariable.cityNumber; i++)
                path[i] = new City[GlobleVariable.cityNumber];

            City nextCity;  // 迭代到的城市

            int originVehicle = _originVehicle;
            int curTime = _curTime;
            int target = _origin;

            int[] flag = new int[GlobleVariable.cityNumber];    // 算法处理过的城市标记

            if (forbidCity == null)
                nextCity = GlobleVariable.CreatCity(target, curTime, originVehicle);    // 始发城市设定
            else
                nextCity = GlobleVariable.CreatCity(target, curTime, originVehicle, forbidCity);

            originVehicle = 0;  // 除了始发站外，其它城市可用所有交通工具

            // 初始化始发城市初始可达的城市
            for (int i = 0; i < GlobleVariable.cityNumber; i++)
                path[i][0] = nextCity;
            for (int i = 0; i < nextCity.RiskInfo.Length; i++)
            {
                if (nextCity.RiskInfo[i] != GlobleVariable.INF)
                {
                    if (forbidCity == null)
                        path[i][1] = GlobleVariable.CreatCity(i, nextCity.CurTime + nextCity.TimeInfo[i], originVehicle);  // 始发城市直接可达的城市加入路径中
                    else
                        path[i][1] = GlobleVariable.CreatCity(i, nextCity.CurTime + nextCity.TimeInfo[i], originVehicle, forbidCity);

                    risks_temp[i] = nextCity.RiskInfo[i];

                    time_temp[i] = nextCity.TimeInfo[i];
                }
            }

            flag[target] = 1;

            for (int j = 1; j < GlobleVariable.cityNumber; j++)
            {

                double min = GlobleVariable.INF;

                for (int i = 0; i < GlobleVariable.cityNumber; i++)
                {
                    if (flag[i] == 0 && risks_temp[i] < min)
                    {
                        min = risks_temp[i];
                        target = i;
                    }
                }
                if (forbidCity == null)
                    nextCity = GlobleVariable.CreatCity(target, curTime + time_temp[target], originVehicle);
                else
                    nextCity = GlobleVariable.CreatCity(target, curTime + time_temp[target], originVehicle, forbidCity);

                flag[target] = 1;

                for (int i = 0; i < GlobleVariable.cityNumber; i++)
                {
                    if (flag[i] == 0 && nextCity.Reachable[i] != 0 && min + nextCity.RiskInfo[i] < risks_temp[i])
                    {
                        risks_temp[i] = min + nextCity.RiskInfo[i];
                        time_temp[i] = time_temp[target] + nextCity.TimeInfo[i];
                        for (int k = 0; k < GlobleVariable.cityNumber; k++)
                        {
                            path[i][k] = path[target][k];
                            if (path[i][k] == null)
                            {
                                if (forbidCity == null)
                                    path[i][k] = GlobleVariable.CreatCity(i, nextCity.CurTime + nextCity.TimeInfo[i], originVehicle);
                                else
                                    path[i][k] = GlobleVariable.CreatCity(i, nextCity.CurTime + nextCity.TimeInfo[i], originVehicle, forbidCity);
                                break;
                            }
                        }
                    }
                }
            }
            risks = risks_temp;
            return path;
        }

        /// <summary>
        /// KSP, Yen算法求次k短风险路径
        /// </summary>
        /// <param name="limitTime">限制时间</param>
        /// <returns>次k短风险路径的泛型集合</returns>
        public List<City[]> Yen()
        {
            List<int[]> forbidCity = new List<int[]>();

            City[] shortestPath = Dijkstra_Def(null, out _)[_destination];    // 目标站点的最短路径

            List<City[]> result = new List<City[]>
            {
                shortestPath
            };   // 存储次k短风险路径

            List<City[]> tempPathSet = new List<City[]>(); // 存储已经被Yen算法迭代过的路径
            List<int[]> tempForbidCity = new List<int[]>(); // 存储每次迭代中被设为无穷远的两个相邻城市路径

            for (; ; )
            {
                for (int i = 0; shortestPath[i + 1] != null; i++)
                {
                    forbidCity.Add(new int[2] { shortestPath[i].CityCode, shortestPath[i + 1].CityCode });
                    City[] tempPath = Dijkstra_Def(forbidCity.ToArray(), out _)[_destination];
                    forbidCity.RemoveAt(forbidCity.Count - 1);

                    // 判断新的到的路径在不在tempPathSet中
                    int flag = 0;
                    for (int j = 0; j < tempPathSet.Count; j++)
                    {
                        if (PathToCode(tempPath).Equals(PathToCode(tempPathSet[j])))
                        {
                            flag = 1;
                            break;
                        }
                    }

                    if (flag == 1)
                        continue;
                    else
                    {
                        tempPathSet.Add(tempPath);
                        tempForbidCity.Add(new int[2] { shortestPath[i].CityCode, shortestPath[i + 1].CityCode });
                    }
                }

                int target = -1;
                double tempRisk = GlobleVariable.INF;
                for (int i = 0; i < tempPathSet.Count; i++)
                {
                    if (tempRisk > CountPathRisk(tempPathSet[i]))
                    {
                        tempRisk = CountPathRisk(tempPathSet[i]);
                        target = i;
                    }
                }
                if (target == -1)
                    break;
                shortestPath = tempPathSet[target];

                forbidCity.Add(tempForbidCity[target]);

                tempPathSet.RemoveAt(target);
                tempForbidCity.RemoveAt(target);
                if (PathToCode(shortestPath).Substring(PathToCode(shortestPath).Length - 1, 1).Equals(_destination.ToString()))
                    result.Add(shortestPath);
            }


            return result;
        }

        /// <summary>
        /// 计算一个路径的风险值
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>路径总风险值</returns>
        public static double CountPathRisk(City[] path)
        {
            double risk = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                if (path[i + 1] == null)
                    break;
                else
                    risk += path[i].RiskInfo[path[i + 1].CityCode];
            }
            return risk;
        }

        /// <summary>
        /// 计算一个路径花费的时间
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>路径总花费时间（小时）</returns>
        public static int CountPathTime(City[] path)
        {
            int time = 0;
            for (int i = 0; i < path.Length - 1; i++)
            {
                if (path[i + 1] == null)
                    break;
                else
                    time += path[i].TimeInfo[path[i + 1].CityCode];
            }
            return time;
        }

        /// <summary>
        /// 计算路径代码，若两条路径的路径代码相同，说明是同一条路径
        /// </summary>
        /// <param name="shortestPath">目标路径</param>
        /// <returns>路径代码</returns>
        private static string PathToCode(City[] shortestPath)
        {
            string shortestPath_code = "";
            for (int i = 0; i < shortestPath.Length; i++)
            {
                if (shortestPath[i] != null)
                    shortestPath_code += shortestPath[i].CityCode.ToString();
                else
                    break;
            }
            return shortestPath_code;
        }

    }
}
