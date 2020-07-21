using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    public partial class MainForm : Form
    {
        Graphics g; // 绘制地图上的路径的GDI+对象
        private int _selectedIndex = -1;    // 当前选中的用户ID

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 委托：UpdateUserList 的函数，作用为删除UsersList列表上的用户
        /// </summary>
        /// <param name="index">用户ID</param>
        private void DelUserList(int index)
        {
            UsersList.Items.RemoveAt(index);
        }

        /// <summary>
        /// 地图加载完成事件，将把地图置于背景之上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MapPicture.BringToFront();
        }

        /// <summary>
        /// 单击添加用户按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUser_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser(DelUserList);
            addUser.ShowDialog();
        }


        /// <summary>
        /// 每10秒一个周期，更新主视窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateMainForm_Tick_1(object sender, EventArgs e)
        {

            GlobleVariable.dt = GlobleVariable.dt.AddHours(1);
            CurTime.Text = GlobleVariable.dt.ToString("yyyy-MM-dd HH") + "时";
            int index = 0;
            UsersList.Items.Clear();
            while (GlobleVariable.users.Count > index)
            {
                UsersList.Items.Add("用户" + index.ToString());
                index++;
            }

            using (FileStream logWriter = new FileStream(GlobleVariable.logPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                logWriter.Seek(0, SeekOrigin.End);

                string content = "\n\n\n\n进入程序内时间：" + GlobleVariable.dt.ToString() + "\n\n";

                byte[] buffer = Encoding.Default.GetBytes(content);
                logWriter.Write(buffer, 0, buffer.Length);
            }

            // 更新每一个用户的状态
            for (int i = 0; i < GlobleVariable.users.Count; i++)
            {
                int timeElapse = (int)GlobleVariable.dt.Subtract(GlobleVariable.users[i].OriginTime).TotalHours;
                int timeTotal = 0;
                int vehicle;

                if (timeElapse >= PathFinding.CountPathTime(GlobleVariable.users[i].RecommandPath))
                {
                    GlobleVariable.users[i].IsArrive = true;
                    GlobleVariable.users[i].State = 0;
                    GlobleVariable.users[i].Location = GlobleVariable.cityMapping[GlobleVariable.users[i].Dest] + "（已到达）";
                }

                #region 更新用户位置数据
                if (!GlobleVariable.users[i].IsArrive)
                {
                    for (int j = 0; GlobleVariable.users[i].RecommandPath[j + 1] != null && j < GlobleVariable.users[i].RecommandPath.Length - 1; j++)
                    {
                        int nextCityTime = GlobleVariable.users[i].RecommandPath[j].TimeInfo[GlobleVariable.users[i].RecommandPath[j + 1].CityCode];
                        timeTotal += nextCityTime;
                        vehicle = GlobleVariable.users[i].RecommandPath[j].VehicleInfo[GlobleVariable.users[i].RecommandPath[j + 1].CityCode];
                        if (timeTotal > timeElapse)
                        {
                            int stayTime;
                            switch (vehicle)
                            {
                                case 1:
                                    stayTime = GlobleVariable.users[i].RecommandPath[j].MinPlaneTime;
                                    break;
                                case 2:
                                    stayTime = GlobleVariable.users[i].RecommandPath[j].MinTrainTime;
                                    break;
                                case 3:
                                    stayTime = GlobleVariable.users[i].RecommandPath[j].MinCarTime;
                                    break;
                                default:
                                    throw new NotImplementedException("路径时间处理失败");
                            }
                            if (timeTotal - nextCityTime + stayTime < timeElapse)
                            {
                                GlobleVariable.users[i].State = 1;
                                GlobleVariable.users[i].Location = GlobleVariable.users[i].RecommandPath[j].Name + " 到 " + GlobleVariable.users[i].RecommandPath[j + 1].Name;
                                GlobleVariable.users[i].NextCityReachTime = GlobleVariable.users[i].CityReachTime[j + 1];
                                GlobleVariable.users[i].Vehicle = vehicle;
                            }
                            else
                            {
                                GlobleVariable.users[i].State = 0;
                                GlobleVariable.users[i].Location = GlobleVariable.users[i].RecommandPath[j].Name;
                            }
                            break;
                        }
                    }

                    // 每个单位时间的用户状态改变写入日志
                    using (FileStream logWriter = new FileStream(GlobleVariable.logPath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        logWriter.Seek(0, SeekOrigin.End);

                        string content = DateTime.Now.ToString() + "(程序内时间：" + GlobleVariable.dt.ToString() + "): ";
                        content += "用户" + i.ToString();
                        if (GlobleVariable.users[i].State == 0)
                            content += "正逗留在" + GlobleVariable.users[i].Location + "。";
                        else
                            content += "正在从" + GlobleVariable.users[i].Location + "，使用交通工具为" + GlobleVariable.vehicleMapping[GlobleVariable.users[i].Vehicle] + "。";
                        content += "已经旅行了" + (int)(GlobleVariable.dt - GlobleVariable.users[i].OriginTime).TotalHours
                            + "小时（" + GlobleVariable.users[i].OriginTime.ToShortDateString() + " " + GlobleVariable.users[i].OriginTime.Hour.ToString() + "时 出发）。 \n\n";

                        byte[] buffer = Encoding.Default.GetBytes(content);
                        logWriter.Write(buffer, 0, buffer.Length);
                    }
                }
                else
                {
                    using (FileStream logWriter = new FileStream(GlobleVariable.logPath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        logWriter.Seek(0, SeekOrigin.End);

                        string content = DateTime.Now.ToString() + "(程序内时间：" + GlobleVariable.dt.ToString() + "): ";
                        content += "用户" + i.ToString() + "已到达目的地\n\n";

                        byte[] buffer = Encoding.Default.GetBytes(content);
                        logWriter.Write(buffer, 0, buffer.Length);
                    }
                }
                #endregion



            }


            // 保持选中路径的绘图更新
            if (UsersList.SelectedIndex != -1 || _selectedIndex != -1)
            {
                // 清空画板
                MapPicture.Refresh();
                g = MapPicture.CreateGraphics();

                // 创建画笔，设置箭头
                Pen pen_Blue = new Pen(Color.Blue, 4);
                Pen pen_Red = new Pen(Color.Red, 4);
                Pen pen_Green = new Pen(Color.Lime, 4);
                Pen pen_Black = new Pen(Color.Orange, 6);
                pen_Blue.EndCap = LineCap.ArrowAnchor;
                pen_Red.EndCap = LineCap.ArrowAnchor;
                pen_Green.EndCap = LineCap.ArrowAnchor;

                User selectedUser = GlobleVariable.users[_selectedIndex];



                for (int i = 0; selectedUser.RecommandPath[i + 1] != null; i++)
                {
                    if (selectedUser.IsArrive)
                    {
                        g.DrawLine(pen_Green, selectedUser.RecommandPath[i].mapPos_x - MapPicture.Location.X, selectedUser.RecommandPath[i].mapPos_y - MapPicture.Location.Y,
                        selectedUser.RecommandPath[i + 1].mapPos_x - MapPicture.Location.X, selectedUser.RecommandPath[i + 1].mapPos_y - MapPicture.Location.Y);
                    }
                    else
                    {
                        g.DrawLine(pen_Blue, selectedUser.RecommandPath[i].mapPos_x - MapPicture.Location.X, selectedUser.RecommandPath[i].mapPos_y - MapPicture.Location.Y,
                            selectedUser.RecommandPath[i + 1].mapPos_x - MapPicture.Location.X, selectedUser.RecommandPath[i + 1].mapPos_y - MapPicture.Location.Y);
                    }
                }

                if (selectedUser.State == 1)
                {
                    string[] strArr = selectedUser.Location.Split();
                    int index_1 = Array.IndexOf(GlobleVariable.cityMapping, strArr[0]);
                    int index_2 = Array.IndexOf(GlobleVariable.cityMapping, strArr[2]);

                    g.DrawLine(pen_Red, GlobleVariable.cityPos[index_1, 0] - MapPicture.Location.X, GlobleVariable.cityPos[index_1, 1] - MapPicture.Location.Y,
                        GlobleVariable.cityPos[index_2, 0] - MapPicture.Location.X, GlobleVariable.cityPos[index_2, 1] - MapPicture.Location.Y);
                }
                else if (selectedUser.IsArrive == false)
                {
                    int index_tmp = Array.IndexOf(GlobleVariable.cityMapping, selectedUser.Location);

                    g.DrawRectangle(pen_Black, GlobleVariable.cityPos[index_tmp, 0] - MapPicture.Location.X, GlobleVariable.cityPos[index_tmp, 1] - MapPicture.Location.Y, 6, 6);
                }
            }

        }

        /// <summary>
        /// 主视窗为焦点时开始计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Activated(object sender, EventArgs e)
        {
            UpdateMainForm.Start();
        }

        /// <summary>
        /// 主视窗失焦时停止计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            UpdateMainForm.Stop();
        }

        /// <summary>
        /// 主视窗第一次加载的初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            CurTime.Text = GlobleVariable.dt.ToString("yyyy-MM-dd HH") + "时";
            UpdateMainForm.Start();
        }

        /// <summary>
        /// 双击用户列表事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = UsersList.IndexFromPoint(e.Location);

            if (index != ListBox.NoMatches)
            {
                ShowPath_win pathInfo = new ShowPath_win(UsersList.SelectedIndex, DelUserList);
                pathInfo.ShowDialog();
            }
        }

        /// <summary>
        /// 单击用户列事件，主要内容是路径的绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 清空画板
            MapPicture.Refresh();
            g = MapPicture.CreateGraphics();

            // 创建画笔，设置箭头
            Pen pen_Blue = new Pen(Color.Blue, 4);
            Pen pen_Red = new Pen(Color.Red, 4);
            Pen pen_Green = new Pen(Color.Lime, 4);
            Pen pen_Orange = new Pen(Color.Orange, 6);
            pen_Blue.EndCap = LineCap.ArrowAnchor;
            pen_Red.EndCap = LineCap.ArrowAnchor;
            pen_Green.EndCap = LineCap.ArrowAnchor;

            // 当刚刚删除用户时，应当加入该判断以防出错
            if (UsersList.SelectedIndex == -1)
                return;

            _selectedIndex = UsersList.SelectedIndex;
            User selectedUser = GlobleVariable.users[_selectedIndex];



            for (int i = 0; selectedUser.RecommandPath[i + 1] != null; i++)
            {
                if (selectedUser.IsArrive)
                {
                    g.DrawLine(pen_Green, selectedUser.RecommandPath[i].mapPos_x - MapPicture.Location.X, selectedUser.RecommandPath[i].mapPos_y - MapPicture.Location.Y,
                    selectedUser.RecommandPath[i + 1].mapPos_x - MapPicture.Location.X, selectedUser.RecommandPath[i + 1].mapPos_y - MapPicture.Location.Y);
                }
                else
                {
                    g.DrawLine(pen_Blue, selectedUser.RecommandPath[i].mapPos_x - MapPicture.Location.X, selectedUser.RecommandPath[i].mapPos_y - MapPicture.Location.Y,
                        selectedUser.RecommandPath[i + 1].mapPos_x - MapPicture.Location.X, selectedUser.RecommandPath[i + 1].mapPos_y - MapPicture.Location.Y);
                }
            }

            if (selectedUser.State == 1)
            {
                string[] strArr = selectedUser.Location.Split();
                int index_1 = Array.IndexOf(GlobleVariable.cityMapping, strArr[0]);
                int index_2 = Array.IndexOf(GlobleVariable.cityMapping, strArr[2]);

                g.DrawLine(pen_Red, GlobleVariable.cityPos[index_1, 0] - MapPicture.Location.X, GlobleVariable.cityPos[index_1, 1] - MapPicture.Location.Y,
                    GlobleVariable.cityPos[index_2, 0] - MapPicture.Location.X, GlobleVariable.cityPos[index_2, 1] - MapPicture.Location.Y);
            }
            else if (selectedUser.IsArrive == false)
            {
                int index_tmp = Array.IndexOf(GlobleVariable.cityMapping, selectedUser.Location);

                g.DrawRectangle(pen_Orange, GlobleVariable.cityPos[index_tmp, 0] - MapPicture.Location.X, GlobleVariable.cityPos[index_tmp, 1] - MapPicture.Location.Y, 6, 6);
            }
        }

        /// <summary>
        /// 系统时钟周期修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeInterval_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(NewIntervalNumber.Text, @"^\d+$"))
            {
                UpdateMainForm.Interval = Convert.ToInt32(NewIntervalNumber.Text) * 1000;

                // 记录用户更改系统单位时间
                using (FileStream logWriter = new FileStream(GlobleVariable.logPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    logWriter.Seek(0, SeekOrigin.End);

                    string content = DateTime.Now.ToString() + "(程序内时间：" + GlobleVariable.dt.ToString() + "): ";
                    content += "系统单位时间已更改为现实时间的" + NewIntervalNumber.Text + "秒\n\n";

                    byte[] buffer = Encoding.Default.GetBytes(content);
                    logWriter.Write(buffer, 0, buffer.Length);
                }

                MessageBox.Show("修改成功！");
            }
            else
                MessageBox.Show("请输入正确的数字");
        }

        /// <summary>
        /// 系统时钟周期输入框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewIntervalNumber_Click(object sender, EventArgs e)
        {
            NewIntervalNumber.Text = "";
        }

        /// <summary>
        /// 改变日志文件路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeLogPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                InitialDirectory = GlobleVariable.logPath,
                Title = "请选择日志文件要保存的路径",
                Filter = "文本文件|*.txt|所有文件|*.*"
            };
            sfd.ShowDialog();

            GlobleVariable.logPath = sfd.FileName;

            if (GlobleVariable.logPath == "")
                GlobleVariable.logPath = Application.StartupPath + @"\log.txt";
            else if (File.Exists(GlobleVariable.logPath))
                File.Delete(GlobleVariable.logPath);
        }

        /*  以下均为显示城市具体信息事件 */

        private void BeijingButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "北京市\n" + "----\n" + "风险等级：\n" + "    高风险城市\n" + "----\n"
                + "交通方式有：\n"
                + "    1. 航班：北京-上海；北京-深圳；北京-重庆\n"
                + "    2. 列车：北京-深圳；北京-昆明；北京-杭州；北京-西安；北京-大连\n"
                + "    3. 汽车：北京-上海；北京-杭州；北京-西安；北京-大连；北京-武汉\n";
        }

        private void ShanghaiButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "上海市\n" + "----\n" + "风险等级：\n" + "    高风险城市\n" + "----\n"
                + "交通方式有：\n"
                + "    1. 航班：上海-北京；上海-深圳；上海-昆明\n"
                + "    2. 列车：上海-昆明；上海-杭州；上海-武汉\n"
                + "    3. 汽车：上海-北京；上海-昆明；上海-杭州；上海-西安\n";
        }

        private void ShenzhenButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "深圳市\n" + "----\n" + "风险等级：\n" + "    高风险城市\n" + "----\n"
                + "交通方式有：\n"
                + "    1. 航班：深圳-北京；深圳-上海；深圳-成都；深圳-西安；深圳-大连\n"
                + "    2. 列车：深圳-北京；深圳-武汉\n"
                + "    3. 汽车：深圳-昆明；深圳-杭州；深圳-武汉\n";
        }

        private void ChengduButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "成都市\n" + "----\n" + "风险等级：\n" + "    低风险城市\n" + "----\n"
                + "交通方式有：\n"
                + "    1. 航班：成都-深圳\n"
                + "    2. 列车：成都-昆明；成都-西安\n"
                + "    3. 汽车：成都-昆明；成都-西安；成都-武汉\n";
        }

        private void KunmingButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "昆明市\n" + "----\n" + "风险等级：\n" + "    中风险城市\n" + "----\n"
               + "交通方式有：\n"
               + "    1. 航班：昆明-上海；昆明-大连\n"
               + "    2. 列车：昆明-北京；昆明-上海；昆明-成都；昆明-重庆；昆明-武汉\n"
               + "    3. 汽车：昆明-上海；昆明-深圳；昆明-成都；昆明-重庆\n";
        }

        private void HangzhouButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "杭州市\n" + "----\n" + "风险等级：\n" + "    中风险城市\n" + "----\n"
               + "交通方式有：\n"
               + "    1. 航班：杭州-大连\n"
               + "    2. 列车：杭州-北京；杭州-上海；杭州-武汉\n"
               + "    3. 汽车：杭州-北京；杭州-上海；杭州-深圳；杭州-重庆；杭州-武汉\n";
        }

        private void ChongqingButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "重庆市\n" + "----\n" + "风险等级：\n" + "    低风险城市\n" + "----\n"
              + "交通方式有：\n"
              + "    1. 航班：重庆-北京\n"
              + "    2. 列车：重庆-昆明；重庆-西安\n"
              + "    3. 汽车：重庆-昆明；重庆-杭州；重庆-西安；重庆-武汉\n";
        }

        private void XianButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "西安市\n" + "----\n" + "风险等级：\n" + "    中风险城市\n" + "----\n"
              + "交通方式有：\n"
              + "    1. 航班：西安-深圳\n"
              + "    2. 列车：西安-北京；西安-成都；西安-重庆\n"
              + "    3. 汽车：西安-北京；西安-上海；西安-成都；西安-重庆；西安-武汉\n";
        }

        private void DalianButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "大连市\n" + "----\n" + "风险等级：\n" + "    低风险城市\n" + "----\n"
             + "交通方式有：\n"
             + "    1. 航班：大连-深圳；大连-昆明；大连-杭州\n"
             + "    2. 列车：大连-北京；大连-武汉\n"
             + "    3. 汽车：大连-北京\n";
        }

        private void WuhanButton_Click(object sender, EventArgs e)
        {
            CityInfo.Text = "武汉市\n" + "----\n" + "风险等级：\n" + "    高风险城市\n" + "----\n"
            + "交通方式有：\n"
            + "    1. 航班：无\n"
            + "    2. 列车：武汉-上海；武汉-深圳；武汉-昆明；武汉-杭州；武汉-大连\n"
            + "    3. 汽车：武汉-北京；武汉-深圳；武汉-成都；武汉-杭州；武汉-重庆；武汉-西安\n";
        }
    }
}
