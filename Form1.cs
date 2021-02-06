using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chourbot_vacuum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Timer timer;
        private int sizeMatrix;

        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += Timer_Tick;
            sizeMatrix = 5;

            Initialize();
        }

        private void Initialize()
        {
            throw new NotImplementedException();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            Bitmap bitmap = new Bitmap(manor.Width, manor.Height);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.FillRectangle(Brushes.Gray, 0, 0, manor.Width, manor.Height);

            manor.BackgroundImage = bitmap;
            float sizeRoom = (float)manor.Width / sizeMatrix;
            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = 0; j < sizeMatrix; j++)
                {
                    graphics.FillRectangle(Brushes.White, i * sizeRoom + 1, j * sizeRoom + 1, sizeRoom - 2, sizeRoom - 2);
                }
            }
        }
    }
}
