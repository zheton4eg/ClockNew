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
    public partial class btnHideControls : Form
    {
        public btnHideControls()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
            if (cbShowDate.Checked)
            {
                labelTime.Text += "\n";
                labelTime.Text += DateTime.Now.ToString("dd.MM.yyyy");
            }
        }

        private void buttonHideControls_Click(object sender, EventArgs e)
        {
            cbShowDate.Visible = false;
            buttonHideControls.Visible = false;
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            labelTime.BackColor = Color.AliceBlue;
            //this.ShowInTaskbar = false;
        }

        private void btnHideControls_DoubleClick(object sender, EventArgs e)
        {

        }

        private void labelTime_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Вы два раза щёлкнули мышью по времени, и теперь Вы управляете временем",
            "Info",
            MessageBoxButtons.OK,
                MessageBoxIcon.Information
                );
        }
    }
}
