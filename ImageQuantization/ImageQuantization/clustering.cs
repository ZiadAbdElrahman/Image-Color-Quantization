using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class clustering : Form
    {
        public clustering()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix1;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix1 = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix1, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix1).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix1).ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            int k = int.Parse(textBox1.Text) ;
            ImageMatrix1= clustring.clustr_image(ImageMatrix1, k);
            ImageOperations.DisplayImage(ImageMatrix1, pictureBox2);
        }

        private void Gauss_Smoothing_Load(object sender, EventArgs e)
        {

        }

        private void nudMaskSize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}