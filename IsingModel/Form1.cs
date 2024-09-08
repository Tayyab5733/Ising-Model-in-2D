using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IsingModel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int NE = 30;
            SpinSystems ss = new SpinSystems(this, NE);
            SolidBrush sbr = new SolidBrush(Color.Red);
            Graphics gg = CreateGraphics();
            int nsweeps = 50;
            for (double T = 0; T < 10; T = T + 0.1)
            {
                double Ma = 0;
                for (int ns = 0; ns < nsweeps; ns++)
                {
                    for (int i = 0; i < NE; i++)
                    {
                        for (int j = 0; j < NE; j++)
                        {
                            ss.FlipDecide(i, j, T);
                        }
                    }
                    Ma = Ma + ss.Mag();
                }
                Ma = Ma / nsweeps;
                gg.FillEllipse(sbr, 600 + (float)T * 50, 250 - (float)Ma * 50, 5, 5);
            }
        }
    }

}

