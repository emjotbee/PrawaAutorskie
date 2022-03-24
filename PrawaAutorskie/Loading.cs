using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrawaAutorskie
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            BackColor = Color.White;
            TransparencyKey = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            progressBar1.Style = ProgressBarStyle.Marquee;
        }
    }
}
