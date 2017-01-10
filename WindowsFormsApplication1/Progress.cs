using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Progress : Form
    {
        public Progress()
        {
            InitializeComponent();
        }
        public void SetProgressValue(int value)
        {
            this.progressBar1.Value = value;
            this.progressLabel.Text = "Progress :" + value.ToString() + "%";
            if (value == this.progressBar1.Maximum)
            {
                this.Close();
            }
        }
        public void SetProgressMaximum(int value)
        {
            progressBar1.Maximum = value;
        }

    }

}
