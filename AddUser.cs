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
    public partial class AddUser : Form
    {

        User newOne;    // 新添加的用户对象实例
        private readonly UpdateUserList _updateUserList;    // 委托在该类中传递所搭载的中间变量

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uul">从调用AddUser窗体的父窗体传递来的委托</param>
        public AddUser(UpdateUserList uul)
        {
            _updateUserList = uul;
            InitializeComponent();
        }

        /// <summary>
        /// 按下确认键后的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accept_Click(object sender, EventArgs e)
        {
            #region 判断始发城市通过所选定交通工具是否可出发
            bool canTravelFromOrigin = false;
            City tempCity = GlobleVariable.CreatCity(newOne.Origin, GlobleVariable.dt.Hour, newOne.Vehicle);
            for (int i = 0; i < tempCity.Reachable.Length; i++)
            {
                if (tempCity.Reachable[i] != 0)
                {
                    canTravelFromOrigin = true;
                    break;
                }
            }
            #endregion

            if (OriginCities_cob.SelectedItem == null || DestCities_cob.SelectedItem == null || Vehicle_cob.SelectedItem == null)
            {
                MessageBox.Show("请将您的出行信息填写完整");
            }
            else if (OriginCities_cob.SelectedIndex == DestCities_cob.SelectedIndex)
            {
                MessageBox.Show("请填写不同的始发城市和目的城市");
            }
            else if (!canTravelFromOrigin)
            {
                MessageBox.Show("无法使用该交通工具从" + GlobleVariable.cityMapping[OriginCities_cob.SelectedIndex] + "出发\n请更换您所在的交通枢纽");
            }
            else if (Limit_rad.Checked && string.IsNullOrWhiteSpace(Time_text.Text))
            {
                MessageBox.Show("请输入您本次旅程的时间限制");
            }
            else if (Limit_rad.Checked && !Regex.IsMatch(Time_text.Text, @"^\d+$"))
            {
                MessageBox.Show("请输入正确的时间！\n应输入一个正整数");
                Time_text.Clear();
            }
            else
            {
                if (Limit_rad.Checked)
                    newOne.LimitTime = Convert.ToInt32(Time_text.Text);

                newOne.LimitTimeStrategy = Limit_rad.Checked;
                GlobleVariable.users.Add(newOne);
                MessageBox.Show("您的信息录入成功！\n即将生成推荐旅行路径......");

                // 信息录入后应当记录
                using (FileStream logWriter = new FileStream(GlobleVariable.logPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    logWriter.Seek(0, SeekOrigin.End);

                    string content = DateTime.Now.ToString() + "(程序内时间：" + GlobleVariable.dt.ToString() + "): ";
                    content += UserName.Text + "设定了";
                    content += "始发城市" + newOne.Location + "，";
                    content += "目标城市" + GlobleVariable.cityMapping[newOne.Dest] + "。";
                    content += "初始交通枢纽为" + Vehicle_cob.SelectedItem.ToString() + "。";
                    if (Limit_rad.Checked)
                        content += "选择了限时最小风险策略。限定时间为" + newOne.LimitTime + "小时\n\n";
                    else
                        content += "选择了最小风险策略。\n\n";
                    byte[] buffer = Encoding.Default.GetBytes(content);
                    logWriter.Write(buffer, 0, buffer.Length);
                }


                ShowPath_win showPath = new ShowPath_win(GlobleVariable.users.Count - 1, _updateUserList);
                showPath.ShowDialog();
                Close();

            }

        }

        /// <summary>
        /// 按下取消键后的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 窗体加载时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUser_Load(object sender, EventArgs e)
        {
            newOne = new User(0, 0, GlobleVariable.dt, 0);
            UserName.Text = "用户" + GlobleVariable.users.Count.ToString();
        }

        /// <summary>
        /// 选择始发地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OriginCities_cob_SelectedIndexChanged(object sender, EventArgs e)
        {
            newOne.Origin = OriginCities_cob.SelectedIndex;
            newOne.Location = GlobleVariable.cityMapping[newOne.Origin];
        }

        /// <summary>
        /// 选择目的地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestCities_cob_SelectedIndexChanged(object sender, EventArgs e)
        {
            newOne.Dest = DestCities_cob.SelectedIndex;
        }

        /// <summary>
        /// 选择初始交通工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Vehicle_cob_SelectedIndexChanged(object sender, EventArgs e)
        {
            newOne.Vehicle = Vehicle_cob.SelectedIndex;
        }

        /// <summary>
        /// 选择不同策略
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Limit_rad_CheckedChanged(object sender, EventArgs e)
        {
            if (Limit_rad.Checked)
            {
                Limit_label.Visible = true;
                Time_text.Visible = true;
            }
            else
            {
                Limit_label.Visible = false;
                Time_text.Visible = false;
            }
        }
    }
}
