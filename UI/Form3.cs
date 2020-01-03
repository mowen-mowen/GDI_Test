using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDI_Test.UI
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        void Test_one()
        {
            //投影文字
            Graphics g = this.CreateGraphics();
            //设置文本输出质量
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Font newFont = new Font("Times New Roman", 48);

            Matrix matrix = new Matrix();

            //投射
            matrix.Shear(-1.5f, 0.0f);         //单个文字变倾斜
            //缩放
            matrix.Scale(1, 0.5f);
            //平移
            matrix.Translate(130, 88);

            //整体旋转
            //matrix.Rotate(30);


            //对绘图平面实施坐标变换
            g.Transform = matrix;

            SolidBrush grayBrush = new SolidBrush(Color.Black);
            SolidBrush colorBrush = new SolidBrush(Color.BlueViolet);
            string text = "MINGRISOFT";

            //绘制阴影
            g.DrawString(text, newFont, grayBrush, new PointF(30, 30));
            g.ResetTransform();

            //绘制前景
            g.DrawString(text, newFont, colorBrush, new PointF(30, 30));
        }

        void test_two()
        {
            //倒影文字
            Brush backBrush = Brushes.Gray;
            Brush foreBrush = Brushes.Black;
            Font font = new Font("幼圆", Convert.ToInt16(40), FontStyle.Regular);
            Graphics g = this.CreateGraphics();

            string text = "MINGRISOFT";

            SizeF size = g.MeasureString(text, font);
            int posX = (this.Width - Convert.ToInt16(size.Width)) / 2;
            int posY = (this.Height - Convert.ToInt16(size.Height)) / 2;
            g.TranslateTransform(posX, posY);

            int ascent = font.FontFamily.GetCellAscent(font.Style);
            int spacing = font.FontFamily.GetLineSpacing(font.Style);
            int lineHeight = System.Convert.ToInt16(font.GetHeight(g));
            int height = lineHeight * ascent / spacing;

            GraphicsState state = g.Save();

            g.ScaleTransform(1, -1.0F);
            g.DrawString(text, font, backBrush, 0, -height);
            g.Restore(state);
            g.DrawString(text, font, foreBrush, 0, -height);
        }

        void Test_three()
        {
            //使用图像填充文字线条
            TextureBrush brush = new TextureBrush(Image.FromFile(@"C:\Users\SPGZ\Desktop\常用文件\壁纸\1.jpg"));
            Graphics g = this.CreateGraphics();
            g.DrawString("MINGRISOFT", new Font("隶书", 60), brush, new PointF(0, 0));
        }


        void Test_four()
        {
            //旋转显示文字
            Graphics g = this.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i <= 360; i += 10)
            {
                //平移Graphics对象到窗体中心
                g.TranslateTransform(this.Width / 2, this.Height / 2);

                //设置Graphics对象的输出角度
                g.RotateTransform(i);

                //设置文字填充颜色
                Brush brush = Brushes.DarkViolet;

                //旋转显示文字
                g.DrawString("......MINGRISOFT", new Font("Lucida Console", 11f), brush, 0, 0);

                //恢复全局变换矩阵
                g.ResetTransform();
            }
        }

        void Test_five()
        {
            //印版立体文字
            int i = 0;
            Brush backBrush = Brushes.Black;
            Brush foreBrush = Brushes.Violet;

            Font font = new Font("Times New Roman", System.Convert.ToInt16(40), FontStyle.Regular);
            Graphics g = this.CreateGraphics();
            string text = "MINGRISOFT";

            SizeF size = g.MeasureString(text, font);
            Single posX = (this.Width - Convert.ToInt16(size.Width)) / 2;
            Single posY = (this.Height - Convert.ToInt16(size.Height)) / 3;


            while (i < 10)
            {
                g.DrawString(text, font, backBrush, posX - i, posY + i);
                i = i + 1;
            }

            g.DrawString(text, font, foreBrush, posX, posY);
        }

        #region 按钮

        private void button2_Click(object sender, EventArgs e)
        {
            Test_one();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test_two();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Test_three();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Test_four();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Test_five();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }
        #endregion
    }
}
