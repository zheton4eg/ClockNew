using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockNew
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            labelTime.BackColor = Color.AliceBlue;

            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, 30);
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
            SetVisibility(false);
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
            SetVisibility(true);
            
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
    }
}
