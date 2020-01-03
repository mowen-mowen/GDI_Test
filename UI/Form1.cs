using GDI_Test.UI;
using NewControlTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GDI_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Refresh();
        }

        #region 绘制直线 矩形 椭圆
        void Test_one()
        {
            Graphics g = this.CreateGraphics();              //创建一个画板

            Pen p = new Pen(Color.Blue, 2);

            g.DrawLine(p, 10, 10, 110, 110);      //在画板上画直线,起始坐标为(10,10),终点坐标为(100,100)


            Thread.Sleep(2000);
            g.DrawRectangle(p, 10, 10, 100, 100); //在画板上画矩形,起始坐标为(10,10), 宽为100, 高为100

            Thread.Sleep(2000);
            g.DrawEllipse(p, 10, 10, 100, 100);   //在画板上画椭圆,起始坐标为(10,10),外接矩形的宽为100,高为100
        }

        #endregion

        #region  绘制虚线  自定义虚线 箭头
        void Test_two()
        {
            Pen p = new Pen(Color.Blue, 3);        //设置笔的粗细为,颜色为蓝色
            Graphics g = this.CreateGraphics();

            //画虚线
            p.DashStyle = DashStyle.Dot;            //定义虚线的样式为点
            g.DrawLine(p, 10, 10, 200, 10);

            //自定义虚线
            p.DashPattern = new float[] { 3, 1 };   //设置短划线和空白部分的数组
            g.DrawLine(p, 10, 50, 200, 50);

            //画箭头,只对不封闭曲线有用
            p.DashStyle = DashStyle.Solid;          //画笔类型定义为实线
            p.StartCap = LineCap.SquareAnchor;
            p.EndCap = LineCap.ArrowAnchor;         //定义线尾的样式为箭头
            g.DrawLine(p, 10, 100, 200, 100);

            g.Dispose();
            p.Dispose();
        }
        #endregion

        #region 绘制矩形 填充矩形 图片填充
        void Test_three()
        {
            Graphics g = this.CreateGraphics();
                   
            Rectangle rect = new Rectangle(10, 10, 200, 200);   //定义矩形,参数为起点横纵坐标以及其长和宽

            //单色填充
            SolidBrush b1 = new SolidBrush(Color.Blue);//定义单色画刷          
            g.FillRectangle(b1, rect);//填充这个矩形


            //用图片填充
            rect.Location = new Point(250, 10);//更改这个矩形的起点坐标
            TextureBrush b2 = new TextureBrush(Image.FromFile(@"C:\Users\SPGZ\Desktop\常用文件\壁纸\07.png"));

            g.FillRectangle(b2, rect);


            //影线画刷（模式，前景色，背景色） 
            HatchBrush hb = new HatchBrush(HatchStyle.BackwardDiagonal, Color.FromArgb(255, 0, 0), Color.Gray);

            rect.Location = new Point(250, 250);//更改这个矩形的起点坐标

            g.FillRectangle(hb, rect);


            //线性渐变色填充
            rect.Location = new Point(10, 250);
            LinearGradientBrush b3 = new LinearGradientBrush(rect, Color.Yellow, Color.Black, LinearGradientMode.Horizontal);
            g.FillRectangle(b3, rect);


            //路径渐变色  PathGradientBrush
            rect.Location = new Point(520, 10);

            Point[] pts = {
                           new Point(rect.Left,rect.Top),
                           new Point(rect.Right,rect.Top),
                           new Point(rect.Right,rect.Bottom),
                           new Point(rect.Left,rect.Bottom)
                           };

            PathGradientBrush pb = new PathGradientBrush(pts);

            //设置颜色
            pb.CenterColor = Color.Green;
            pb.SurroundColors = new Color[] { SystemColors.ButtonFace };   //设置周围的颜色为背景颜色（否则默认为白色，会有一个白色边框）

            //填充矩形并绘制其边框
            g.FillRectangle(pb, rect);
       
            //释放资源
            g.Dispose();
            b1.Dispose();
            b2.Dispose();
            b3.Dispose();
            hb.Dispose();
            pb.Dispose();

        }
        #endregion

        #region 多色填充
        void Test_Three_one()
        {
            Graphics g = this.CreateGraphics();

            Rectangle rect = new Rectangle(10, 10, 250, 250);   //定义矩形,参数为起点横纵坐标以及其长和宽

            //多色渐变
            rect.Location = new Point(100, 100);
            g.Clear(Color.White);

            Color[] colors =        //多种颜色
            {
                Color.Red,
                Color.Green,
                Color.Blue
            };

            float[] positions =      //此值表示各个颜色所在的位置（0-1）
            {
                0.0f,
                0.3f,
                1.0f
            };

            //定义ColorBlend对象
            ColorBlend colorBlend = new ColorBlend(3);
            colorBlend.Colors = colors;
            colorBlend.Positions = positions;

            //定义线型渐变画刷
            LinearGradientBrush lBrush = new LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.Horizontal);

            //设置渐变画刷的多色渐变信息
            lBrush.InterpolationColors = colorBlend;

            g.FillRectangle(lBrush, rect);
        }

        #endregion

        #region  旋转坐标轴
        void Test_four()
        {
            Graphics g = this.CreateGraphics();

            //单色填充
            //SolidBrush b1 = new SolidBrush(Color.Blue);//定义单色画刷          
            Pen p = new Pen(Color.Blue, 1);

            //转变坐标轴角度
            for (int i = 0; i < 90; i++)
            {
                g.RotateTransform(i);//每旋转一度就画一条线

                g.DrawLine(p, 0, 0, 100, 0);

                g.ResetTransform();//恢复坐标轴坐标
            }

            Thread.Sleep(1000);

            //先平移到指定坐标,然后进行度旋转
            g.TranslateTransform(100, 200);

            for (int i = 0; i < 8; i++)
            {
                g.RotateTransform(45);             //将角度旋转45°
                g.DrawLine(p, 0, 0, 100, 0);       //画一条直线
            }

            g.Dispose();
        }

        #endregion

        #region 绘制柱状图
        void Test_five()
        {
            Graphics g = CreateGraphics();
            Pen MyPen = new Pen(Color.Blue, 2);

            int x = 100;
            for (int i = 0; i <= 10; i++)   //绘制纵向线条
            {
                g.DrawLine(MyPen, x, 400, x, 100);
                x += 40;
            }

            Thread.Sleep(1000);  //线程休眠200毫秒，便于观察绘制情况
            int y = 400;
            for (int i = 0; i < +10; i++)   //绘制横向线条 
            {
                g.DrawLine(MyPen, 100, y, 550, y);
                y -= 30;
            }

            Thread.Sleep(1000);
            x = 110;
            y = 400;
            Brush MyBrush = new SolidBrush(Color.BlueViolet);

            int[] saleNum = { 120, 178, 263, 215, 99, 111, 265, 171, 136, 100, 129 };
            for (int i = 0; i < saleNum.Length; i++)
            {
                g.FillRectangle(MyBrush, x, y - saleNum[i], 20, saleNum[i]);  //绘制填充矩形
                x += 40;
            }

            g.Dispose();
            MyPen.Dispose();
        }

        #endregion

        #region 绘制三角形
        void Test_six()
        {
            Graphics g = CreateGraphics();
            Pen p = new Pen(Color.Green, 2);

            Point[] points = new Point[3];

            points[0] = new Point(10, 10); ;
            points[1] = new Point(10, 190);
            points[2] = new Point(190, 290);

            Brush myBrush = new SolidBrush(Color.Green);

            g.FillPolygon(myBrush, points, FillMode.Winding);
        }

        #endregion

        #region 自定义定时器控件
        private void button8_Click(object sender, EventArgs e)
        {
            UserControl1 control = new UserControl1();
            control.Location = new Point(100, 100);
            this.Controls.Add(control);
        }
        #endregion

        #region 绘制圆弧

        void Test_Seven()
        {


            Graphics g = this.CreateGraphics();              //创建一个画板

            Pen p = new Pen(Color.Blue, 2);

            Rectangle r = new Rectangle(250,50,200,200);

            g.DrawRectangle(p, r);             //在画板上画矩形

            g.DrawArc(p, r, 180, 180);         //绘制圆弧


        }
        #endregion

        #region 绘制扇形
        void Test_eight()
        {
            Graphics g = this.CreateGraphics();              //创建一个画板

            Pen p = new Pen(Color.Blue, 2);

            Rectangle r = new Rectangle(250, 250, 200, 200);

        //    g.DrawRectangle(p, r);     //在画板上画矩形,起始坐标为(10,10),宽为,高为

            g.DrawPie(p,r,180,90);    //绘制扇形
        }
        #endregion

        #region 绘制折线
        void Test_none()
        {
            Graphics g = this.CreateGraphics();              //创建一个画板

            Pen p = new Pen(Color.Blue, 2);

            Point[] points = new Point[4];

            points[0] = new Point(50,50);
            points[1] = new Point(70,150);
            points[2] = new Point(90,50);
            points[3] = new Point(110,100);

            g.DrawLines(p, points);    //绘制
        }

        #endregion

        #region 折线图
        void Test_ten()
        {
            Pen p = new Pen(Color.Black, 3);        //设置笔的粗细为,颜色为蓝色
            Graphics g = this.CreateGraphics();

            //画箭头,只对不封闭曲线有用
            p.EndCap = LineCap.ArrowAnchor;         //定义线尾的样式为箭头
            g.DrawLine(p, 50, 400, 500, 400);

            g.DrawLine(p, 50, 400, 50, 50);

            p = new Pen(Color.Blue, 2);

            Point[] points = new Point[4];
            points[0] = new Point(100, 100);
            points[1] = new Point(200, 300);
            points[2] = new Point(300, 150);
            points[3] = new Point(380, 350);

            g.DrawLines(p, points);    //绘制

        }
        #endregion

        #region  绘制组合图形     

        void Test_eleven()
        {
            Pen p = new Pen(Color.Black, 1);        //设置笔的粗细为,颜色为蓝色
            Graphics g = this.CreateGraphics();

            Rectangle r = new Rectangle(50, 50, 200, 200);
            int i = 100;

            GraphicsPath path = GetGraphicsPath(r, i);

            g.DrawPath(p, path);
        }

        private GraphicsPath GetGraphicsPath(Rectangle rc, int r)
        {
            int x = rc.X, y = rc.Y, w = rc.Width, h = rc.Height;

            GraphicsPath path = new GraphicsPath();

            path.AddArc(x, y, r, r, 180, 90);				      //AddArc：追加一段圆弧
            path.AddArc(x + w - r, y, r, r, 270, 90);			
            path.AddArc(x + w - r, y + h - r, r, r, 0, 90);		
            path.AddArc(x, y + h - r, r, r, 90, 90);            

            path.CloseFigure();                                 //闭合当前图形    
            return path;
        }

        #endregion

        #region 水晶按钮

        void Test_twelve()
        {

        }



        #endregion

        #region 获取电脑安装的所有字体
        void Test_Thirteen()
        {
            List<string> list = new List<string>();

            InstalledFontCollection installedCollection = new InstalledFontCollection();

            FontFamily[] fontFamilyArray = installedCollection.Families;

            foreach (FontFamily i in fontFamilyArray)
            {
                list.Add(i.Name);
            }

            ComboBox c = new ComboBox();
            c.Size = new Size(200,50);
            c.Location = new Point(200,200);

            this.Controls.Add(c);

            c.DataSource = list;

        }

        #endregion

        #region 渐变色环   //使用一条有宽度的弧线来代替当作圆环
        void Test_Fourteen()
        {
            Graphics graphics = this.CreateGraphics();

            Rectangle r = new Rectangle(100, 100, 300, 300);   //定义圆弧外接矩形
            Pen pen;

            float PI = 3.1415926f;
            int angle = 0;
            int wide = 40;             //色环宽度

            graphics.SmoothingMode = SmoothingMode.AntiAlias;           //抗锯齿

            for (float i = 0.0f; i < PI; i += (float)((float)PI / 360))
            {
                float h, tc, tx;
                float tr, tg, tb;

                tr = tg = tb = 1;
                h = i * 1.9f;
                tc = 1;

                tx =  tc* (1 - Math.Abs(Fmod(h, 2) - 1));

                if (0 <= h && h < 1) { tr = tc; tg = tx; tb = 0; }
                else if (1 <= h && h < 2) { tr = tx; tg = tc; tb = 0; }
                else if (2 <= h && h < 3) { tr = 0; tg = tc; tb = tx; }
                else if (3 <= h && h < 4) { tr = 0; tg = tx; tb = tc; }
                else if (4 <= h && h < 5) { tr = tx; tg = 0; tb = tc; }
                else if (5 <= h && h < 6) { tr = tc; tg = 0; tb = tx; }

                tr = tr * 255;
                tg = tg * 255;
                tb = tb * 255;

                pen = new Pen(Color.FromArgb(255, (byte)tr, (byte)tg, (byte)tb), wide);

                angle += 1;

                graphics.DrawArc(pen, r, angle, 3);
            }

            float Fmod(float x,float y)
            {
               return x % y;
            }
        }

        #endregion

        #region 贝塞尔样条：一组由多个点组连接而成的原话曲线

        void Test_fifteen()
        {
            Graphics g = this.CreateGraphics();

            Pen blackPen = new Pen(Color.Black, 3);

            float startX = 100.0F;
            float startY = 100.0F;
            float controlX1 = 200.0F;
            float controlY1 = 10.0F;
            float controlX2 = 350.0F;
            float controlY2 = 50.0F;
            float endX = 500.0F;
            float endY = 100.0F;

            g.DrawBezier(blackPen,
                startX, startY,
                controlX1, controlY1,
                controlX2, controlY2,
                endX, endY);

        }
        #endregion

        #region 贝塞尔与普通对比
        void Test_sixteen()
        {
            Graphics g = this.CreateGraphics();
            Pen redPen = new Pen(Color.Red, 3);

            Pen greenPen = new Pen(Color.Green, 3);

            PointF point1 = new PointF(50.0F, 50.0F);
            PointF point2 = new PointF(100.0F, 25.0F);
            PointF point3 = new PointF(200.0F, 5.0F);
            PointF point4 = new PointF(250.0F, 50.0F);
            PointF point5 = new PointF(300.0F, 100.0F);
            PointF point6 = new PointF(350.0F, 200.0F);
            PointF point7 = new PointF(250.0F, 250.0F);

            PointF[] curvePoints = { point1, point2, point3, point4, point5, point6, point7 };

            g.DrawLines(redPen, curvePoints);          //普通的折线

            g.DrawClosedCurve(greenPen, curvePoints);  //将折线变为闭合的贝塞尔曲线
        }

        #endregion

        #region 按钮

        private void 直线矩形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_one();
        }

        private void 虚线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_two();
        }
        private void 填充矩形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_three();
        }

        private void 旋转坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_four();
        }

        private void 柱状图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_five();
        }

        private void 三角形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_six();
        }

        private void 自定义控件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button8_Click(sender,e);
        }

        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_Load(sender,e);
        }
        private void 圆弧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Test_Seven();

            Form2 f = new Form2();
            f.ShowDialog();
        }

        private void 扇形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_eight();
        }

        private void 折线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_none();
        }
        private void 折线图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_ten();
        }
        private void 组合图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_eleven();
        }

        private void 字符串ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            SolidBrush b1 = new SolidBrush(Color.Blue);//定义单色画刷   
            //字符串
            g.DrawString("字符串", new Font("宋体", 10), b1, new PointF(90, 10));
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_Thirteen();
        }

        private void 多色填充ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_Three_one();
        }

        private void 渐变色环ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_Fourteen();
        }
        private void 文字特效ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.ShowDialog();
        }

        private void graphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.ShowDialog();
        }

        private void 贝塞尔样条ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_fifteen();
        }

        #endregion

        private void 贝塞尔2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test_sixteen();
        }

    }
}
