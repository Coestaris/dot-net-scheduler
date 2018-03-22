using LibCfg;
using Program;
using Scheduler;
using Scheduler.Files;
using SchedulerUI.Properties;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SchedulerUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string MainFileName = "main.sch";
        private string LibDirPath = new FileInfo(Application.ExecutablePath).DirectoryName + "\\";
        private string UnreadFileName = ".unread";

        private void CreateUnreadFile()
        {
            File.Create(UnreadFileName).Close();
        }

        private void SetAsRead()
        {
            if(File.Exists(UnreadFileName))
                File.Delete(UnreadFileName);
        }

        private bool IfUnread()
        {
            return File.Exists(UnreadFileName);
        }

        private void SelectUnactive()
        {
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = Resources.unactive;
        }

        private void SelectActive()
        {
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = Resources.active;
        }

        private SchedulerCore SchedulerCore;
        private Logger logger;
        
        private void UIInfo(SCHElement element)
        {
            groupBox_info.Enabled = true;
            label_m_name.Text = string.Format("Name: {0}", element.Metadata.Name);
            label_m_descr.Text = string.Format("Description: {0}", element.Metadata.Description);
            label_m_autor.Text = string.Format("Autor: {0}", element.Metadata.Autor);
            label_m_crdate.Text = string.Format("CreationDate: {0}", element.Metadata.CreationDate.ToString());

            label_d_acttype.Text = string.Format("Action Type: {0}", element.Data.ActionType);
            label_d_comdtype.Text = string.Format("Command Type: {0}", element.Data.CommandType);
            if (element.Data.CommandType == CommandType.OneTime)
            {
                label_d_reptype.Visible = false;
                tabControl_d.SelectedIndex = 0;
                label_d_onetimedate.Text = string.Format("One Time Date: {0}", element.Data.OneTimeDate);

            } else
            {
                label_d_reptype.Visible = true;
                label_d_reptype.Text = string.Format("Repeatable Type: {0}", element.Data.RepeatableType);
                if(element.Data.RepeatableType == RepeatableType.Monotonous)
                {
                    label_d_rm_startTime.Text = string.Format("Start time: {0}", element.Data.MonotonousStartTime);
                    label_d_rm_period.Text = string.Format("Period: {0}", element.Data.MonotonousPeriod);
                    label_d_rm_count.Text = string.Format("Count: {0}", element.Data.MonotonousRepeatCount);
                    tabControl_d.SelectedIndex = 1;
                } else
                {
                    listBox_d_rs.Items.Clear();
                    listBox_d_rs.Items.AddRange(element.Data.SpecificDates.Select(p => p.ToString()).ToArray());
                    tabControl_d.SelectedIndex = 2;
                }
            }
        }

        private void UpdateUI()
        {
            if(IfUnread())
            {
                SelectActive();
            }
            else
            {
                SelectUnactive();
            }

            logger.I("UpdateUI() call");

            listBox_main.Items.Clear();
            listBox_main.Items.AddRange(SchedulerCore.File.Elements.Select(
                p => p.Metadata.Name + " ---- " + p.Metadata.Autor).ToArray());

            if (listBox_main.Items.Count != 0)
                listBox_main.SelectedIndex = 0;
            else
            {
                label_m_name.Text = string.Format("Name: {0}", "-");
                label_m_descr.Text = string.Format("Description: {0}", "-");
                label_m_autor.Text = string.Format("Autor: {0}", "-");
                label_m_crdate.Text = string.Format("CreationDate: {0}", "-");
                label_d_acttype.Text = string.Format("Action Type: {0}", "-");
                label_d_comdtype.Text = string.Format("Command Type: {0}", "-");
                label_d_reptype.Text = string.Format("Repeatable Type: {0}", "-");
                label_d_rm_startTime.Text = string.Format("Start time: {0}", "-");
                label_d_rm_period.Text = string.Format("Period: {0}", "-");
                label_d_rm_count.Text = string.Format("Count: {0}", "-");
                listBox_d_rs.Items.Clear();
                groupBox_info.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon.Text = Text;
            notifyIcon.Icon = Icon;
            notifyIcon.Visible = false;

            logger = new Logger("scheduler", "schedulerUI", true);
            logger.V("Starting application...");

            SchedulerCoreParameters parameters = new SchedulerCoreParameters()
            {
                DataChangedAction = ListChanged,
                ElapsedAction = Elapsed,
                FileName = MainFileName,
                Interval = 1000,
                LibDir = SCHEnvironment.LibDir,
                Logger = logger,
                OnErrorAction = OnCoreError
            };

            SchedulerCore = new SchedulerCore(parameters);

            logger.I("SchedulerCore item has been created");
            label_lastElapsed.Text = string.Format("Last updated: {0}", DateTime.Now);
            UpdateUI();
        }

        private void OnCoreError(Exception inner, string message)
        {
            CreateUnreadFile();
            SelectActive();
            logger.E(message);
            logger.E(inner.Message);
            logger.E(inner.StackTrace);
        }

        private delegate void ElapsedDelegate();

        private void ListChanged()
        {
            if (InvokeRequired)
            {
                Invoke(new ElapsedDelegate(ListChanged));
            }
            else
            {
                logger.I("ListChanged() has been invoked");
                UpdateUI();
            }
        }

        private void Elapsed()
        {
            if (InvokeRequired)
            {
                Invoke(new ElapsedDelegate(Elapsed));
            }
            else
            {
                label_lastElapsed.Text = string.Format("Last updated: {0}", SchedulerCore.LastElapsed);
                if(SchedulerCore.File.Elements != null)
                    notifyIcon.Text = string.Format("{0}. Planned: {1}", Text, SchedulerCore.File.Elements.Count);
            }
        }

        private void button_hide_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(500);
            Hide();
            ShowInTaskbar = false;
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            UpdateUI();

            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            notifyIcon.Visible = false;
        }

        private void listBox_main_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_main.SelectedIndex != -1)
            {
                UIInfo(SchedulerCore.File.Elements[listBox_main.SelectedIndex]);
            }
        }

        private void button_d_viewActionData_Click(object sender, EventArgs e)
        {
            if (listBox_main.SelectedIndex != -1)
            {
                new DataVIewer(SchedulerCore.File.Elements[listBox_main.SelectedIndex])
                    .Show();
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
                Hide();
                ShowInTaskbar = false;
            }

            else if (FormWindowState.Normal == WindowState)
            {
                notifyIcon.Visible = false;
            }
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            if (listBox_main.SelectedIndex != -1)
            {
                SchedulerCore.File.Elements.RemoveAt(listBox_main.SelectedIndex);
                SchedulerCore.File.SaveToFile(SchedulerCore.Parameters.FileName);
                UpdateUI();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SchedulerCore.Close();
            Environment.Exit(0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(IfUnread())
            {
                SetAsRead();
                SelectUnactive();
            }
            new LogViewer(logger.FileName).Show();
        }
    }
}
