using System;
using System.Collections.Generic;
using System.IO;

namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    /// <summary>
    /// 用于窗体互联的委托
    /// </summary>
    /// <param name="index">删除的用户ID</param>
    public delegate void UpdateUserList(int index);

    /// <summary>
    /// 静态公共类，作为全局变量使用
    /// </summary>
    public static class GlobleVariable
    {
        /// <summary>
        /// 系统时间
        /// </summary>
        public static DateTime dt;

        /// <summary>
        /// 日志保存路径
        /// </summary>
        public static string logPath;

        /// <summary>
        /// 城市数量
        /// </summary>
        public const int cityNumber = 10;

        /// <summary>
        /// 定义无限为double/int数据类型的最大值
        /// </summary>
        public const double INF = double.MaxValue;
        public const int INF_int = int.MaxValue;

        /// <summary>
        /// 各个城市控件在主窗体的坐标
        /// </summary>
        public static int[,] cityPos = { { 951, 240 }, { 1027, 360 }, { 938, 485 }, { 816, 373 }, { 794, 447 },
            { 1005, 373 }, { 838, 389 }, { 874, 322 }, { 1027, 221 }, { 938, 373 } };

        /// <summary>
        /// 城市映射字典
        /// </summary>
        public static string[] cityMapping = new string[cityNumber];

        /// <summary>
        /// 交通工具映射字典
        /// </summary>
        public static string[] vehicleMapping = new string[4];

        /// <summary>
        /// 城市风险值表
        /// </summary>
        public static double[] risks = new double[cityNumber];

        /// <summary>
        /// 飞机的速度
        /// </summary>
        public const double planeSpeed = 800.0;

        /// <summary>
        /// 火车的速度
        /// </summary>
        public const double trainSpeed = 300.0;

        /// <summary>
        /// 汽车的速度
        /// </summary>
        public const double carSpeed = 100.0;

        /// <summary>
        /// 飞机的风险参数
        /// </summary>
        public const int planeRisk = 9;

        /// <summary>
        /// 火车的风险参数
        /// </summary>
        public const int trainRisk = 5;

        /// <summary>
        /// 汽车的风险参数
        /// </summary>
        public const int carRisk = 2;

        /// <summary>
        /// 在一天中，飞机每8小时起飞一班
        /// </summary>
        public const int planeCycleTime = 8;

        /// <summary>
        /// 在一天中，火车每4小时发车一班
        /// </summary>
        public const int trainCycleTime = 4;

        /// <summary>
        /// 在一天中，汽车每2小时发车一班
        /// </summary>
        public const int carCycleTime = 2;

        /// <summary>
        /// 利用泛型存储用户数据
        /// </summary>
        public static List<User> users = new List<User>();

        /// <summary>
        /// 通过城市代码创建城市对象实例
        /// </summary>
        /// <param name="cityCode">城市代码</param>
        /// <param name="curTime">当前时间</param>
        /// <param name="originVehicle">始发城市时所在的交通工具<para>1：飞机；2：列车；3：汽车；0：非始发站</para></param>
        /// <returns>创建的城市对象</returns>
        public static City CreatCity(int cityCode, int curTime, int originVehicle)
        {
            City target;
            switch (cityCode)
            {
                case 0:
                    target = new Beijing(curTime, originVehicle);
                    break;
                case 1:
                    target = new Shanghai(curTime, originVehicle);
                    break;
                case 2:
                    target = new Shenzhen(curTime, originVehicle);
                    break;
                case 3:
                    target = new Chengdu(curTime, originVehicle);
                    break;
                case 4:
                    target = new Kunming(curTime, originVehicle);
                    break;
                case 5:
                    target = new Hangzhou(curTime, originVehicle);
                    break;
                case 6:
                    target = new Chongqing(curTime, originVehicle);
                    break;
                case 7:
                    target = new Xian(curTime, originVehicle);
                    break;
                case 8:
                    target = new Dalian(curTime, originVehicle);
                    break;
                case 9:
                    target = new Wuhan(curTime, originVehicle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("城市代码错误，应输入0-9的值");
            }
            return target;
        }

        /// <summary>
        /// 通过城市代码创建城市对象实例，Yen算法专用重载
        /// </summary>
        /// <param name="cityCode">城市代码</param>
        /// <param name="curTime">当前时间</param>
        /// <param name="originVehicle">始发城市时所在的交通工具<para>1：飞机；2：列车；3：汽车；0：非始发站</para></param>
        /// <param name="forbidCity">一个n行2列的矩阵，表示不可达路径，第一列为不可达路径的起始城市，第二列为不可达路径的终止城市</param>
        /// <returns>创建的城市对象</returns>
        public static City CreatCity(int cityCode, int curTime, int originVehicle, int[][] forbidCity)
        {
            City target = CreatCity(cityCode, curTime, originVehicle);
            for (int i = 0; i < forbidCity.GetLength(0); i++)
            {
                if (cityCode == forbidCity[i][0])
                {
                    target.Reachable[forbidCity[i][1]] = 0;
                    target.UpdateInfo(); ;
                }
            }

            return target;
        }

    }
}
