using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDI_Test.UI
{
    public partial class Form4 : Form
    {
        private Graphics g;
        private Pen pen;
        private FontFamily myFimaly;
        private Font myFont;
        private int w;
        private int h;
        int x1, y1;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            timer_Second.Start();
        }

        private void timer_Second_Tick(object sender, EventArgs e)
        {
            pictureBox2.Invalidate();
        }

        void initClock(PaintEventArgs e)          //绘制表盘
        {
            g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            w = pictureBox2.Width;
            h = pictureBox2.Height;

            x1 = pictureBox2.Location.X;
            y1 = pictureBox2.Location.Y;


            /*
             一个小时刻度：360/12=30°
             一分钟刻度：360/60=6
             一秒钟 刻度：360/60=6
             */


            //画外圆圆
            g.FillEllipse(Brushes.Black, x1 + 4, y1 + 4, w - 8, w - 8);


            //画装饰性文字
            myFimaly = new FontFamily("Impact");    
            myFont = new Font(myFimaly, 30, FontStyle.Bold, GraphicsUnit.Pixel);
           


            //画内圈圆
            pen = new Pen(Color.White, 2);
            g.DrawEllipse(pen, x1 + 7, y1 + 7, w - 13, h - 13);  //内圆

            g.TranslateTransform(x1 + (w / 2), y1 + (h / 2));    //重新设置坐标原点
            g.FillEllipse(Brushes.White, -5, -5, 10, 10);

            for (int x = 0; x < 60; x++)  //小刻度
            {
                g.FillRectangle(Brushes.White, new Rectangle(-2, ((h - 8) / 2 - 2) * (-1), 3, 10));

                g.RotateTransform(6);   //偏移角度
            }

            for (int i = 12; i > 0; i--)   //大刻度
            {
                string myString = i.ToString();

                //绘制整点刻度
                g.FillRectangle(Brushes.White, new Rectangle(-3, ((h - 8) / 2 - 2) * (-1), 6, 20));

                //绘制数值
                g.DrawString(myString, myFont, Brushes.White, new PointF(myString.Length * (-6), (h - 8) / -2 + 26));

                //顺时针旋转度
                g.RotateTransform(-30);      //偏移角度

            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            initClock(e);

            g.ResetTransform();

            g = e.Graphics;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            g.TranslateTransform(w/2,h/2);   //重新设置坐标原点


            //获得系统时间值

            int second = DateTime.Now.Second;
            int minute = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;

            //绘制秒针
            pen = new Pen(Color.White,1);

            pen.EndCap = LineCap.ArrowAnchor;

            g.RotateTransform(6 * second);                               //秒针的旋转角度
             
            float y = (float)((-1) * (h / 2.75));                        //秒针的长度

            g.DrawLine(pen, new Point(0, 0), new PointF((float)0, y));



            //绘制分针
            pen.Width = 3;

            g.RotateTransform(-6 * second);                              //恢复系统的偏移量

            g.RotateTransform((float)((minute * 6) + (second * 0.1)));   //分针的旋转角度 = 分钟数*每分钟分针的旋转角度 + 秒针数*每秒钟分针的旋转角度

            y = (float)((-1) * (h - 40) / 2.75);                         //分针的长度

            g.DrawLine(pen, new Point(0, 0), new PointF((float)0, y));


            //绘制时针
            pen.Width = 6;

            g.RotateTransform((float)(-second * 0.1 - minute * 6));                //恢复系统偏移量

            g.RotateTransform((float)(hour * 30 + minute * 0.1 + second * 0.01));  //时针的旋转角度=

            y = (float)((-1) * (h - 60) / 2.75);                                   //时针的长度

            g.DrawLine(pen, new Point(0, 0), new PointF((float)0, y));

            g.ResetTransform();

            string DateTimeStr = DateTime.Now.ToString();

            SizeF size=g.MeasureString(DateTimeStr, myFont);   //获取界面上所绘制字符串的尺寸
            
            //绘制当前时间
            g.DrawString(DateTimeStr, myFont, Brushes.Yellow, (x1 + w - 4) / 2 - (size.Width)/2, (y1 + w - 4) * 2 / 3); ;

           } 
    }
}
