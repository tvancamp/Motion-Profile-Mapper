using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private float tension = 0.5F;

        private void Form1_Paint(object sender,
        System.Windows.Forms.PaintEventArgs e)
        {
            // Create a pen
            Pen bluePen = new Pen(Color.Blue, 1);
            // Create an array of points
            PointF pt1 = new PointF(40.0F, 50.0F);
            PointF pt2 = new PointF(50.0F, 75.0F);
            PointF pt3 = new PointF(100.0F, 115.0F);
            PointF pt4 = new PointF(200.0F, 180.0F);
            PointF pt5 = new PointF(200.0F, 90.0F);
            PointF[] ptsArray =
			{
				pt1, pt2, pt3, pt4, pt5
			};
            // Draw curve
            e.Graphics.DrawCurve(bluePen, ptsArray, tension);
            // Dispose of object
            bluePen.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tension = (float)Convert.ToDouble(textBox1.Text);
            Invalidate();

        }


    }
}
