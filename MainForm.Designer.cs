namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MapPicture = new System.Windows.Forms.PictureBox();
            this.BeijingButton = new System.Windows.Forms.Button();
            this.ShenzhenButton = new System.Windows.Forms.Button();
            this.ShanghaiButton = new System.Windows.Forms.Button();
            this.ChengduButton = new System.Windows.Forms.Button();
            this.KunmingButton = new System.Windows.Forms.Button();
            this.HangzhouButton = new System.Windows.Forms.Button();
            this.ChongqingButton = new System.Windows.Forms.Button();
            this.XianButton = new System.Windows.Forms.Button();
            this.DalianButton = new System.Windows.Forms.Button();
            this.WuhanButton = new System.Windows.Forms.Button();
            this.AddUser = new System.Windows.Forms.Button();
            this.CurTime = new System.Windows.Forms.Label();
            this.UpdateMainForm = new System.Windows.Forms.Timer(this.components);
            this.UsersList = new System.Windows.Forms.ListBox();
            this.Comment = new System.Windows.Forms.Label();
            this.CityInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NewIntervalNumber = new System.Windows.Forms.TextBox();
            this.ChangeInterval = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ChangeLogPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MapPicture)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapPicture
            // 
            this.MapPicture.Image = global::战疫旅人.Properties.Resources.中国地图;
            this.MapPicture.Location = new System.Drawing.Point(452, 12);
            this.MapPicture.Name = "MapPicture";
            this.MapPicture.Size = new System.Drawing.Size(800, 587);
            this.MapPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MapPicture.TabIndex = 0;
            this.MapPicture.TabStop = false;
            this.MapPicture.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.PictureBox1_LoadCompleted);
            // 
            // BeijingButton
            // 
            this.BeijingButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BeijingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BeijingButton.Location = new System.Drawing.Point(955, 244);
            this.BeijingButton.Name = "BeijingButton";
            this.BeijingButton.Size = new System.Drawing.Size(16, 17);
            this.BeijingButton.TabIndex = 1;
            this.BeijingButton.UseVisualStyleBackColor = false;
            this.BeijingButton.Click += new System.EventHandler(this.BeijingButton_Click);
            // 
            // ShenzhenButton
            // 
            this.ShenzhenButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ShenzhenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShenzhenButton.Location = new System.Drawing.Point(943, 486);
            this.ShenzhenButton.Name = "ShenzhenButton";
            this.ShenzhenButton.Size = new System.Drawing.Size(16, 17);
            this.ShenzhenButton.TabIndex = 2;
            this.ShenzhenButton.UseVisualStyleBackColor = false;
            this.ShenzhenButton.Click += new System.EventHandler(this.ShenzhenButton_Click);
            // 
            // ShanghaiButton
            // 
            this.ShanghaiButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ShanghaiButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShanghaiButton.Location = new System.Drawing.Point(1031, 364);
            this.ShanghaiButton.Name = "ShanghaiButton";
            this.ShanghaiButton.Size = new System.Drawing.Size(16, 17);
            this.ShanghaiButton.TabIndex = 3;
            this.ShanghaiButton.UseVisualStyleBackColor = false;
            this.ShanghaiButton.Click += new System.EventHandler(this.ShanghaiButton_Click);
            // 
            // ChengduButton
            // 
            this.ChengduButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ChengduButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChengduButton.Location = new System.Drawing.Point(820, 377);
            this.ChengduButton.Name = "ChengduButton";
            this.ChengduButton.Size = new System.Drawing.Size(16, 17);
            this.ChengduButton.TabIndex = 4;
            this.ChengduButton.UseVisualStyleBackColor = false;
            this.ChengduButton.Click += new System.EventHandler(this.ChengduButton_Click);
            // 
            // KunmingButton
            // 
            this.KunmingButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.KunmingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KunmingButton.Location = new System.Drawing.Point(798, 451);
            this.KunmingButton.Name = "KunmingButton";
            this.KunmingButton.Size = new System.Drawing.Size(16, 17);
            this.KunmingButton.TabIndex = 5;
            this.KunmingButton.UseVisualStyleBackColor = false;
            this.KunmingButton.Click += new System.EventHandler(this.KunmingButton_Click);
            // 
            // HangzhouButton
            // 
            this.HangzhouButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.HangzhouButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HangzhouButton.Location = new System.Drawing.Point(1009, 377);
            this.HangzhouButton.Name = "HangzhouButton";
            this.HangzhouButton.Size = new System.Drawing.Size(16, 17);
            this.HangzhouButton.TabIndex = 6;
            this.HangzhouButton.UseVisualStyleBackColor = false;
            this.HangzhouButton.Click += new System.EventHandler(this.HangzhouButton_Click);
            // 
            // ChongqingButton
            // 
            this.ChongqingButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ChongqingButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChongqingButton.Location = new System.Drawing.Point(842, 393);
            this.ChongqingButton.Name = "ChongqingButton";
            this.ChongqingButton.Size = new System.Drawing.Size(16, 17);
            this.ChongqingButton.TabIndex = 7;
            this.ChongqingButton.UseVisualStyleBackColor = false;
            this.ChongqingButton.Click += new System.EventHandler(this.ChongqingButton_Click);
            // 
            // XianButton
            // 
            this.XianButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.XianButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.XianButton.Location = new System.Drawing.Point(878, 326);
            this.XianButton.Name = "XianButton";
            this.XianButton.Size = new System.Drawing.Size(16, 17);
            this.XianButton.TabIndex = 8;
            this.XianButton.UseVisualStyleBackColor = false;
            this.XianButton.Click += new System.EventHandler(this.XianButton_Click);
            // 
            // DalianButton
            // 
            this.DalianButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.DalianButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DalianButton.Location = new System.Drawing.Point(1031, 225);
            this.DalianButton.Name = "DalianButton";
            this.DalianButton.Size = new System.Drawing.Size(16, 17);
            this.DalianButton.TabIndex = 9;
            this.DalianButton.UseVisualStyleBackColor = false;
            this.DalianButton.Click += new System.EventHandler(this.DalianButton_Click);
            // 
            // WuhanButton
            // 
            this.WuhanButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.WuhanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WuhanButton.Location = new System.Drawing.Point(943, 378);
            this.WuhanButton.Name = "WuhanButton";
            this.WuhanButton.Size = new System.Drawing.Size(16, 17);
            this.WuhanButton.TabIndex = 10;
            this.WuhanButton.UseVisualStyleBackColor = false;
            this.WuhanButton.Click += new System.EventHandler(this.WuhanButton_Click);
            // 
            // AddUser
            // 
            this.AddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddUser.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AddUser.Location = new System.Drawing.Point(798, 606);
            this.AddUser.Name = "AddUser";
            this.AddUser.Size = new System.Drawing.Size(150, 63);
            this.AddUser.TabIndex = 12;
            this.AddUser.Text = "添加用户";
            this.AddUser.UseVisualStyleBackColor = true;
            this.AddUser.Click += new System.EventHandler(this.AddUser_Click);
            // 
            // CurTime
            // 
            this.CurTime.AutoSize = true;
            this.CurTime.BackColor = System.Drawing.Color.Transparent;
            this.CurTime.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CurTime.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CurTime.Location = new System.Drawing.Point(156, 42);
            this.CurTime.Name = "CurTime";
            this.CurTime.Size = new System.Drawing.Size(251, 38);
            this.CurTime.TabIndex = 14;
            this.CurTime.Text = "0001-01-01 12时";
            // 
            // UpdateMainForm
            // 
            this.UpdateMainForm.Enabled = true;
            this.UpdateMainForm.Interval = 10000;
            this.UpdateMainForm.Tick += new System.EventHandler(this.UpdateMainForm_Tick_1);
            // 
            // UsersList
            // 
            this.UsersList.BackColor = System.Drawing.Color.DarkSlateGray;
            this.UsersList.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UsersList.ForeColor = System.Drawing.SystemColors.Info;
            this.UsersList.FormattingEnabled = true;
            this.UsersList.ItemHeight = 25;
            this.UsersList.Location = new System.Drawing.Point(17, 189);
            this.UsersList.Name = "UsersList";
            this.UsersList.Size = new System.Drawing.Size(111, 404);
            this.UsersList.TabIndex = 15;
            this.UsersList.SelectedIndexChanged += new System.EventHandler(this.UsersList_SelectedIndexChanged);
            this.UsersList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.UsersList_MouseDoubleClick);
            // 
            // Comment
            // 
            this.Comment.AutoSize = true;
            this.Comment.BackColor = System.Drawing.Color.Transparent;
            this.Comment.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Comment.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.Comment.Location = new System.Drawing.Point(158, 89);
            this.Comment.Name = "Comment";
            this.Comment.Size = new System.Drawing.Size(243, 147);
            this.Comment.TabIndex = 16;
            this.Comment.Text = "从0：00开始：\r\n    每2小时一班汽车\r\n    每4小时一班火车\r\n    每8小时一班飞机\r\n\r\n默认时间每10秒前进1小时\r\n可在下方修改前进1小时所" +
    "需秒数";
            // 
            // CityInfo
            // 
            this.CityInfo.BackColor = System.Drawing.Color.Transparent;
            this.CityInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CityInfo.Location = new System.Drawing.Point(148, 274);
            this.CityInfo.Name = "CityInfo";
            this.CityInfo.Size = new System.Drawing.Size(283, 319);
            this.CityInfo.TabIndex = 17;
            this.CityInfo.Text = "\r\n\r\n\r\n\r\n\r\n\r\n\r\n选定相应的城市以在此显示其详细信息";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label2.Location = new System.Drawing.Point(16, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 105);
            this.label2.TabIndex = 18;
            this.label2.Text = "图例\r\n总路径：\r\n正在经过：\r\n已完成：\r\n停留：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(112, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "    ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(112, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "    ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Lime;
            this.label5.Location = new System.Drawing.Point(112, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "    ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(452, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(159, 123);
            this.panel1.TabIndex = 22;
            // 
            // NewIntervalNumber
            // 
            this.NewIntervalNumber.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.NewIntervalNumber.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NewIntervalNumber.Location = new System.Drawing.Point(163, 244);
            this.NewIntervalNumber.Name = "NewIntervalNumber";
            this.NewIntervalNumber.Size = new System.Drawing.Size(107, 21);
            this.NewIntervalNumber.TabIndex = 24;
            this.NewIntervalNumber.Text = "请在此处输入整数";
            this.NewIntervalNumber.Click += new System.EventHandler(this.NewIntervalNumber_Click);
            // 
            // ChangeInterval
            // 
            this.ChangeInterval.Location = new System.Drawing.Point(328, 243);
            this.ChangeInterval.Name = "ChangeInterval";
            this.ChangeInterval.Size = new System.Drawing.Size(71, 22);
            this.ChangeInterval.TabIndex = 25;
            this.ChangeInterval.Text = "确认";
            this.ChangeInterval.UseVisualStyleBackColor = true;
            this.ChangeInterval.Click += new System.EventHandler(this.ChangeInterval_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label6.Location = new System.Drawing.Point(11, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 68);
            this.label6.TabIndex = 26;
            this.label6.Text = "  单击用户显示路径\r\n   （每小时刷新）\r\n\r\n双击用户显示详细信息";
            // 
            // ChangeLogPath
            // 
            this.ChangeLogPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeLogPath.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChangeLogPath.Location = new System.Drawing.Point(337, 606);
            this.ChangeLogPath.Name = "ChangeLogPath";
            this.ChangeLogPath.Size = new System.Drawing.Size(150, 63);
            this.ChangeLogPath.TabIndex = 27;
            this.ChangeLogPath.Text = "更改日志路径";
            this.ChangeLogPath.UseVisualStyleBackColor = true;
            this.ChangeLogPath.Click += new System.EventHandler(this.ChangeLogPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(112, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "    ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = global::战疫旅人.Properties.Resources.主窗体背景;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.ChangeLogPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ChangeInterval);
            this.Controls.Add(this.NewIntervalNumber);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CityInfo);
            this.Controls.Add(this.Comment);
            this.Controls.Add(this.UsersList);
            this.Controls.Add(this.CurTime);
            this.Controls.Add(this.AddUser);
            this.Controls.Add(this.WuhanButton);
            this.Controls.Add(this.DalianButton);
            this.Controls.Add(this.XianButton);
            this.Controls.Add(this.ChongqingButton);
            this.Controls.Add(this.HangzhouButton);
            this.Controls.Add(this.KunmingButton);
            this.Controls.Add(this.ChengduButton);
            this.Controls.Add(this.ShanghaiButton);
            this.Controls.Add(this.ShenzhenButton);
            this.Controls.Add(this.BeijingButton);
            this.Controls.Add(this.MapPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "战疫旅人";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MapPicture)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox MapPicture;
        private System.Windows.Forms.Button BeijingButton;
        private System.Windows.Forms.Button ShenzhenButton;
        private System.Windows.Forms.Button ShanghaiButton;
        private System.Windows.Forms.Button ChengduButton;
        private System.Windows.Forms.Button KunmingButton;
        private System.Windows.Forms.Button HangzhouButton;
        private System.Windows.Forms.Button ChongqingButton;
        private System.Windows.Forms.Button XianButton;
        private System.Windows.Forms.Button DalianButton;
        private System.Windows.Forms.Button WuhanButton;
        private System.Windows.Forms.Button AddUser;
        private System.Windows.Forms.Label CurTime;
        private System.Windows.Forms.Timer UpdateMainForm;
        private System.Windows.Forms.ListBox UsersList;
        private System.Windows.Forms.Label Comment;
        private System.Windows.Forms.Label CityInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox NewIntervalNumber;
        private System.Windows.Forms.Button ChangeInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ChangeLogPath;
        private System.Windows.Forms.Label label1;
    }
}

