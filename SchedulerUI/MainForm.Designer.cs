namespace SchedulerUI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listBox_main = new System.Windows.Forms.ListBox();
            this.button_remove = new System.Windows.Forms.Button();
            this.groupBox_info = new System.Windows.Forms.GroupBox();
            this.groupBox_data = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl_d = new System.Windows.Forms.TabControl();
            this.tabPage_oneTime = new System.Windows.Forms.TabPage();
            this.label_d_onetimedate = new System.Windows.Forms.Label();
            this.tabPage_rm = new System.Windows.Forms.TabPage();
            this.label_d_rm_count = new System.Windows.Forms.Label();
            this.label_d_rm_period = new System.Windows.Forms.Label();
            this.label_d_rm_startTime = new System.Windows.Forms.Label();
            this.tabPage_rs = new System.Windows.Forms.TabPage();
            this.listBox_d_rs = new System.Windows.Forms.ListBox();
            this.button_d_viewActionData = new System.Windows.Forms.Button();
            this.label_d_reptype = new System.Windows.Forms.Label();
            this.label_d_comdtype = new System.Windows.Forms.Label();
            this.label_d_acttype = new System.Windows.Forms.Label();
            this.groupBox_metadata = new System.Windows.Forms.GroupBox();
            this.label_m_crdate = new System.Windows.Forms.Label();
            this.label_m_descr = new System.Windows.Forms.Label();
            this.label_m_autor = new System.Windows.Forms.Label();
            this.label_m_name = new System.Windows.Forms.Label();
            this.button_hide = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label_lastElapsed = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox_info.SuspendLayout();
            this.groupBox_data.SuspendLayout();
            this.tabControl_d.SuspendLayout();
            this.tabPage_oneTime.SuspendLayout();
            this.tabPage_rm.SuspendLayout();
            this.tabPage_rs.SuspendLayout();
            this.groupBox_metadata.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox_main
            // 
            this.listBox_main.FormattingEnabled = true;
            this.listBox_main.Location = new System.Drawing.Point(12, 12);
            this.listBox_main.Name = "listBox_main";
            this.listBox_main.Size = new System.Drawing.Size(211, 329);
            this.listBox_main.TabIndex = 0;
            this.listBox_main.SelectedIndexChanged += new System.EventHandler(this.listBox_main_SelectedIndexChanged);
            // 
            // button_remove
            // 
            this.button_remove.Location = new System.Drawing.Point(12, 356);
            this.button_remove.Name = "button_remove";
            this.button_remove.Size = new System.Drawing.Size(75, 36);
            this.button_remove.TabIndex = 1;
            this.button_remove.Text = "Remove";
            this.button_remove.UseVisualStyleBackColor = true;
            this.button_remove.Click += new System.EventHandler(this.button_remove_Click);
            // 
            // groupBox_info
            // 
            this.groupBox_info.Controls.Add(this.groupBox_data);
            this.groupBox_info.Controls.Add(this.groupBox_metadata);
            this.groupBox_info.Location = new System.Drawing.Point(229, 12);
            this.groupBox_info.Name = "groupBox_info";
            this.groupBox_info.Size = new System.Drawing.Size(279, 328);
            this.groupBox_info.TabIndex = 2;
            this.groupBox_info.TabStop = false;
            this.groupBox_info.Text = "Item Info";
            // 
            // groupBox_data
            // 
            this.groupBox_data.Controls.Add(this.panel1);
            this.groupBox_data.Controls.Add(this.tabControl_d);
            this.groupBox_data.Controls.Add(this.button_d_viewActionData);
            this.groupBox_data.Controls.Add(this.label_d_reptype);
            this.groupBox_data.Controls.Add(this.label_d_comdtype);
            this.groupBox_data.Controls.Add(this.label_d_acttype);
            this.groupBox_data.Location = new System.Drawing.Point(6, 131);
            this.groupBox_data.Name = "groupBox_data";
            this.groupBox_data.Size = new System.Drawing.Size(267, 191);
            this.groupBox_data.TabIndex = 5;
            this.groupBox_data.TabStop = false;
            this.groupBox_data.Text = "Item Data";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(262, 25);
            this.panel1.TabIndex = 5;
            // 
            // tabControl_d
            // 
            this.tabControl_d.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl_d.Controls.Add(this.tabPage_oneTime);
            this.tabControl_d.Controls.Add(this.tabPage_rm);
            this.tabControl_d.Controls.Add(this.tabPage_rs);
            this.tabControl_d.Location = new System.Drawing.Point(6, 85);
            this.tabControl_d.Name = "tabControl_d";
            this.tabControl_d.SelectedIndex = 0;
            this.tabControl_d.Size = new System.Drawing.Size(252, 100);
            this.tabControl_d.TabIndex = 4;
            // 
            // tabPage_oneTime
            // 
            this.tabPage_oneTime.Controls.Add(this.label_d_onetimedate);
            this.tabPage_oneTime.Location = new System.Drawing.Point(4, 25);
            this.tabPage_oneTime.Name = "tabPage_oneTime";
            this.tabPage_oneTime.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_oneTime.Size = new System.Drawing.Size(244, 71);
            this.tabPage_oneTime.TabIndex = 0;
            this.tabPage_oneTime.Text = "One Time";
            this.tabPage_oneTime.UseVisualStyleBackColor = true;
            // 
            // label_d_onetimedate
            // 
            this.label_d_onetimedate.AutoSize = true;
            this.label_d_onetimedate.Location = new System.Drawing.Point(16, 28);
            this.label_d_onetimedate.Name = "label_d_onetimedate";
            this.label_d_onetimedate.Size = new System.Drawing.Size(83, 13);
            this.label_d_onetimedate.TabIndex = 0;
            this.label_d_onetimedate.Text = "One TIme Date:";
            // 
            // tabPage_rm
            // 
            this.tabPage_rm.Controls.Add(this.label_d_rm_count);
            this.tabPage_rm.Controls.Add(this.label_d_rm_period);
            this.tabPage_rm.Controls.Add(this.label_d_rm_startTime);
            this.tabPage_rm.Location = new System.Drawing.Point(4, 25);
            this.tabPage_rm.Name = "tabPage_rm";
            this.tabPage_rm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_rm.Size = new System.Drawing.Size(244, 71);
            this.tabPage_rm.TabIndex = 1;
            this.tabPage_rm.Text = "RM";
            this.tabPage_rm.UseVisualStyleBackColor = true;
            // 
            // label_d_rm_count
            // 
            this.label_d_rm_count.AutoSize = true;
            this.label_d_rm_count.Location = new System.Drawing.Point(12, 56);
            this.label_d_rm_count.Name = "label_d_rm_count";
            this.label_d_rm_count.Size = new System.Drawing.Size(41, 13);
            this.label_d_rm_count.TabIndex = 2;
            this.label_d_rm_count.Text = "Count: ";
            // 
            // label_d_rm_period
            // 
            this.label_d_rm_period.AutoSize = true;
            this.label_d_rm_period.Location = new System.Drawing.Point(12, 31);
            this.label_d_rm_period.Name = "label_d_rm_period";
            this.label_d_rm_period.Size = new System.Drawing.Size(43, 13);
            this.label_d_rm_period.TabIndex = 1;
            this.label_d_rm_period.Text = "Period: ";
            // 
            // label_d_rm_startTime
            // 
            this.label_d_rm_startTime.AutoSize = true;
            this.label_d_rm_startTime.Location = new System.Drawing.Point(12, 6);
            this.label_d_rm_startTime.Name = "label_d_rm_startTime";
            this.label_d_rm_startTime.Size = new System.Drawing.Size(61, 13);
            this.label_d_rm_startTime.TabIndex = 0;
            this.label_d_rm_startTime.Text = "Start Time: ";
            // 
            // tabPage_rs
            // 
            this.tabPage_rs.Controls.Add(this.listBox_d_rs);
            this.tabPage_rs.Location = new System.Drawing.Point(4, 25);
            this.tabPage_rs.Name = "tabPage_rs";
            this.tabPage_rs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_rs.Size = new System.Drawing.Size(244, 71);
            this.tabPage_rs.TabIndex = 2;
            this.tabPage_rs.Text = "RS";
            this.tabPage_rs.UseVisualStyleBackColor = true;
            // 
            // listBox_d_rs
            // 
            this.listBox_d_rs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_d_rs.FormattingEnabled = true;
            this.listBox_d_rs.Location = new System.Drawing.Point(3, 3);
            this.listBox_d_rs.Name = "listBox_d_rs";
            this.listBox_d_rs.Size = new System.Drawing.Size(238, 65);
            this.listBox_d_rs.TabIndex = 0;
            // 
            // button_d_viewActionData
            // 
            this.button_d_viewActionData.Location = new System.Drawing.Point(181, 20);
            this.button_d_viewActionData.Name = "button_d_viewActionData";
            this.button_d_viewActionData.Size = new System.Drawing.Size(77, 38);
            this.button_d_viewActionData.TabIndex = 4;
            this.button_d_viewActionData.Text = "View Action Data";
            this.button_d_viewActionData.UseVisualStyleBackColor = true;
            this.button_d_viewActionData.Click += new System.EventHandler(this.button_d_viewActionData_Click);
            // 
            // label_d_reptype
            // 
            this.label_d_reptype.AutoSize = true;
            this.label_d_reptype.Location = new System.Drawing.Point(15, 65);
            this.label_d_reptype.Name = "label_d_reptype";
            this.label_d_reptype.Size = new System.Drawing.Size(116, 13);
            this.label_d_reptype.TabIndex = 2;
            this.label_d_reptype.Text = "RepeatableType (opt): ";
            // 
            // label_d_comdtype
            // 
            this.label_d_comdtype.AutoSize = true;
            this.label_d_comdtype.Location = new System.Drawing.Point(15, 45);
            this.label_d_comdtype.Name = "label_d_comdtype";
            this.label_d_comdtype.Size = new System.Drawing.Size(81, 13);
            this.label_d_comdtype.TabIndex = 1;
            this.label_d_comdtype.Text = "Command Type";
            // 
            // label_d_acttype
            // 
            this.label_d_acttype.AutoSize = true;
            this.label_d_acttype.Location = new System.Drawing.Point(15, 25);
            this.label_d_acttype.Name = "label_d_acttype";
            this.label_d_acttype.Size = new System.Drawing.Size(64, 13);
            this.label_d_acttype.TabIndex = 0;
            this.label_d_acttype.Text = "Action Type";
            // 
            // groupBox_metadata
            // 
            this.groupBox_metadata.Controls.Add(this.label_m_crdate);
            this.groupBox_metadata.Controls.Add(this.label_m_descr);
            this.groupBox_metadata.Controls.Add(this.label_m_autor);
            this.groupBox_metadata.Controls.Add(this.label_m_name);
            this.groupBox_metadata.Location = new System.Drawing.Point(6, 19);
            this.groupBox_metadata.Name = "groupBox_metadata";
            this.groupBox_metadata.Size = new System.Drawing.Size(267, 106);
            this.groupBox_metadata.TabIndex = 4;
            this.groupBox_metadata.TabStop = false;
            this.groupBox_metadata.Text = "Item Metadata";
            // 
            // label_m_crdate
            // 
            this.label_m_crdate.AutoSize = true;
            this.label_m_crdate.Location = new System.Drawing.Point(15, 85);
            this.label_m_crdate.Name = "label_m_crdate";
            this.label_m_crdate.Size = new System.Drawing.Size(75, 13);
            this.label_m_crdate.TabIndex = 3;
            this.label_m_crdate.Text = "Creation Date:";
            // 
            // label_m_descr
            // 
            this.label_m_descr.AutoSize = true;
            this.label_m_descr.Location = new System.Drawing.Point(15, 65);
            this.label_m_descr.Name = "label_m_descr";
            this.label_m_descr.Size = new System.Drawing.Size(41, 13);
            this.label_m_descr.TabIndex = 2;
            this.label_m_descr.Text = "Descr: ";
            // 
            // label_m_autor
            // 
            this.label_m_autor.AutoSize = true;
            this.label_m_autor.Location = new System.Drawing.Point(15, 45);
            this.label_m_autor.Name = "label_m_autor";
            this.label_m_autor.Size = new System.Drawing.Size(38, 13);
            this.label_m_autor.TabIndex = 1;
            this.label_m_autor.Text = "Autor: ";
            // 
            // label_m_name
            // 
            this.label_m_name.AutoSize = true;
            this.label_m_name.Location = new System.Drawing.Point(15, 25);
            this.label_m_name.Name = "label_m_name";
            this.label_m_name.Size = new System.Drawing.Size(41, 13);
            this.label_m_name.TabIndex = 0;
            this.label_m_name.Text = "Name: ";
            // 
            // button_hide
            // 
            this.button_hide.Location = new System.Drawing.Point(381, 356);
            this.button_hide.Name = "button_hide";
            this.button_hide.Size = new System.Drawing.Size(75, 36);
            this.button_hide.TabIndex = 3;
            this.button_hide.Text = "Hide";
            this.button_hide.UseVisualStyleBackColor = true;
            this.button_hide.Click += new System.EventHandler(this.button_hide_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Application has been minimized. Press it to open";
            this.notifyIcon.BalloonTipTitle = "Scheduler UI";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // label_lastElapsed
            // 
            this.label_lastElapsed.AutoSize = true;
            this.label_lastElapsed.Location = new System.Drawing.Point(93, 382);
            this.label_lastElapsed.Name = "label_lastElapsed";
            this.label_lastElapsed.Size = new System.Drawing.Size(89, 13);
            this.label_lastElapsed.TabIndex = 4;
            this.label_lastElapsed.Text = "label_lastElapsed";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SchedulerUI.Properties.Resources.active;
            this.pictureBox1.Location = new System.Drawing.Point(462, 353);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 404);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_lastElapsed);
            this.Controls.Add(this.button_hide);
            this.Controls.Add(this.groupBox_info);
            this.Controls.Add(this.button_remove);
            this.Controls.Add(this.listBox_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GovnoCode inc. Scheduler";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.groupBox_info.ResumeLayout(false);
            this.groupBox_data.ResumeLayout(false);
            this.groupBox_data.PerformLayout();
            this.tabControl_d.ResumeLayout(false);
            this.tabPage_oneTime.ResumeLayout(false);
            this.tabPage_oneTime.PerformLayout();
            this.tabPage_rm.ResumeLayout(false);
            this.tabPage_rm.PerformLayout();
            this.tabPage_rs.ResumeLayout(false);
            this.groupBox_metadata.ResumeLayout(false);
            this.groupBox_metadata.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_main;
        private System.Windows.Forms.Button button_remove;
        private System.Windows.Forms.GroupBox groupBox_info;
        private System.Windows.Forms.Label label_m_autor;
        private System.Windows.Forms.Label label_m_name;
        private System.Windows.Forms.Button button_hide;
        private System.Windows.Forms.Label label_m_crdate;
        private System.Windows.Forms.Label label_m_descr;
        private System.Windows.Forms.GroupBox groupBox_data;
        private System.Windows.Forms.Button button_d_viewActionData;
        private System.Windows.Forms.Label label_d_reptype;
        private System.Windows.Forms.Label label_d_comdtype;
        private System.Windows.Forms.Label label_d_acttype;
        private System.Windows.Forms.GroupBox groupBox_metadata;
        private System.Windows.Forms.TabControl tabControl_d;
        private System.Windows.Forms.TabPage tabPage_oneTime;
        private System.Windows.Forms.Label label_d_onetimedate;
        private System.Windows.Forms.TabPage tabPage_rm;
        private System.Windows.Forms.Label label_d_rm_count;
        private System.Windows.Forms.Label label_d_rm_period;
        private System.Windows.Forms.Label label_d_rm_startTime;
        private System.Windows.Forms.TabPage tabPage_rs;
        private System.Windows.Forms.ListBox listBox_d_rs;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_lastElapsed;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

