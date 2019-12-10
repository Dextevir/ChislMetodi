using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMetodi
{
    class SimmHaleckySolver
    {
        public double[,] UpperPart;
        public int N, L;
        public double[] VectorF;
        public double[] VectorXOrig;

        public double[] VectorX;
        public double[] VectorY;
        public double[,] MatrixB;
        public double[,] MatrixC;
        public double[,] Matrix;
        Random rnd;
        public SimmHaleckySolver(int n, int l)
        {
            rnd = new Random();
            N = n;
            L = l;
            UpperPart = new double[N, L];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < L; j++)
                {
                    UpperPart[i, j] = GetRandom();
                }
            }
            //UpperPart = BadObs(6);
            var matrix = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < N; j++)
                {
                    var p = j - i;
                    if (p < L)
                    {
                        matrix[i, j] = UpperPart[i, p];
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                    matrix[j, i] = matrix[i, j];
                }
            }

            VectorF = new double[N];
            VectorXOrig = new double[n];
            VectorXOrig = VectorXOrig.Select(fuf => 1d).ToArray();
            for(int i=0;i<N;i++)
            {
                double sum = 0;
                for(int j=0;j<N;j++)
                {
                    sum += matrix[i, j];
                }
                VectorF[i] = sum;
            }


        }
        public double[,] BadObs(int k)
        {
            double[,] l = new double[N, N];
            double[,] u = new double[N, N];
            double[,] result = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    l[i, j] = GetRandom();
                    if(i==j)
                    {
                        l[i, j] /= Math.Pow(10d, k);
                    }
                }
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < N; j++)
                {
                    u[i, j] = GetRandom();
                    if (i == j)
                    {
                        u[i, j] /= Math.Pow(10d, k);
                    }
                }
            }

            var matrix = new double[N, N];
            matrix = Multiplication(l, u);
            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < N; j++)
                {
                    var p = j - i;
                    if (p < L)
                    {
                        result[i, p] = matrix[i, j];
                        matrix[i, j] = UpperPart[i, p];
                    }
                }
            }
            return result;
        }
        public double Pogr()
        {
            return VectorX.Select(_ => Math.Abs(_ - 1)).Max();
        }
        static double[,] Multiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }
        double GetRandom()
        {
            return (double)((rnd.Next() % 20)-10);
        }
        public double[,] ToMatrix()
        {
            var matrix = new double[N, N];
            for(int i=0; i<N;i++)
            {
                for(int j=i; j<N;j++)
                {
                    var p = j - i;
                    if(p<L)
                    {
                        matrix[i, j] = UpperPart[i, p];
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                    matrix[j, i] = matrix[i, j];
                }
            }
            return matrix;
        }

        public void Solve()
        {
            
            MatrixB = new double[N, N];
            Matrix = ToMatrix();
            for(int i=0;i<N;i++)
            {
                for(int j=i;j<N;j++)
                {
                    MatrixB[j, i] = Matrix[j, i];
                    double sum = 0;
                    for(int k=0;k<i;k++)
                    {
                        if (Math.Round(MatrixB[k, k], 15) == 0) throw new Exception("Алгоритм не применим");
                        sum += MatrixB[i, k] * MatrixB[j, k] / MatrixB[k, k];
                    }
                    MatrixB[j, i] -= sum;
                }
            }
            MatrixC = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = i; j < N; j++)
                {
                    if (Math.Round(MatrixB[i, i], 15) == 0) throw new Exception("Алгоритм не применим");
                    MatrixC[i, j] = MatrixB[j, i] / MatrixB[i, i];
                }
            }
        }


        public void Solve(int p)
        {
            for (int j = 0; j < N; j++)
            {
                for (int i = j; i <= KN(j, L, N); i++)
                {
                    double S = UpperPart[i, j - i + L - 1];
                    for (int k = 0; k <= j - 1; k++)
                    {
                        int j1 = k - i + L - 1;
                        int j2 = j - k + L - 1;
                        if (j1 >= 0 && (j1 < 2 * L - 1) && j2 >= 0 && (j2 < 2 * L - 1))
                        {
                            S -= MatrixB[i, j1] * MatrixB[k, j2];
                        }
                    }
                    MatrixB[i, j - i + L - 1] = S;
                    S = UpperPart[j, i - j + L - 1];
                    for (int k = 0; k <= j - 1; k++)
                    {
                        int j1 = k - j + L - 1;
                        int j2 = i - k + L - 1;
                        if (j1 >= 0 && (j1 < 2 * L - 1) && j2 >= 0 && (j2 < 2 * L - 1))
                        {
                            S -= MatrixB[j, j1] * MatrixB[k, j2];
                        }

                    }
                    if (j != i)
                    {
                        if (Math.Round(MatrixB[j, L-1], 15) == 0) throw new Exception("Алгоритм не применим");
                        MatrixB[j, i - j + L - 1] = S / MatrixB[j, L - 1];
                    }
                }
            }

        }
        public void SolveStep2()
        {
            VectorY = new double[N];
            VectorX = new double[N];
            for (int i=0;i<N;i++)
            {
                VectorY[i] = VectorF[i];
                double sum = 0;
                for (int k=0;k<i;k++)
                {
                    sum += MatrixB[i, k] * VectorY[k];
                }
                VectorY[i] -= sum;
                if (Math.Round(MatrixB[i, i], 15) == 0) throw new Exception("Алгоритм не применим");
                VectorY[i] /= MatrixB[i, i];
            }
            for (int i = N-1; i >= 0; i--)
            {
                VectorX[i] = VectorY[i];
                double sum = 0;
                for (int k = i+1; k < N; k++)
                {
                    sum += MatrixB[k, i] * VectorX[k];
                }
                if (Math.Round(MatrixB[i, i], 15) == 0) throw new Exception("Алгоритм не применим");
                sum /= MatrixB[i, i];
                VectorX[i] -= sum;
            }
        }
        int EnidngIndex(int i)
        {
            if(i<=N-L)
            {
                return L;
            }else
            {
                return N - i;
            }
        }
        int DDDIndex(int i, int j,int k)
        {
            if(i<L)
            {
                return j;
            }
            else if(k>=L)
            {
                return j + (i - k);
            }
            else
            {
                return j + (i - L + 1);
            }
        }
        int K0(int i, int L)
        {
            if (i >= L) return i-L + 1;
            else return 0;
        }


        int KN(int i, int L, int n)
        {
            if (i > L) return i - L;
            else return L - 1;
        }

    }
}
