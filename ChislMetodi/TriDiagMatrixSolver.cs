using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMetodi
{
    class TriDiagMatrixSolver
    {
        double[] DiagMain;
        double[] DiagUp;
        double[] DiagDown;
        double[] VectorF;
        double[] Gorizontal1;
        double[] Gorizontal2;
        int k, n;
        public TriDiagMatrixSolver(int n, int k)
        {
            Random rnd = new Random();
            DiagMain = new double[n];
            DiagMain = DiagMain.Select(fuf => rnd.NextDouble()).ToArray();
            DiagUp = new double[n-1];
            DiagUp = DiagUp.Select(fuf => rnd.NextDouble()).ToArray();
            DiagDown = new double[n-1];
            DiagDown = DiagDown.Select(fuf => rnd.NextDouble()).ToArray();
            Gorizontal1 = new double[n];
            Gorizontal1 = Gorizontal1.Select(fuf => rnd.NextDouble()).ToArray();
            Gorizontal2 = new double[n];
            Gorizontal2 = Gorizontal2.Select(fuf => rnd.NextDouble()).ToArray();
            VectorF = new double[n];
            var vectorX = new double[n];
            vectorX = vectorX.Select(fuf => 1d).ToArray();
            for (int i=0;i<n;i++)
            {
                if(i == k)
                {
                    VectorF[k] = Gorizontal1.Select(fuf => fuf * vectorX[k]).Sum();
                }else if(i == k+1)
                {
                    VectorF[k+1] = Gorizontal2.Select(fuf => fuf * vectorX[k]).Sum();
                }
                else if(i==0)
                {
                    VectorF[0] = DiagMain[0] * vectorX[0] + DiagUp[0] * vectorX[1];
                }else if(i==n-1)
                {
                    VectorF[n-1] = DiagMain[n] * vectorX[n] + DiagDown[n-1] * vectorX[n-1];
                }
                else
                {
                    VectorF[i] = DiagDown[i - 1] * vectorX[i - 1] + DiagMain[i] * vectorX[i] + DiagUp[i] * vectorX[i + 1];
                }
            }
            
            
        }
    }
}
