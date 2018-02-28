using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Life
{
    public partial class ps : Form
    {
        SolidBrush blueBrush = new SolidBrush(Color.Gray);


        Mass myMas = new Mass();

        int StepX = 0, StepY = 0;
        int i = 0, j = 0;


        public ps()
        {
            InitializeComponent();

            this.StepX = panel1.Width / Constants.xLine;
            this.StepY = panel1.Height / Constants.yLine;

            myMas.pointData(StepX, StepY);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //отрисовка полей
            Graphics gr = e.Graphics;

            int w = panel1.Width;
            int h = panel1.Height;

            for (i = 0; i < w; i += StepX)
            {
                gr.DrawLine(Pens.Gray, new Point(i, 0), new Point(i, h));
            }

            for (i = 0; i < h; i += StepY)
            {
                gr.DrawLine(Pens.Gray, new Point(0, i), new Point(w, i));
            }

            // массив
            if (myMas.livingElement() == true)
            {
                int xx, yy;
                Pen p = new Pen(Brushes.Coral);

                for (i = 0; i < Constants.xLine; i++)
                    for (j = 0; j < Constants.yLine; j++)
                    {
                        if (myMas.alive(i, j) == true)
                        {
                            xx = myMas.returnPointX(i, j);
                            yy = myMas.returnPointY(i, j);

                            Rectangle rect = new Rectangle(xx, yy, StepX, StepY);
                            e.Graphics.FillRectangle(blueBrush, rect);
                        }
                    }
            }

        }


        //forI, forJ
        public int numBlockPoint(int z, int step)
        {
            while (z % step != 0)
                z--;
            return z / step;
        }

        //////////////////
        private void Start_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (myMas.repeat0() == true && myMas.repeat() == true && myMas.livingElement() == true)
            {
                myMas.processOfStep();
                panel1.Invalidate();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Все закончилось/ или зависло", "", MessageBoxButtons.OK);
                myMas.ClearMas();
                panel1.Invalidate();
            }
        }

        private void panel1_MouseClick_1(object sender, MouseEventArgs e)
        {
            int x = Convert.ToInt32(e.X);
            int y = Convert.ToInt32(e.Y);

            int forI = numBlockPoint(x, StepX);
            int forJ = numBlockPoint(y, StepY);

            myMas.editLive(forI, forJ);
            panel1.Invalidate();
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            myMas.pointData(StepX, StepY);
            Thread.Sleep(1000);
            timer1.Start();
            panel1.Invalidate();
        }
    }

    public class Constants
    {
        public const int xLine = 34;
        public const int yLine = 29;
        /*public const int xLine = 10;
        public const int yLine = 10;*/
    }
}

