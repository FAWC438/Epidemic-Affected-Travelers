using System;

namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="origin">始发城市</param>
        /// <param name="dest">目标城市</param>
        /// <param name="originTime">出发时间</param>
        /// <param name="originVehicle">初始交通工具</param>
        public User(int origin, int dest, DateTime originTime, int originVehicle)
        {
            Origin = origin;
            Location = GlobleVariable.cityMapping[origin];
            Dest = dest;
            OriginTime = originTime;
            Vehicle = originVehicle;
            LimitTimeStrategy = false;
            IsArrive = false;
            RecommandPath = null;
        }

        /// <summary>
        /// 判断用户是否到达
        /// </summary>
        public bool IsArrive { get; set; }

        /// <summary>
        /// 推荐用户的路径
        /// </summary>
        public City[] RecommandPath { get; set; }

        /// <summary>
        /// 起始城市
        /// </summary>
        public int Origin { get; set; }

        /// <summary>
        /// 目标城市
        /// </summary>
        public int Dest { get; set; }

        /// <summary>
        /// 用户出发时间
        /// </summary>
        public DateTime OriginTime { get; set; }

        /// <summary>
        /// 路径城市的到达时间
        /// </summary>
        public DateTime[] CityReachTime { get; set; }

        /// <summary>
        /// 下一个城市的到达时间
        /// </summary>
        public DateTime NextCityReachTime { get; set; }

        /// <summary>
        /// 停留所在城市
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 出发时乘坐的交通工具
        /// </summary>
        public int Vehicle { get; set; }

        /// <summary>
        /// 在城市逗留为状态0，在移动中为状态1
        /// </summary>
        public int State { get; set; } = 0;

        /// <summary>
        /// 是否采用限时风险最小策略
        /// </summary>
        public bool LimitTimeStrategy { get; set; }

        /// <summary>
        /// 限时风险最小策略中的时间限制
        /// </summary>
        public int LimitTime { get; set; }
    }
}
