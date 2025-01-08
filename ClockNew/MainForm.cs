using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Diagnostics; 
namespace ClockNew
{
    public partial class MainForm : Form
    {
        ChooseFontForm fontDialog =null;
        public MainForm()
        {
            InitializeComponent();
            labelTime.BackColor = Color.AliceBlue;
            cmShowControls.Checked = true;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 30);
            cmShowConsole.Checked = true;
           LoadSettings();
            fontDialog=new ChooseFontForm();
        }
        void SetVisibility(bool visible)
        {
            cbShowDate.Visible = visible;
            cbShowWeekDay.Visible = visible;
            buttonHideControls.Visible = visible;
            this.TransparencyKey = visible? Color.Empty : this.BackColor;
            this.FormBorderStyle =visible? FormBorderStyle.FixedToolWindow: FormBorderStyle.None;
           
            this.ShowInTaskbar = false;
        }
        void SaveSettings()
        {
            StreamWriter sw = new StreamWriter("Settings.ini");
            sw.WriteLine($"{cmTopmost.Checked}");
            sw.WriteLine($"{cmShowControls.Checked}");
            sw.WriteLine($"{cmShowDate.Checked}");
            sw.WriteLine($"{cmShowWeekDay.Checked}");
            sw.WriteLine($"{cmShowConsole.Checked}");         
            sw.WriteLine($"{labelTime.BackColor.ToArgb()}");
            sw.WriteLine($"{labelTime.ForeColor.ToArgb()}");
            sw.WriteLine($"{fontDialog.Filename}");
            sw.WriteLine($"{labelTime.Font.Size}");
            sw.Close();
            Process.Start("notepad", "Settings.ini");
        }
        void LoadSettings()
        {
            Directory.SetCurrentDirectory("..\\..\\..\\Fonts");
            StreamReader sr = new StreamReader("Settings.ini");
            cmTopmost.Checked = bool.Parse(sr.ReadLine());
            cmShowControls.Checked = bool.Parse(sr.ReadLine());
            cmShowDate.Checked = bool.Parse(sr.ReadLine());
            cmShowWeekDay.Checked = bool.Parse(sr.ReadLine());
            cmShowConsole.Checked = bool.Parse(sr.ReadLine());
            labelTime.BackColor = Color.FromArgb(Convert.ToInt32(sr.ReadLine()));
            labelTime.ForeColor = Color.FromArgb(Convert.ToInt32(sr.ReadLine()));
            string font_name=sr.ReadLine();
            int font_size = (int)Convert.ToDouble(sr.ReadLine());
             sr.Close();
            fontDialog = new ChooseFontForm(font_name, font_size);
            labelTime.Font=fontDialog.Font;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
            if (cbShowDate.Checked)
            {
                labelTime.Text += "\n";
                labelTime.Text += DateTime.Now.ToString("dd.MM.yyyy");
            }
            if (cbShowWeekDay.Checked)
            {
                labelTime.Text += "\n";
                labelTime.Text += DateTime.Now.DayOfWeek;
            }
            
            notifyIcon.Text= labelTime.Text;          
        }

        private void buttonHideControls_Click(object sender, EventArgs e)
        {
            //cbShowDate.Visible = false;
            //buttonHideControls.Visible = false;
            //this.TransparencyKey = this.BackColor;
            //this.FormBorderStyle = FormBorderStyle.None;
            //labelTime.BackColor = Color.AliceBlue;
            //this.ShowInTaskbar = false;
            SetVisibility(cmShowControls.Checked=false);

            
        }

        private void btnHideControls_DoubleClick(object sender, EventArgs e)
        {

        }

        private void labelTime_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(this,
            //    "Вы два раза щёлкнули мышью по времени, и теперь Вы управляете временем",
            //"Info",
            //MessageBoxButtons.OK,
            //    MessageBoxIcon.Information
            //    );

            //cbShowDate.Visible = true;
            //buttonHideControls.Visible = true;
            //this.TransparencyKey = Color.Empty;
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //labelTime.BackColor = Color.AliceBlue;
            //this.ShowInTaskbar = true;
            //SetVisibility(true);
            SetVisibility(cmShowControls.Checked = true);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cmExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmTopmost_CheckedChanged(object sender, EventArgs e)
        {
this.TopMost=cmTopmost.Checked;
        }

        private void cmShowDate_CheckedChanged(object sender, EventArgs e)
        {
            cbShowDate.Checked=cmShowDate.Checked;
            
        }

        private void cbShowDate_CheckedChanged(object sender, EventArgs e)
        {
            cmShowDate.Checked = cbShowDate.Checked;
        }

        private void cmShowWeekDay_CheckedChanged(object sender, EventArgs e)
        {
            cbShowWeekDay.Checked=cmShowWeekDay.Checked;
        }

        private void cbShowWeekDay_CheckedChanged(object sender, EventArgs e)
        {
            cmShowWeekDay.Checked=cbShowWeekDay.Checked;
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (!this.TopMost)
            {
                this.TopMost = true;
                this.TopMost = false;
            }
        }

        private void cmShowControls_CheckedChanged(object sender, EventArgs e)
        {
SetVisibility(cmShowControls.Checked);
        }

        private void cmBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color=labelTime.BackColor;
            if(dialog.ShowDialog() == DialogResult.OK)
                labelTime.BackColor = dialog.Color;
        }

        private void cmForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = labelTime.ForeColor;
            if (dialog.ShowDialog() == DialogResult.OK)
                labelTime.ForeColor = dialog.Color;
        }

        private void cmChooseFont_Click(object sender, EventArgs e)
        {
           if( fontDialog.ShowDialog()==DialogResult.OK);
           labelTime.Font = fontDialog.Font;
        }

        private void cmShowConsole_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Checked)

                AllocConsole();
            else
                FreeConsole();
        }
            [DllImport("kernel32.dll")]
            public static extern bool AllocConsole();
            [DllImport("kernel32.dll")]
            public static extern bool FreeConsole();

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }
    }
}
