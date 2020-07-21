namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    partial class AddUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Accept = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.OriginCities_cob = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DestCities_cob = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Vehicle_cob = new System.Windows.Forms.ComboBox();
            this.NoLimit_rad = new System.Windows.Forms.RadioButton();
            this.Limit_rad = new System.Windows.Forms.RadioButton();
            this.Limit_label = new System.Windows.Forms.Label();
            this.Time_text = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Accept
            // 
            this.Accept.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Accept.Location = new System.Drawing.Point(153, 364);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(111, 58);
            this.Accept.TabIndex = 0;
            this.Accept.Text = "确定";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.Location = new System.Drawing.Point(523, 364);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(111, 58);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "取消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // OriginCities_cob
            // 
            this.OriginCities_cob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OriginCities_cob.FormattingEnabled = true;
            this.OriginCities_cob.Items.AddRange(new object[] {
            "北京",
            "上海",
            "深圳",
            "成都",
            "昆明",
            "杭州",
            "重庆",
            "西安",
            "大连",
            "武汉"});
            this.OriginCities_cob.Location = new System.Drawing.Point(367, 70);
            this.OriginCities_cob.Name = "OriginCities_cob";
            this.OriginCities_cob.Size = new System.Drawing.Size(121, 20);
            this.OriginCities_cob.TabIndex = 2;
            this.OriginCities_cob.SelectedIndexChanged += new System.EventHandler(this.OriginCities_cob_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(273, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "始发城市：";
            // 
            // DestCities_cob
            // 
            this.DestCities_cob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DestCities_cob.FormattingEnabled = true;
            this.DestCities_cob.Items.AddRange(new object[] {
            "北京",
            "上海",
            "深圳",
            "成都",
            "昆明",
            "杭州",
            "重庆",
            "西安",
            "大连",
            "武汉"});
            this.DestCities_cob.Location = new System.Drawing.Point(367, 121);
            this.DestCities_cob.Name = "DestCities_cob";
            this.DestCities_cob.Size = new System.Drawing.Size(121, 20);
            this.DestCities_cob.TabIndex = 4;
            this.DestCities_cob.SelectedIndexChanged += new System.EventHandler(this.DestCities_cob_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(273, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "目的城市：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(217, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "当前所在交通枢纽：";
            // 
            // Vehicle_cob
            // 
            this.Vehicle_cob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Vehicle_cob.FormattingEnabled = true;
            this.Vehicle_cob.Items.AddRange(new object[] {
            "系统默认",
            "飞机场",
            "火车站",
            "汽车站"});
            this.Vehicle_cob.Location = new System.Drawing.Point(367, 170);
            this.Vehicle_cob.Name = "Vehicle_cob";
            this.Vehicle_cob.Size = new System.Drawing.Size(121, 20);
            this.Vehicle_cob.TabIndex = 6;
            this.Vehicle_cob.SelectedIndexChanged += new System.EventHandler(this.Vehicle_cob_SelectedIndexChanged);
            // 
            // NoLimit_rad
            // 
            this.NoLimit_rad.AutoSize = true;
            this.NoLimit_rad.Checked = true;
            this.NoLimit_rad.Location = new System.Drawing.Point(367, 217);
            this.NoLimit_rad.Name = "NoLimit_rad";
            this.NoLimit_rad.Size = new System.Drawing.Size(95, 16);
            this.NoLimit_rad.TabIndex = 8;
            this.NoLimit_rad.TabStop = true;
            this.NoLimit_rad.Text = "最少风险策略";
            this.NoLimit_rad.UseVisualStyleBackColor = true;
            // 
            // Limit_rad
            // 
            this.Limit_rad.AutoSize = true;
            this.Limit_rad.Location = new System.Drawing.Point(367, 253);
            this.Limit_rad.Name = "Limit_rad";
            this.Limit_rad.Size = new System.Drawing.Size(119, 16);
            this.Limit_rad.TabIndex = 9;
            this.Limit_rad.Text = "限时最少风险策略";
            this.Limit_rad.UseVisualStyleBackColor = true;
            this.Limit_rad.CheckedChanged += new System.EventHandler(this.Limit_rad_CheckedChanged);
            // 
            // Limit_label
            // 
            this.Limit_label.AutoSize = true;
            this.Limit_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Limit_label.Location = new System.Drawing.Point(175, 288);
            this.Limit_label.Name = "Limit_label";
            this.Limit_label.Size = new System.Drawing.Size(177, 20);
            this.Limit_label.TabIndex = 10;
            this.Limit_label.Text = "限定时间（单位：小时）：";
            this.Limit_label.Visible = false;
            // 
            // Time_text
            // 
            this.Time_text.Location = new System.Drawing.Point(367, 288);
            this.Time_text.Name = "Time_text";
            this.Time_text.Size = new System.Drawing.Size(121, 21);
            this.Time_text.TabIndex = 11;
            this.Time_text.Visible = false;
            this.Time_text.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(231, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "低风险旅行策略：";
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UserName.Location = new System.Drawing.Point(365, 27);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(74, 21);
            this.UserName.TabIndex = 13;
            this.UserName.Text = "用户名称";
            // 
            // AddUser
            // 
            this.AcceptButton = this.Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Time_text);
            this.Controls.Add(this.Limit_label);
            this.Controls.Add(this.Limit_rad);
            this.Controls.Add(this.NoLimit_rad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Vehicle_cob);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DestCities_cob);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OriginCities_cob);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Accept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddUser";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加新用户";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AddUser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ComboBox OriginCities_cob;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox DestCities_cob;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Vehicle_cob;
        private System.Windows.Forms.RadioButton NoLimit_rad;
        private System.Windows.Forms.RadioButton Limit_rad;
        private System.Windows.Forms.Label Limit_label;
        private System.Windows.Forms.TextBox Time_text;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label UserName;
    }
}