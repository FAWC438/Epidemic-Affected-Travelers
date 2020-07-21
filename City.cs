using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    /// <summary>
    /// 城市基类(抽象类)
    /// </summary>
    public abstract class City
    {
        /// <summary>
        /// 地图上的X坐标
        /// </summary>
        public int mapPos_x;

        /// <summary>
        /// 地图上的Y坐标
        /// </summary>
        public int mapPos_y;


        /// <summary>
        /// 城市类构造函数
        /// </summary>
        /// <param name="curTime">当前时间</param>
        /// <param name="originVehicle">始发城市时所在的交通工具<para>1：飞机；2：列车；3：汽车；0：系统自动选择</para></param>
        public City(int curTime, int originVehicle)
        {
            OriginVehicle = originVehicle;
            CurTime = curTime;
            Name = GlobleVariable.cityMapping[CityCode];
            MinPlaneTime = (GlobleVariable.planeCycleTime - CurTime % GlobleVariable.planeCycleTime) % GlobleVariable.planeCycleTime;
            MinTrainTime = (GlobleVariable.trainCycleTime - CurTime % GlobleVariable.trainCycleTime) % GlobleVariable.trainCycleTime;
            MinCarTime = (GlobleVariable.carCycleTime - CurTime % GlobleVariable.carCycleTime) % GlobleVariable.carCycleTime;
            UpdateInfo();
        }

        /// <summary>
        /// 城市编号
        /// </summary>
        public abstract int CityCode { get; set; }

        /// <summary>
        /// 与其它城市的距离
        /// </summary>
        public abstract int[] Distance { get; set; }

        /// <summary>
        /// 可达性向量，表示该城市与哪些城市可达。
        /// <para>0代表不可达；</para>
        /// <para>1代表航班可达；</para>
        /// <para>2代表列车可达；</para>
        /// <para>3代表汽车可达；</para>
        /// <para>4代表航班、列车可达；</para>
        /// <para>5代表航班、汽车可达；</para>
        /// <para>6代表汽车、列车可达；</para>
        /// <para>7代表航班、汽车、列车均可达</para>
        /// </summary>
        public abstract int[] Reachable { get; set; }

        /// <summary>
        /// 风险信息
        /// </summary>
        public abstract double[] RiskInfo { get; set; }

        /// <summary>
        /// 时间信息
        /// </summary>
        public abstract int[] TimeInfo { get; set; }

        /// <summary>
        /// 交通工具信息
        /// </summary>
        public abstract int[] VehicleInfo { get; set; }

        /// <summary>
        /// 城市自身的风险值
        /// </summary>
        public abstract double Risk { get; set; }

        /// <summary>
        /// 城市的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 最快上汽车时间
        /// </summary>
        public int MinCarTime { get; set; }

        /// <summary>
        /// 最快上火车时间
        /// </summary>
        public int MinTrainTime { get; set; }

        /// <summary>
        /// 最快上飞机时间
        /// </summary>
        public int MinPlaneTime { get; set; }

        /// <summary>
        /// 始发城市所乘交通工具
        /// </summary>
        public int OriginVehicle { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        public int CurTime { get; set; }

        /// <summary>
        /// 计算在最k小风险内能到达的城市。当k = 1时，等价于求最小风险城市
        /// <para>注意：由于时间是由 路程/速度 算出的，因此时间不满整小时时，向上取整</para>
        /// </summary>
        /// <param name="k">参数值，即本函数求第k小风险城市，k的最小值为1。</param>
        /// <param name="riskInfo">所有可达城市的最小风险值</param>
        /// <param name="vehicleInfo">所有可达城市最小风险值使用的交通工具</param>
        /// /// <param name="timeInfo">所有可达城市最小风险值花费的时间</param>
        /// <returns>目标城市</returns>
        public int K_MinRisk(int k, out double[] riskInfo, out int[] vehicleInfo, out int[] timeInfo)
        {
            int destCity = 0, vehicleTemp, timeTemp;
            double minRisk = GlobleVariable.INF;

            double[] outInfo_risk = new double[GlobleVariable.cityNumber];
            int[] outInfo_vehicle = new int[GlobleVariable.cityNumber];
            int[] outInfo_time = new int[GlobleVariable.cityNumber];

            for (int i = 0; i < GlobleVariable.cityNumber; i++)
            {
                outInfo_vehicle[i] = GlobleVariable.INF_int;
                outInfo_risk[i] = GlobleVariable.INF;
                outInfo_time[i] = GlobleVariable.INF_int;
            }

            double minRiskTemp, tempVar1, tempVar2, tempVar3;

            for (int i = 0; i < GlobleVariable.cityNumber; i++)
            {
                if (Reachable[i] != 0)
                {
                    switch (Reachable[i])
                    {
                        case 1:
                            minRiskTemp = MinPlaneTime * Risk + GlobleVariable.planeRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.planeSpeed);
                            vehicleTemp = 1;
                            timeTemp = MinPlaneTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.planeSpeed);
                            break;
                        case 2:
                            minRiskTemp = MinTrainTime * Risk + GlobleVariable.trainRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.trainSpeed);
                            vehicleTemp = 2;
                            timeTemp = MinTrainTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.trainSpeed);
                            break;
                        case 3:
                            minRiskTemp = MinCarTime * Risk + GlobleVariable.carRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.carSpeed);
                            vehicleTemp = 3;
                            timeTemp = MinCarTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.carSpeed);
                            break;
                        case 4:
                            tempVar1 = MinPlaneTime * Risk + GlobleVariable.planeRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.planeSpeed);
                            tempVar2 = MinTrainTime * Risk + GlobleVariable.trainRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.trainSpeed);
                            minRiskTemp = tempVar1 > tempVar2 ? tempVar2 : tempVar1;
                            if (minRiskTemp == tempVar2)
                            {
                                vehicleTemp = 2;
                                timeTemp = MinTrainTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.trainSpeed);
                            }
                            else
                            {
                                vehicleTemp = 1;
                                timeTemp = MinPlaneTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.planeSpeed);
                            }
                            break;
                        case 5:
                            tempVar1 = MinPlaneTime * Risk + GlobleVariable.planeRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.planeSpeed);
                            tempVar2 = MinCarTime * Risk + GlobleVariable.carRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.carSpeed);
                            minRiskTemp = tempVar1 > tempVar2 ? tempVar2 : tempVar1;
                            if (minRiskTemp == tempVar2)
                            {
                                vehicleTemp = 3;
                                timeTemp = MinCarTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.carSpeed);
                            }
                            else
                            {
                                vehicleTemp = 1;
                                timeTemp = MinPlaneTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.planeSpeed);
                            }
                            break;
                        case 6:
                            tempVar1 = MinTrainTime * Risk + GlobleVariable.trainRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.trainSpeed);
                            tempVar2 = MinCarTime * Risk + GlobleVariable.carRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.carSpeed);
                            minRiskTemp = tempVar1 > tempVar2 ? tempVar2 : tempVar1;
                            if (minRiskTemp == tempVar2)
                            {
                                vehicleTemp = 3;
                                timeTemp = MinCarTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.carSpeed);
                            }
                            else
                            {
                                vehicleTemp = 2;
                                timeTemp = MinTrainTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.trainSpeed);
                            }
                            break;
                        default:
                            tempVar1 = MinPlaneTime * Risk + GlobleVariable.planeRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.planeSpeed);
                            tempVar2 = MinTrainTime * Risk + GlobleVariable.trainRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.trainSpeed);
                            tempVar3 = MinCarTime * Risk + GlobleVariable.carRisk * Risk * (int)Math.Ceiling(Distance[i] / GlobleVariable.carSpeed);
                            minRiskTemp = Math.Min(tempVar1, Math.Min(tempVar2, tempVar3));
                            if (minRiskTemp == tempVar1)
                            {
                                vehicleTemp = 1;
                                timeTemp = MinPlaneTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.planeSpeed);
                            }
                            else if (minRiskTemp == tempVar2)
                            {
                                vehicleTemp = 2;
                                timeTemp = MinTrainTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.trainSpeed);
                            }
                            else
                            {
                                vehicleTemp = 3;
                                timeTemp = MinCarTime + (int)Math.Ceiling(Distance[i] / GlobleVariable.carSpeed);
                            }
                            break;
                    }
                    if (minRiskTemp < minRisk)
                    {
                        minRisk = minRiskTemp;
                        destCity = i;
                    }
                    outInfo_risk[i] = minRiskTemp;
                    outInfo_vehicle[i] = vehicleTemp;
                    outInfo_time[i] = timeTemp;
                }
            }
            riskInfo = outInfo_risk;
            vehicleInfo = outInfo_vehicle;
            timeInfo = outInfo_time;

            double[] countDest = (double[])outInfo_risk.Clone();
            for (int i = 0; i < k; i++)
            {
                destCity = GetMin(countDest, out _);
                if (destCity < 0)
                    return -1;
                countDest[destCity] = GlobleVariable.INF;
            }

            return destCity;
        }

        /// <summary>
        /// 更新Info系列属性，由子类负责重写，本质上是再次调用K_MinRisk()方法
        /// </summary>
        public abstract void UpdateInfo();


        /// <summary>
        /// 寻找数组最小值
        /// <para>若没有最小值，返回-1</para>
        /// </summary>
        /// <param name="arr">目标数组</param>
        /// <param name="minValue">最小值</param>
        /// <returns>最小值下标</returns>
        public int GetMin(double[] arr, out double minValue)
        {
            double min = GlobleVariable.INF;
            int index = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    index = i;
                }
            }
            minValue = min;
            return index;
        }
    }



    /// <summary>
    /// 北京市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>高风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：北京-上海；北京-深圳；北京-重庆</para>
    /// <para>2. 列车：北京-深圳；北京-昆明；北京-杭州；北京-西安；北京-大连</para>
    /// <para>3. 汽车：北京-上海；北京-杭州；北京-西安；北京-大连；北京-武汉</para>
    /// </summary>
    public class Beijing : City
    {
        private int _cityCode = 0;
        private int[] _distance = { 0, 1300, 2400, 2000, 3000, 1600, 1900, 1200, 900, 1200 };
        private double _risk = GlobleVariable.risks[0];
        private int[] _reachable = { 0, 5, 4, 0, 2, 6, 1, 6, 6, 3 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Beijing(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 951;
            mapPos_y = 240;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }

        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }
    }

    /// <summary>
    /// 上海市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>高风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：上海-北京；上海-深圳；上海-昆明</para>
    /// <para>2. 列车：上海-昆明；上海-杭州；上海-武汉</para>
    /// <para>3. 汽车：上海-北京；上海-昆明；上海-杭州；上海-西安</para>
    /// </summary>
    public class Shanghai : City
    {
        private int _cityCode = 1;
        private int[] _distance = { 1300, 0, 1700, 2200, 2700, 200, 1900, 1500, 2300, 800 };
        private double _risk = GlobleVariable.risks[1];
        private int[] _reachable = { 5, 0, 1, 0, 7, 6, 0, 3, 0, 2 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Shanghai(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 1027;
            mapPos_y = 360;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }


        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }

    }

    /// <summary>
    /// 深圳市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>高风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：深圳-北京；深圳-上海；深圳-成都；深圳-西安；深圳-大连</para>
    /// <para>2. 列车：深圳-北京；深圳-武汉</para>
    /// <para>3. 汽车：深圳-昆明；深圳-杭州；深圳-武汉</para>
    /// </summary>
    public class Shenzhen : City
    {
        private int _cityCode = 2;
        private int[] _distance = { 2400, 1700, 0, 2200, 1800, 1500, 1800, 2200, 3300, 1200 };
        private double _risk = GlobleVariable.risks[2];
        private int[] _reachable = { 4, 1, 0, 1, 3, 3, 0, 1, 1, 6 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Shenzhen(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 938;
            mapPos_y = 485;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }


        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }

    }

    /// <summary>
    /// 成都市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>低风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：成都-深圳</para>
    /// <para>2. 列车：成都-昆明；成都-西安</para>
    /// <para>3. 汽车：成都-昆明；成都-西安；成都-武汉</para>
    /// </summary>
    public class Chengdu : City
    {
        private int _cityCode = 3;
        private int[] _distance = { 2000, 2200, 2200, 0, 1100, 2800, 500, 800, 3100, 1300 };
        private double _risk = GlobleVariable.risks[3];
        private int[] _reachable = { 0, 0, 1, 0, 6, 0, 0, 6, 0, 3 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Chengdu(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 816;
            mapPos_y = 373;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }


        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }

    }

    /// <summary>
    /// 昆明市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>中风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：昆明-上海；昆明-大连</para>
    /// <para>2. 列车：昆明-北京；昆明-上海；昆明-成都；昆明-重庆；昆明-武汉</para>
    /// <para>3. 汽车：昆明-上海；昆明-深圳；昆明-成都；昆明-重庆</para>
    /// </summary>
    public class Kunming : City
    {
        private int _cityCode = 4;
        private int[] _distance = { 3000, 2700, 1800, 1100, 0, 2500, 1200, 1900, 4200, 2000 };
        private double _risk = GlobleVariable.risks[4];
        private int[] _reachable = { 2, 7, 3, 6, 0, 0, 6, 0, 1, 2 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Kunming(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 794;
            mapPos_y = 447;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }


        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }

    }

    /// <summary>
    /// 杭州市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>中风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：杭州-大连</para>
    /// <para>2. 列车：杭州-北京；杭州-上海；杭州-武汉</para>
    /// <para>3. 汽车：杭州-北京；杭州-上海；杭州-深圳；杭州-重庆；杭州-武汉</para>
    /// </summary>
    public class Hangzhou : City
    {
        private int _cityCode = 5;
        private int[] _distance = { 3000, 2700, 1800, 1100, 0, 2500, 1200, 1900, 4200, 2000 };
        private double _risk = GlobleVariable.risks[5];
        private int[] _reachable = { 6, 6, 3, 0, 0, 0, 3, 0, 1, 6 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Hangzhou(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 1005;
            mapPos_y = 373;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }


        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }

    }

    /// <summary>
    /// 重庆市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>低风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：重庆-北京</para>
    /// <para>2. 列车：重庆-昆明；重庆-西安</para>
    /// <para>3. 汽车：重庆-昆明；重庆-杭州；重庆-西安；重庆-武汉</para>
    /// </summary>
    public class Chongqing : City
    {
        private int _cityCode = 6;
        private int[] _distance = { 1900, 1900, 1800, 500, 1200, 2300, 0, 800, 3000, 1200 };
        private double _risk = GlobleVariable.risks[6];
        private int[] _reachable = { 1, 0, 0, 0, 6, 3, 0, 6, 0, 3 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Chongqing(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 838;
            mapPos_y = 389;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }


        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }

    }

    /// <summary>
    /// 西安市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>中风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：西安-深圳</para>
    /// <para>2. 列车：西安-北京；西安-成都；西安-重庆</para>
    /// <para>3. 汽车：西安-北京；西安-上海；西安-成都；西安-重庆；西安-武汉</para>
    /// </summary>
    public class Xian : City
    {
        private int _cityCode = 7;
        private int[] _distance = { 1200, 1500, 2200, 800, 1900, 1600, 800, 0, 2200, 1000 };
        private double _risk = GlobleVariable.risks[7];
        private int[] _reachable = { 6, 3, 1, 6, 0, 0, 6, 0, 0, 3 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Xian(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 874;
            mapPos_y = 322;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }


        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }

    }

    /// <summary>
    /// 大连市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>低风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：大连-深圳；大连-昆明；大连-杭州</para>
    /// <para>2. 列车：大连-北京；大连-武汉</para>
    /// <para>3. 汽车：大连-北京</para>
    /// </summary>
    public class Dalian : City
    {
        private int _cityCode = 8;
        private int[] _distance = { 900, 2300, 3300, 3100, 4200, 2300, 3000, 2200, 0, 2200 };
        private double _risk = GlobleVariable.risks[8];
        private int[] _reachable = { 6, 0, 1, 0, 1, 1, 0, 0, 0, 2 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Dalian(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 1027;
            mapPos_y = 221;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }


        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }

    }

    /// <summary>
    /// 武汉市
    /// <para>----</para>
    /// <para>风险等级：</para>
    /// <para>高风险城市</para>
    /// <para>----</para>
    /// <para>交通方式有：</para>
    /// <para>1. 航班：无</para>
    /// <para>2. 列车：武汉-上海；武汉-深圳；武汉-昆明；武汉-杭州；武汉-大连</para>
    /// <para>3. 汽车：武汉-北京；武汉-深圳；武汉-成都；武汉-杭州；武汉-重庆；武汉-西安</para>
    /// </summary>
    public class Wuhan : City
    {
        private int _cityCode = 9;
        private int[] _distance = { 1200, 800, 1200, 1300, 2000, 800, 1200, 1000, 2200, 0 };
        private double _risk = GlobleVariable.risks[9];
        private int[] _reachable = { 3, 2, 6, 3, 2, 6, 3, 3, 2, 0 };
        private double[] _riskInfo = new double[GlobleVariable.cityNumber];
        private int[] _vehicleInfo = new int[GlobleVariable.cityNumber];
        private int[] _timeInfo = new int[GlobleVariable.cityNumber];

        public Wuhan(int curTime, int originVehicle) : base(curTime, originVehicle)
        {
            mapPos_x = 938;
            mapPos_y = 373;
        }

        public override int[] Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public override double Risk
        {
            get { return _risk; }
            set { _risk = value; }
        }

        public override int[] Reachable
        {
            get
            {
                switch (OriginVehicle)
                {
                    case 1:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 1 || _reachable[i] == 4 || _reachable[i] == 5 || _reachable[i] == 7)
                                _reachable[i] = 1;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 2 || _reachable[i] == 4 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 2;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < GlobleVariable.cityNumber; i++)
                        {
                            if (_reachable[i] == 3 || _reachable[i] == 5 || _reachable[i] == 6 || _reachable[i] == 7)
                                _reachable[i] = 3;
                            else
                                _reachable[i] = 0;
                        }
                        break;
                    default:
                        break;
                }
                return _reachable;
            }
            set { _reachable = value; }
        }

        public override int CityCode
        {
            get { return _cityCode; }
            set { _cityCode = value; }
        }

        public override double[] RiskInfo
        {
            get { return _riskInfo; }
            set { _riskInfo = value; }
        }

        public override int[] VehicleInfo
        {
            get { return _vehicleInfo; }
            set { _vehicleInfo = value; }
        }

        public override int[] TimeInfo
        {
            get { return _timeInfo; }
            set { _timeInfo = value; }
        }


        public override void UpdateInfo()
        {
            _ = K_MinRisk(1, out _riskInfo, out _vehicleInfo, out _timeInfo);
        }

    }
}




