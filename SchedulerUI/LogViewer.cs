using FastColoredTextBoxNS;
using Program;
using Scheduler;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SchedulerUI
{
    public partial class LogViewer : Form
    {
        private bool Inited;
        private List<LogItem> Items;
        private TextStyle dateStyle = new TextStyle(Brushes.Gray, null, FontStyle.Italic);
        private TextStyle vStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        private TextStyle iStyle = new TextStyle(Brushes.Green, null, FontStyle.Bold);
        private TextStyle eStyle = new TextStyle(Brushes.Red, null, FontStyle.Bold);
        private TextStyle dStyle = new TextStyle(Brushes.Green, null, FontStyle.Bold);
        private int panelWidth;
        private int textBoxWidth;
        private int textBoxHeight;
        private int checkedListBoxWidth;
        private int checkedListBoxHeight;
        private int buttonWidth;
        private int buttonHeight;


        public string FileName;

        public LogViewer(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
        }

        private void UpdateCheckbox()
        {
            if (Items != null)
            {
                var v = Items.FindAll(p => p.Tag == 'V').Count;
                var i = Items.FindAll(p => p.Tag == 'I').Count;
                var d = Items.FindAll(p => p.Tag == 'D').Count;
                var e = Items.FindAll(p => p.Tag == 'E').Count;

                checkedListBox1.Items[0] = string.Format("Verbose ({0} {1})", v, v == 1 ? "item" : "items");
                checkedListBox1.Items[1] = string.Format("Info ({0} {1})", i, i == 1 ? "item" : "items");
                checkedListBox1.Items[2] = string.Format("Error ({0} {1})", e, e == 1 ? "item" : "items");
                checkedListBox1.Items[3] = string.Format("Debug ({0} {1})", d, d == 1 ? "item" : "items");
            }
        }

        private void DisplayList(List<LogItem> Items)
        {
            fastColoredTextBox1.ReadOnly = false;
            fastColoredTextBox1.Text = string.Join("\n", Items.Select(p => $"{p.DateTime.ToString(Logger.DateFormat)}:{p.Tag}/[{p.TagName}]: {p.Message}"));
            fastColoredTextBox1.ReadOnly = true;
        }

        private void LogViewer_Load(object sender, EventArgs e)
        {
            Items = LogParser.Parse(FileName);

            checkedListBox1.SetItemChecked(0, true);
            checkedListBox1.SetItemChecked(1, true);
            checkedListBox1.SetItemChecked(2, true);
            checkedListBox1.SetItemChecked(3, true);

            DisplayList(Items);
            UpdateCheckbox();

            panelWidth = Width - panel1.Left;
            textBoxWidth = Width - fastColoredTextBox1.Width - fastColoredTextBox1.Left + 10;
            textBoxHeight = Height - fastColoredTextBox1.Height - fastColoredTextBox1.Top + 5;
            checkedListBoxWidth = Width - checkedListBox1.Left;
            checkedListBoxHeight = Height - checkedListBox1.Top;
            buttonWidth = Width - button1.Left;
            buttonHeight = Height - button1.Top;
            LogViewer_SizeChanged(null, null);

            dateTimePicker_from.Value = Items.OrderBy(p => p.DateTime).First().DateTime;
            dateTimePicker_to.Value = Items.OrderBy(p => p.DateTime).Last().DateTime;

            fastColoredTextBox1.GoEnd();

            Inited = true;
        }

        private void fastColoredTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(dateStyle);
            e.ChangedRange.SetStyle(dateStyle, Logger.DateRegex, RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(vStyle, Logger.VItemRegex, RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(iStyle, Logger.IItemRegex, RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(eStyle, Logger.EItemRegex, RegexOptions.IgnoreCase);
            e.ChangedRange.SetStyle(dStyle, Logger.DItemRegex, RegexOptions.IgnoreCase);
        }

        private void LogViewer_SizeChanged(object sender, EventArgs e)
        {
            fastColoredTextBox1.Width = Width - textBoxWidth;
            fastColoredTextBox1.Height = Height - textBoxHeight;
            checkedListBox1.Left = Width - checkedListBoxWidth;
            button1.Left = Width - buttonWidth;
            panel1.Left = Width - panelWidth;
            button1.Top = Height - buttonHeight;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (Inited)
            {
                var v = e.Index == 0 ? e.NewValue == CheckState.Checked : checkedListBox1.GetItemChecked(0);
                var i = e.Index == 1 ? e.NewValue == CheckState.Checked : checkedListBox1.GetItemChecked(1);
                var er = e.Index == 2 ? e.NewValue == CheckState.Checked : checkedListBox1.GetItemChecked(2);
                var d = e.Index == 3 ? e.NewValue == CheckState.Checked : checkedListBox1.GetItemChecked(3);

                DisplayList(Items.FindAll(p =>
                {
                    if (v && p.Tag == 'V') return true;
                    if (i && p.Tag == 'I') return true;
                    if (er && p.Tag == 'E') return true;
                    if (d && p.Tag == 'D') return true;
                    return false;
                }));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateDateTime()
        {
            if (Inited)
            {
                DateTime from = dateTimePicker_from.Value;
                DateTime to = dateTimePicker_to.Value;
                if (from > to)
                {
                    DisplayList(Items.FindAll(p => p.DateTime > to && p.DateTime < from));
                }
                else
                {
                    DisplayList(Items.FindAll(p => p.DateTime > from && p.DateTime < to));
                }
            }
        }

        private void dateTimePicker_from_ValueChanged(object sender, EventArgs e)
        {
            UpdateDateTime();
        }

        private void dateTimePicker_to_ValueChanged(object sender, EventArgs e)
        {
            UpdateDateTime();
        }
    }
}
