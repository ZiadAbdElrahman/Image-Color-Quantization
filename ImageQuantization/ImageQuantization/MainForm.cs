using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gauss_Smoothing f = new Gauss_Smoothing();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clustering f = new clustering();
            f.ShowDialog();

        }
    }
}
