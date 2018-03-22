using Scheduler.Files;
using System;
using System.IO;
using System.Windows.Forms;

namespace SchedulerUI
{
    public partial class DataVIewer : Form
    {
        SCHElement element;

        public DataVIewer(SCHElement element)
        {
            InitializeComponent();
            fastColoredTextBox1.Text = "";
            this.element = element;

            if (element.Data.ActionType == ActionType.CSharpCode)
            {
                var refs = element.Data.GetCSharpRefs(new FileInfo(Application.ExecutablePath).DirectoryName + "Lib\\");
                if (refs != null && refs.Length != 0)
                {
                    fastColoredTextBox1.Text =  "//==REFFERENES==\n";
                    fastColoredTextBox1.Text += "//" + string.Join("\n//", refs);
                    fastColoredTextBox1.Text += "\n\n";
                }
                fastColoredTextBox1.Text += element.Data.GetCSharpCode();
                button_output.Visible = true;
            }
            else
            {
                button_output.Visible = false;
                fastColoredTextBox1.Text = element.Data.GetCMDScript();
            }
            fastColoredTextBox1.ReadOnly = true;

            textBoxWidth = Width - fastColoredTextBox1.Width - fastColoredTextBox1.Left + 10;
            textBoxHeight = Height - fastColoredTextBox1.Height - fastColoredTextBox1.Top + button_close.Height - 30;
            buttonBotOffset = button_close.Height * 2 + 10;
            buttonLeftOffset = button_close.Width + 20;
            buttonOutTopOffset = button_close.Height * 2 + 10;
            buttonOutLeftOffset = buttonLeftOffset + 5 + button_output.Width;
            DataVIewer_SizeChanged(null, null);
        }

        private int textBoxWidth;
        private int textBoxHeight;
        private int buttonBotOffset;
        private int buttonLeftOffset;
        private int buttonOutLeftOffset;
        private int buttonOutTopOffset;

        private void button_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DataVIewer_SizeChanged(object sender, EventArgs e)
        {
            fastColoredTextBox1.Width = Width - textBoxWidth;
            fastColoredTextBox1.Height = Height - textBoxHeight;
            button_close.Top = Height - buttonBotOffset;
            button_output.Top = Height - buttonOutTopOffset;
            button_close.Left = Width - buttonLeftOffset;
            button_output.Left = Width - buttonOutLeftOffset;
        }

        private void button_output_Click(object sender, EventArgs e)
        {
            DialogResult DialogResult = saveFileDialog1.ShowDialog();
            if(DialogResult == DialogResult.OK)
            {
                File.WriteAllBytes(saveFileDialog1.FileName, element.Data.GetCSharpPrecompiledBytes());
            }
        }
    }
}
