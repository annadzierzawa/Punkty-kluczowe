using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punkty_kluczowe
{ 
    class Program
    {
        struct Point
        {
            public int x;
            public int y;
            public double value;
        }

        static void Main(string[] args)
        {

            Bitmap btm = new Bitmap(@"D:\Semestr 4\Systemy sztucznej inteligencji\kot-1030x686.JPG");
            Bitmap btmF = new Bitmap(btm.Width, btm.Height);

            double[][] kernel = new double[3][];
            kernel[0] = new double[] { -1, -1, -1 };
            kernel[1] = new double[] { -1, 8, -1 };
            kernel[2] = new double[] { -1, -1, -1 };



            for (int i = 0; i < btm.Width - kernel.Length; i++)
            {
                for (int j = 0; j < btm.Height - kernel[0].Length; j++)
                {
                    double ss1 = 0;
                    double ss2 = 0;
                    double ss3 = 0;

                    for (int l = kernel.Length - 1; l > -1; l--)
                    {
                        for (int k = kernel[0].Length - 1; k > -1; k--)
                        {

                            Color pxl = btm.GetPixel(i + kernel.Length - 1 - l, j + kernel[0].Length - 1 - k);
                            ss1 += kernel[l][k] * pxl.R;
                            ss2 += kernel[l][k] * pxl.G;
                            ss2 += kernel[l][k] * pxl.B;

                        }
                    }

                    if (ss1 > 255) ss1 = 255;
                    if (ss1 < 0) ss1 = 0;
                    if (ss2 > 255) ss2 = 255;
                    if (ss2 < 0) ss2 = 0;
                    if (ss3 > 255) ss3 = 255;
                    if (ss3 < 0) ss3 = 0;

                    btmF.SetPixel(i, j, Color.FromArgb((int)ss1, (int)ss2, (int)ss3));
                }
                //btmF.Save(@"D:\Semestr 4\Systemy sztucznej inteligencji\btmF.JPG");
            }
            double[] tableofmaxs = new double[1000];
            Point[] tableOfPoints = new Point[1000];
            

            for (int i = 0; i < tableofmaxs.Length; i++)
            {
                tableofmaxs[i] = 0;
            }

            for (int i = 0; i < btmF.Width - kernel.Length; i++)
            {
                for (int j = 0; j < btmF.Height-kernel[0].Length; j++)
                {
                    double w = 0;
                    Color pxl = btmF.GetPixel(i, j);
                    for (int l = kernel.Length - 1; l > -1; l--)
                    {
                        for (int k = kernel[0].Length - 1; k > -1; k--)
                        {
                            Color pxl2 = btmF.GetPixel(i + kernel.Length - 1 - l, j + kernel[0].Length - 1 - k);
                            w += pxl2.R + pxl2.G + pxl2.B;

                        }
                    }
                    w = w / 9;
                    Console.WriteLine(w);

                    //w = pxl.R + pxl.G + pxl.B;
                    double min = tableofmaxs.Min();
                    if (w > min)
                    {
                        int index = Array.IndexOf(tableofmaxs, min);
                        tableofmaxs[index] = w;
                        tableOfPoints[index].value = w;
                        tableOfPoints[index].x = i;
                        tableOfPoints[index].y = j;
                    }
                }

            }
            foreach (var item in tableOfPoints)
            {
                btmF.SetPixel(item.x, item.y, Color.FromArgb(250, 22, 170));

            }
            btmF.Save(@"D:\Semestr 4\Systemy sztucznej inteligencji\btmFpokeypoints.JPG");

        }
    }
}
