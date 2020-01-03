using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDI_Test.UI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Graphics g;
        Pen p;
        Rectangle r;

        private void Form2_Load(object sender, EventArgs e)
        {
            r = new Rectangle(250, 50, 200, 200);

            Test();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            Test();
        }

        void Test()
        {         
            int a = int.Parse(numericUpDown1.Value.ToString());
            int b = int.Parse(numericUpDown2.Value.ToString());

            g = this.CreateGraphics();   //创建一个画板

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
         
            p = new Pen(Color.Blue, 2);            

            g.DrawRectangle(p, r);             //在画板上画矩形

            g.DrawArc(p, r, a, b);         //绘制圆弧
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
