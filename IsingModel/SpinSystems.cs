using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace IsingModel
{
    class SpinSystems
    {
        //define the data variables
        Random rnd;
        int no_eleectron;
        int[,] Lattice;
        int E1, E2, x, y, xoffset, yoffset;
        int KB, J;
        Graphics gg;
        SolidBrush sbw, sbr;
        public SpinSystems(Form1 f1, int NE)
        {
            rnd = new Random();
            J = 1;
            KB = 1;
            no_eleectron = NE;
            Lattice = new int[no_eleectron, no_eleectron];
            gg = f1.CreateGraphics();
            sbw = new SolidBrush(Color.White);
            sbr = new SolidBrush(Color.Red);
            x = f1.ClientSize.Width / 2;
            y = f1.ClientSize.Height / 2;
            xoffset = x / 2;
            yoffset = y / 2;
            for (int i = 0; i < no_eleectron; i++)
            {
                for (int j = 0; j < no_eleectron; j++)
                {
                    Lattice[i, j] = 1;
                    gg.FillEllipse(sbw, x - xoffset + j * 6, y - yoffset + i * 6, 5, 5);
                }
            }
        }
        public int EnergyCal(int r, int c)
        {
            int ans = 0;
            if (r != 0 && c != 0 && r != no_eleectron - 1 && c != no_eleectron - 1)
            {
                ans = -J * Lattice[r, c] * (Lattice[r - 1, c] + Lattice[r + 1, c] + Lattice[r, c - 1] * Lattice[r, c + 1]);
            }
            return ans;

        }
        public void FlipDecide(int r, int c, double T)
        {
            if (r != 0 && c != 0 && r != no_eleectron - 1 && c != no_eleectron - 1)
            {
                E1 = EnergyCal(r, c);
                Lattice[r, c] = Lattice[r, c] * -1;
                E2 = EnergyCal(r, c);
                if (E2 <= E1)
                {
                    if (Lattice[r, c] == 1)
                    {
                        gg.FillEllipse(sbw, x - xoffset + c * 6, y - yoffset + r * 6, 5, 5);
                    }
                    if (Lattice[r, c] == -1)
                    {
                        gg.FillEllipse(sbr, x - xoffset + c * 6, y - yoffset + r * 6, 5, 5);
                    }
                }
                else
                {
                    if (rnd.NextDouble() <= Math.Exp(-(E2 - E1) / (KB * T)))
                    {
                        if (Lattice[r, c] == 1)
                        {
                            gg.FillEllipse(sbw, x - xoffset + c * 6, y - yoffset + r * 6, 5, 5);
                        }
                        if (Lattice[r, c] == -1)
                        {
                            gg.FillEllipse(sbr, x - xoffset + c * 6, y - yoffset + r * 6, 5, 5);
                        }
                    }
                    else
                    {
                        Lattice[r, c] = Lattice[r, c] * -1;
                    }
                }
            }
        }
        public double Mag()
        {
            double ans = 0;
            for (int i = 0; i < no_eleectron; i++)
            {
                for (int j = 0; j < no_eleectron; j++)
                {
                    if (i != 0 && j != 0 && i != no_eleectron && j != no_eleectron - 1)
                    {
                        ans = ans + Lattice[i, j];
                    }
                }
            }
            ans = ans / (no_eleectron * no_eleectron - 4 * no_eleectron);
            return ans;
        }
    }
}

