using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMetodi
{
    class TriDiagMatrixSolver
    {
        public double[] DiagMain;
        public double[] DiagUp;
        public double[] DiagDown;
        public double[] VectorF;
        public double[] Gorizontal1;
        public double[] Gorizontal2;
        public double[] VectorX;
        public double[] VectorX1;
        public double[] VectorF1;
        Random rnd;
        int k, n;
        public TriDiagMatrixSolver(int N, int K)
        {
            if (K <= 0 || K >= N || N<=2) throw new Exception("Недопустимые данные");

            n = N; k = K-1;
            rnd = new Random();
            DiagMain = new double[n];
            DiagMain = DiagMain.Select(fuf => GetRandom()).ToArray();
            DiagUp = new double[n-1];
            DiagUp = DiagUp.Select(fuf => GetRandom()).ToArray();
            DiagDown = new double[n-1];
            DiagDown = DiagDown.Select(fuf => GetRandom()).ToArray();
            Gorizontal1 = new double[n];
            Gorizontal1 = Gorizontal1.Select(fuf => GetRandom()).ToArray();
            Gorizontal2 = new double[n];
            Gorizontal2 = Gorizontal2.Select(fuf => GetRandom()).ToArray();
            ReDef();
            VectorF = new double[n];
            VectorX = new double[n];
            VectorF1 = new double[n];
            VectorX1 = new double[n];
            VectorX = VectorX.Select(fuf => 1d).ToArray();
            VectorX1 = VectorX1.Select(_ => GetRandom()).ToArray();
            for (int i=0;i<n;i++)
            {
                if(i == k)
                {
                    VectorF[k] = Gorizontal1.Select(fuf => fuf * VectorX[k]).Sum();
                    var buffVecrof = new double[VectorX1.Length];
                    for (int j=0;j<VectorX1.Length;j++)
                    {
                        buffVecrof[j] = VectorX1[j] * Gorizontal1[j];
                        //VectorF1[] = Gorizontal1.Select(fuf => fuf * VectorX1[k]).Sum();
                    }
                    VectorF1[i] = buffVecrof.Sum();
                }
                else if(i == k+1)
                {
                    VectorF[k+1] = Gorizontal2.Select(fuf => fuf * VectorX[k]).Sum();
                    var buffVecrof = new double[VectorX1.Length];
                    for (int j = 0; j < VectorX1.Length; j++)
                    {
                        buffVecrof[j] = VectorX1[j] * Gorizontal2[j];
                        //VectorF1[] = Gorizontal1.Select(fuf => fuf * VectorX1[k]).Sum();
                    }
                    VectorF1[i] = buffVecrof.Sum();
                    //VectorF1[k + 1] = Gorizontal2.Select(fuf => fuf * VectorX1[k]).Sum();
                }
                else if(i==0)
                {
                    VectorF[0] = DiagMain[0] * VectorX[0] + DiagUp[0] * VectorX[1];
                    VectorF1[0] = DiagMain[0] * VectorX1[0] + DiagUp[0] * VectorX1[1];
                }
                else if(i==n-1)
                {
                    VectorF[n-1] = DiagMain[n-1] * VectorX[n-1] + DiagDown[n-2] * VectorX[n-2];
                    VectorF1[n - 1] = DiagMain[n - 1] * VectorX1[n - 1] + DiagDown[n - 2] * VectorX1[n - 2];
                }
                else
                {
                    VectorF[i] = DiagDown[i - 1] * VectorX[i - 1] + DiagMain[i] * VectorX[i] + DiagUp[i] * VectorX[i + 1];
                    VectorF1[i] = DiagDown[i - 1] * VectorX1[i - 1] + DiagMain[i] * VectorX1[i] + DiagUp[i] * VectorX1[i + 1];
                }
            }              
        }
        double GetRandom()
        {            
            return (double)((rnd.Next() % 20-10));
        }
        public double[,] ToMatrix()
        {
            var matrix = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                if (i == k)
                {
                    for(int j=0;j<n;j++)
                    {
                        matrix[i, j] = Gorizontal1[j];
                    }
                }
                else if (i == k + 1)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = Gorizontal2[j];
                    }
                }
                else if (i == 0)
                {
                    matrix[i, 0] = DiagMain[0];
                    matrix[i, 1] = DiagUp[0];
                    for (int j = 2; j < n; j++)
                    {
                        matrix[i, j] = 0;
                    }
                }
                else if (i == n - 1)
                {
                    matrix[i, n - 1] = DiagMain[n - 1];
                    matrix[i, n - 2] = DiagDown[n - 2];
                    for (int j = 0; j < n-2; j++)
                    {
                        matrix[i, j] = 0;
                    }
                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        if(j == i-1)
                        {
                            matrix[i, j] = DiagDown[j];
                        }
                        else if(j == i)
                        {
                            matrix[i, j] = DiagMain[j];
                        }
                        else if(j == i+1)
                        {
                            matrix[i, j] = DiagUp[j-1];
                        }
                        else
                        {
                            matrix[i, j] = 0;
                        }
                    }
                }
            }
            return matrix;
        }
        void ReDef()
        {
            if(k == 0)
            {
                Gorizontal1[0] = DiagMain[0];
                Gorizontal1[1] = DiagUp[0];
                Gorizontal2[0] = DiagDown[0];
                Gorizontal2[1] = DiagMain[1];
                Gorizontal2[2] = DiagUp[1];
            }else if(k==(n-2))
            {
                Gorizontal1[n - 3] = DiagDown[n - 3];
                Gorizontal1[n - 2] = DiagMain[n - 2];
                Gorizontal1[n - 1] = DiagUp[n - 2];
                Gorizontal2[n - 2] = DiagDown[n - 2];
                Gorizontal2[n - 1] = DiagMain[n - 1];
            }else
            {
                Gorizontal1[k - 1] = DiagDown[k - 1];
                Gorizontal1[k] = DiagMain[k];
                Gorizontal1[k+1] = DiagUp[k];
                Gorizontal2[k] = DiagDown[k];
                Gorizontal2[k+1] = DiagMain[k + 1];
                Gorizontal2[k + 2] = DiagUp[k + 1];
            }
        }
        public void SolveStep1()
        {
            for(int i=0; i<k;i++)
            {
                //if (i == k - 1)
                //{
                //    Console.WriteLine();
                //}
                if (Math.Round(DiagMain[i], 20) == 0) throw new Exception("Алгоритм не применим"); 
                var p = DiagDown[i]/ DiagMain[i];
                DiagDown[i] -= DiagMain[i]*p;
                DiagMain[i + 1] -= DiagUp[i] * p;
                if(i!=k-1)
                {
                    VectorF[i + 1] -= VectorF[i] * p;
                    VectorF1[i + 1] -= VectorF1[i] * p;
                }
                if (Math.Round(DiagMain[i], 20) == 0) throw new Exception("Алгоритм не применим");
                p = Gorizontal1[i] / DiagMain[i];
                Gorizontal1[i] -= DiagMain[i] * p;
                Gorizontal1[i+1] -= DiagUp[i] * p;
                VectorF[k] -= VectorF[i] * p;
                VectorF1[k] -= VectorF1[i] * p;

                if (i == k - 2)
                {
                    DiagDown[i+1] -= DiagUp[i] * p;
                }
                if (Math.Round(DiagMain[i], 20) == 0) throw new Exception("Алгоритм не применим");
                p = Gorizontal2[i] / DiagMain[i];
                Gorizontal2[i] -= DiagMain[i] * p;
                Gorizontal2[i + 1] -= DiagUp[i] * p;
                VectorF[k+1] -= VectorF[i] * p;
                VectorF1[k + 1] -= VectorF1[i] * p;
                if (i == k - 1)
                {
                    DiagDown[k] -= DiagUp[i] * p;
                }
            }
        }
        public void SolveStep2()
        {
            for (int i = n - 1; i > k+1; i--)
            {
                if (Math.Round(DiagMain[i], 20) == 0) throw new Exception("Алгоритм не применим");
                var p = DiagUp[i-1] / DiagMain[i];
                DiagUp[i-1] -= DiagMain[i] * p;
                DiagMain[i - 1] -= DiagDown[i-1] * p;
                if (i != k+2)
                {
                    VectorF[i - 1] -= VectorF[i] * p;
                    VectorF1[i - 1] -= VectorF1[i] * p;
                }
                if (Math.Round(DiagMain[i], 20) == 0) throw new Exception("Алгоритм не применим");
                p = Gorizontal1[i] / DiagMain[i];
                Gorizontal1[i] -= DiagMain[i] * p;
                Gorizontal1[i -1] -= DiagDown[i-1] * p;
                VectorF[k] -= VectorF[i] * p;
                VectorF1[k] -= VectorF1[i] * p;
                if (i == k + 2)
                {
                    DiagUp[k] -= DiagDown[i - 1] * p;
                }
                if (Math.Round(DiagMain[i], 20) == 0) throw new Exception("Алгоритм не применим");
                p = Gorizontal2[i] / DiagMain[i];
                Gorizontal2[i] -= DiagMain[i] * p;
                Gorizontal2[i - 1] -= DiagDown[i-1] * p;
                VectorF[k+1] -= VectorF[i] * p;
                VectorF1[k + 1] -= VectorF1[i] * p;
                if (i == k + 3)
                {
                    DiagUp[k+1] -= DiagDown[i - 1] * p;
                }

            }
        }
        public void SolveStep3()///xtn ytnfr
        {
            for (int i = k + 1; i > 0; i--)
            {
                if (Math.Round(DiagMain[i], 20) == 0) throw new Exception("Алгоритм не применим");
                var p = DiagUp[i - 1] / DiagMain[i];
                DiagUp[i - 1] -= DiagMain[i] * p; 
                VectorF[i - 1] -= VectorF[i] * p;
                VectorF1[i - 1] -= VectorF1[i] * p;
                if (i == (k+1))
                {
                    Gorizontal1[k+1] -= DiagMain[i] * p;
                    Gorizontal1[k] -= DiagDown[i-1] * p;
                    DiagMain[i - 1] -= DiagDown[i - 1] * p;
                }
            }
        }
        public void SolveStep4()
        {
            for (int i = k; i < n-1; i++)
            {
                if (Math.Round(DiagMain[i], 20) == 0) throw new Exception("Алгоритм не применим");
                var p = DiagDown[i] / DiagMain[i];
                DiagDown[i] -= DiagMain[i] * p;
                VectorF[i + 1] -= VectorF[i] * p;
                VectorF1[i + 1] -= VectorF1[i] * p;
                if (i == k)
                {
                    Gorizontal2[k]-= DiagMain[i] * p;
                }
            }
        }
        public void SolveStep5()
        {
            for (int i = 0; i < n ; i++)
            {
                var p = DiagMain[i];
                DiagMain[i] /= p;
                VectorF[i] /= p;
                VectorF1[i] /= p;
            }
            ReDef();
        }
        public double Pogr()
        {
            return VectorF.Select(_ =>Math.Abs(_ - 1)).Max();
        }
        public double Pogr1()
        {
            var VectorPogr = new double[VectorX1.Length];
            for(int i=0;i<VectorX1.Length;i++)
            {
                if(VectorX1[i]>1)
                {
                    VectorPogr[i] = Math.Abs((VectorX1[i] - VectorF1[i]) / VectorX1[i]);
                }else
                {
                    VectorPogr[i] = Math.Abs((VectorX1[i] - VectorF1[i]));
                }
 
            }
            return VectorPogr.Max();
        }

    }
    
}
