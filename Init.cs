using System;
using System.IO;

namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    /// <summary>
    /// 初始化类
    /// </summary>
    public static class Init
    {
        /// <summary>
        /// 初始化调用函数
        /// </summary>
        public static void Init_def()
        {
            GlobleVariable.dt = new DateTime(2020, 1, 1);
            InitCityMapping();
            InitRisks();
            InitVehicleMapping();
            GlobleVariable.logPath = System.Windows.Forms.Application.StartupPath + @"\log.txt";  // 日志默认保存在和可执行文件相同的目录下
            if (File.Exists(GlobleVariable.logPath))
                File.Delete(GlobleVariable.logPath);
        }

        /// <summary>
        /// 初始化城市映射字典
        /// </summary>
        public static void InitCityMapping()
        {
            GlobleVariable.cityMapping[0] = "Beijing";
            GlobleVariable.cityMapping[1] = "Shanghai";
            GlobleVariable.cityMapping[2] = "Shenzhen";
            GlobleVariable.cityMapping[3] = "Chengdu";
            GlobleVariable.cityMapping[4] = "Kunming";
            GlobleVariable.cityMapping[5] = "Hangzhou";
            GlobleVariable.cityMapping[6] = "Chongqing";
            GlobleVariable.cityMapping[7] = "Xian";
            GlobleVariable.cityMapping[8] = "Dalian";
            GlobleVariable.cityMapping[9] = "Wuhan";
        }

        /// <summary>
        /// 初始化交通工具映射字典
        /// </summary>
        public static void InitVehicleMapping()
        {
            GlobleVariable.vehicleMapping[0] = "该目标为始发城市自身";
            GlobleVariable.vehicleMapping[1] = "飞机";
            GlobleVariable.vehicleMapping[2] = "列车";
            GlobleVariable.vehicleMapping[3] = "汽车";
        }


        /// <summary>
        /// 初始化城市风险表，低风险0.2，中风险0.5，高风险0.9
        /// </summary>
        public static void InitRisks()
        {
            GlobleVariable.risks = new double[10] { 0.9, 0.9, 0.9, 0.2, 0.5, 0.5, 0.2, 0.5, 0.2, 0.9 };
        }

    }
}
