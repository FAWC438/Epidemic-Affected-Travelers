namespace COVID_19疫情环境下低风险旅行模拟系统的设计
{
    partial class ShowPath_win
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TimeError_label = new System.Windows.Forms.Label();
            this.ChangedLimitTimeInput = new System.Windows.Forms.TextBox();
            this.Accept = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Path_label = new System.Windows.Forms.Label();
            this.Time_label = new System.Windows.Forms.Label();
            this.Risk_label = new System.Windows.Forms.Label();
            this.State_label = new System.Windows.Forms.Label();
            this.TimePass_label = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.UserId_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(243, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "推荐路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(229, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "路径总用时：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(229, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "路径总风险：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(215, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "当前旅客状态：";
            // 
            // TimeError_label
            // 
            this.TimeError_label.AutoSize = true;
            this.TimeError_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TimeError_label.ForeColor = System.Drawing.Color.Red;
            this.TimeError_label.Location = new System.Drawing.Point(109, 216);
            this.TimeError_label.Name = "TimeError_label";
            this.TimeError_label.Size = new System.Drawing.Size(555, 19);
            this.TimeError_label.TabIndex = 4;
            this.TimeError_label.Text = "您的限制时间过低，请在下方修改限制时间或删除该用户信息，最小限制时间必须不小于";
            this.TimeError_label.Visible = false;
            // 
            // ChangedLimitTimeInput
            // 
            this.ChangedLimitTimeInput.Location = new System.Drawing.Point(252, 238);
            this.ChangedLimitTimeInput.Name = "ChangedLimitTimeInput";
            this.ChangedLimitTimeInput.Size = new System.Drawing.Size(264, 21);
            this.ChangedLimitTimeInput.TabIndex = 5;
            this.ChangedLimitTimeInput.Visible = false;
            this.ChangedLimitTimeInput.TextChanged += new System.EventHandler(this.ChangedLimitTimeInput_TextChanged);
            // 
            // Accept
            // 
            this.Accept.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Accept.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Accept.Location = new System.Drawing.Point(161, 278);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(161, 71);
            this.Accept.TabIndex = 6;
            this.Accept.Text = "确定";
            this.Accept.UseVisualStyleBackColor = true;
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cancel.Location = new System.Drawing.Point(446, 278);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(161, 71);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "取消并删除该用户信息";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Path_label
            // 
            this.Path_label.AutoSize = true;
            this.Path_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Path_label.Location = new System.Drawing.Point(328, 42);
            this.Path_label.Name = "Path_label";
            this.Path_label.Size = new System.Drawing.Size(23, 20);
            this.Path_label.TabIndex = 8;
            this.Path_label.Text = "无";
            // 
            // Time_label
            // 
            this.Time_label.AutoSize = true;
            this.Time_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Time_label.Location = new System.Drawing.Point(328, 75);
            this.Time_label.Name = "Time_label";
            this.Time_label.Size = new System.Drawing.Size(23, 20);
            this.Time_label.TabIndex = 9;
            this.Time_label.Text = "无";
            // 
            // Risk_label
            // 
            this.Risk_label.AutoSize = true;
            this.Risk_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Risk_label.Location = new System.Drawing.Point(328, 110);
            this.Risk_label.Name = "Risk_label";
            this.Risk_label.Size = new System.Drawing.Size(23, 20);
            this.Risk_label.TabIndex = 10;
            this.Risk_label.Text = "无";
            // 
            // State_label
            // 
            this.State_label.AutoSize = true;
            this.State_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.State_label.Location = new System.Drawing.Point(328, 145);
            this.State_label.Name = "State_label";
            this.State_label.Size = new System.Drawing.Size(23, 20);
            this.State_label.TabIndex = 11;
            this.State_label.Text = "无";
            // 
            // TimePass_label
            // 
            this.TimePass_label.AutoSize = true;
            this.TimePass_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TimePass_label.Location = new System.Drawing.Point(328, 183);
            this.TimePass_label.Name = "TimePass_label";
            this.TimePass_label.Size = new System.Drawing.Size(23, 20);
            this.TimePass_label.TabIndex = 12;
            this.TimePass_label.Text = "无";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(229, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "已旅行时间：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(256, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "用户ID：";
            // 
            // UserId_label
            // 
            this.UserId_label.AutoSize = true;
            this.UserId_label.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UserId_label.Location = new System.Drawing.Point(328, 12);
            this.UserId_label.Name = "UserId_label";
            this.UserId_label.Size = new System.Drawing.Size(23, 20);
            this.UserId_label.TabIndex = 15;
            this.UserId_label.Text = "无";
            // 
            // ShowPath_win
            // 
            this.AcceptButton = this.Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(758, 361);
            this.Controls.Add(this.UserId_label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TimePass_label);
            this.Controls.Add(this.State_label);
            this.Controls.Add(this.Risk_label);
            this.Controls.Add(this.Time_label);
            this.Controls.Add(this.Path_label);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.ChangedLimitTimeInput);
            this.Controls.Add(this.TimeError_label);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ShowPath_win";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "推荐路径";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label TimeError_label;
        private System.Windows.Forms.TextBox ChangedLimitTimeInput;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label Path_label;
        private System.Windows.Forms.Label Time_label;
        private System.Windows.Forms.Label Risk_label;
        private System.Windows.Forms.Label State_label;
        private System.Windows.Forms.Label TimePass_label;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label UserId_label;
    }
}