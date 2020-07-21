using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    public partial class ShowPath_win : Form
    {
        private readonly int _recommandTime;    // 建议旅程花费时间
        private readonly bool _isNewUser = false;   // 判断是否是新用户
        private readonly UpdateUserList _updateUserList;    // 委托在该类中传递所搭载的中间变量

        /// <summary>
        /// 属性：用户ID
        /// </summary>
        private int UserId { get; set; }

        /// <summary>
        /// 属性：限制时间更改后的输入
        /// </summary>
        private string ChangedLimitTime { get; set; }

        /// <summary>
        /// 属性：该类所处理的目标用户
        /// </summary>
        public User TargetUser { get; set; }

        /// <summary>
        /// 构造函数，其中需要判断是添加新用户后的情况还是双击UserList的情况
        /// </summary>
        /// <param name="userId">所查询的用户ID</param>
        /// <param name="uul">从调用ShowPath_win窗体的父窗体传递来的委托</param>
        public ShowPath_win(int userId, UpdateUserList uul)
        {
            _updateUserList = uul;
            UserId = userId;
            TargetUser = GlobleVariable.users[UserId];
            InitializeComponent();
            if (TargetUser.RecommandPath == null)
            {
                _isNewUser = true;
                TargetUser.RecommandPath = FindPath(out _recommandTime);
            }
            ShowInfo();
        }

        /// <summary>
        /// 调用算法计算用户最短路径。在限时策略中，若无法找到符合条件的最短路径，将返回null
        /// </summary>
        /// <param name="recommandTime">建议用户设定的最小时间，通常用于返回null的情况</param>
        /// <returns>用户最短路径</returns>
        private City[] FindPath(out int recommandTime)
        {
            PathFinding pathfinding = new PathFinding(TargetUser.Origin, TargetUser.Dest, TargetUser.OriginTime.Hour, TargetUser.Vehicle);
            if (TargetUser.LimitTimeStrategy)
            {
                List<City[]> nextKShortPath = pathfinding.Yen();
                int time;
                recommandTime = GlobleVariable.INF_int;
                for (int i = 0; i < nextKShortPath.Count; i++)
                {
                    time = PathFinding.CountPathTime(nextKShortPath[i]);
                    if (recommandTime > time)
                        recommandTime = time;
                    if (time <= TargetUser.LimitTime)
                        return nextKShortPath[i];
                }
                return null;
            }
            else
            {
                recommandTime = 0;
                return pathfinding.Dijkstra_Def(null, out _)[TargetUser.Dest];
            }
        }

        /// <summary>
        /// 在窗体显示该旅程的详情
        /// </summary>
        private void ShowInfo()
        {
            if (TargetUser.RecommandPath != null)
            {
                TargetUser.CityReachTime = new DateTime[TargetUser.RecommandPath.Length];
                TargetUser.CityReachTime[0] = TargetUser.OriginTime;
                for (int j = 0; TargetUser.RecommandPath[j + 1] != null && j < TargetUser.RecommandPath.Length - 1; j++)
                {
                    int nextCityTime = TargetUser.RecommandPath[j].TimeInfo[TargetUser.RecommandPath[j + 1].CityCode];
                    TargetUser.CityReachTime[j + 1] = TargetUser.CityReachTime[j].AddHours(nextCityTime);
                }


                string pathStr = "";
                for (int i = 0; i < TargetUser.RecommandPath.Length; i++)
                {
                    if (TargetUser.RecommandPath[i] != null)
                        pathStr += TargetUser.RecommandPath[i].Name;
                    else
                        break;
                    pathStr += " -> ";
                }
                pathStr = pathStr.Substring(0, pathStr.Length - 4);
                Path_label.Text = pathStr;
                UserId_label.Text = UserId.ToString();
                Time_label.Text = PathFinding.CountPathTime(TargetUser.RecommandPath).ToString();
                Risk_label.Text = PathFinding.CountPathRisk(TargetUser.RecommandPath).ToString("0.00");
                TimePass_label.Text = (int)(GlobleVariable.dt - TargetUser.OriginTime).TotalHours
                    + "小时（" + TargetUser.OriginTime.ToShortDateString() + " " + TargetUser.OriginTime.Hour.ToString() + "时 出发）";
                if (TargetUser.State == 0)
                    State_label.Text = "正逗留在" + TargetUser.Location;
                else
                    State_label.Text = "正在从" + TargetUser.Location + "，使用交通工具为" + GlobleVariable.vehicleMapping[TargetUser.Vehicle] + "\n预计到达时间为" + TargetUser.NextCityReachTime.ToString("yyyy-MM-dd HH") + "时";
            }
            else
            {
                TimeError_label.Text = "您的限制时间过低，请在下方修改限制时间或删除该用户信息，最小限制时间必须不小于" + _recommandTime.ToString() + "小时";
                TimeError_label.Visible = true;
                ChangedLimitTimeInput.Visible = true;
            }
        }

        /// <summary>
        /// 判断是否输入新的限制时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangedLimitTimeInput_TextChanged(object sender, EventArgs e)
        {
            ChangedLimitTime = ChangedLimitTimeInput.Text;
        }

        /// <summary>
        /// 点击确认的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accept_Click(object sender, EventArgs e)
        {
            if (ChangedLimitTimeInput.Visible)
            {
                if (string.IsNullOrWhiteSpace(ChangedLimitTime))
                {
                    MessageBox.Show("请输入您修改后的时间限制");
                }
                else if (!Regex.IsMatch(ChangedLimitTime, @"^\d+$"))
                {
                    MessageBox.Show("请输入正确的时间！\n应输入一个正整数");
                    ChangedLimitTimeInput.Clear();
                }
                else if (Convert.ToInt32(ChangedLimitTime) < _recommandTime)
                {
                    MessageBox.Show("您输入的时间限制过小");
                    ChangedLimitTimeInput.Clear();
                }
                else
                {
                    TargetUser.LimitTime = Convert.ToInt32(ChangedLimitTime);
                    TargetUser.RecommandPath = FindPath(out _);

                    TargetUser.CityReachTime = new DateTime[TargetUser.RecommandPath.Length];
                    TargetUser.CityReachTime[0] = TargetUser.OriginTime;
                    for (int j = 0; TargetUser.RecommandPath[j + 1] != null && j < TargetUser.RecommandPath.Length - 1; j++)
                    {
                        int nextCityTime = TargetUser.RecommandPath[j].TimeInfo[TargetUser.RecommandPath[j + 1].CityCode];
                        TargetUser.CityReachTime[j + 1] = TargetUser.CityReachTime[j].AddHours(nextCityTime);
                    }

                    GlobleVariable.users[UserId] = TargetUser;

                    // 用户改变限定时间时应当记录
                    using (FileStream logWriter = new FileStream(GlobleVariable.logPath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        logWriter.Seek(0, SeekOrigin.End);

                        string content = DateTime.Now.ToString() + "(程序内时间：" + GlobleVariable.dt.ToString() + "): ";
                        content += "用户" + UserId.ToString() + "将限定时间变更为" + TargetUser.LimitTime + "小时\n\n";

                        byte[] buffer = Encoding.Default.GetBytes(content);
                        logWriter.Write(buffer, 0, buffer.Length);
                    }

                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// 点击取消的事件，同时也是删除用户的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            if (!_isNewUser)
                _updateUserList(UserId);
            GlobleVariable.users.RemoveAt(UserId);

            // 删除用户时应当在日志中提醒ID变化
            using (FileStream logWriter = new FileStream(GlobleVariable.logPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                logWriter.Seek(0, SeekOrigin.End);

                string content = DateTime.Now.ToString() + "(程序内时间：" + GlobleVariable.dt.ToString() + "): ";
                content += "\n#####################\n用户" + UserId.ToString() + "被删除！以下所有ID大于" + UserId.ToString() + "的用户，ID减1\n#####################\n\n";

                byte[] buffer = Encoding.Default.GetBytes(content);
                logWriter.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
