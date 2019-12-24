using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMetodi
{
    class EigenValuesSolver
    {
        public double[,] Matrix;
        public double[] EigenValues;
        public double[,] EigenVectors;
        public double OriginalValue;
        public double[] OriginalVector;
        public double FindedValue;
        public double[] FindedVector;
        public int IterationsCount;
        public double EpsilonLambda;
        public double EpsilonG;
        public double MaximumIterations;
        public double FirstValue;
        public double[] FirstVector;
        public double TochnostValue;
        public double TochnostVector;
        public int N;
        Random rnd;

        public EigenValuesSolver(int n, int iterations, double epsilonLambda, double epsilonG)
        {
            rnd = new Random();
            N = n;
            EpsilonLambda = epsilonLambda;
            EpsilonG = epsilonG;
            MaximumIterations = iterations;
            GenerateTestMatrix();
        }
        public void Solve()
        {
            double[,] matrix1 = Matrix;
            double[] vectorX = new double[N];
            vectorX = new double[N];
            for (int i = 0; i < N; i++)
            {
                double z = rnd.Next() % 4 + 2 + rnd.NextDouble();
                while (vectorX.Select(_=>Math.Abs(_)).Contains(Math.Abs(z)))
                {
                    z = rnd.Next() % 4 + 2 + rnd.NextDouble();
                }
                vectorX[i] = z;
            }
            while (Math.Round(Multiplicationd(vectorX, FirstVector), 1) == 0 || Math.Round(Multiplicationd(vectorX, OriginalVector), 1) == 0)
            {
                vectorX = new double[N];
                for (int i = 0; i < N; i++)
                {
                    double z = rnd.Next() % 4 + 2 + rnd.NextDouble();
                    while (vectorX.Select(_ => Math.Abs(_)).Contains(Math.Abs(z)))
                    {
                        z = rnd.Next() % 4 + 2 + rnd.NextDouble();
                    }
                    vectorX[i] = z;
                }
            }
            FindedVector = new double[N];
            matrix1 = MatrixMinusMatrix(matrix1, Multiplication(Multiplicationm(FirstVector, FirstVector), FirstValue));
            for(int i=0;i<MaximumIterations;i++)
            {
                IterationsCount = i + 1;
                var pVec = FindedVector;
                FindedVector = Normalize(vectorX);
                vectorX = Multiplicationl(matrix1, FindedVector);
                var pVal = FindedValue;
                FindedValue = Multiplicationd(vectorX, FindedVector);

                TochnostValue =  Math.Abs(pVal - FindedValue);
                TochnostVector = AngleVec(pVec, FindedVector);
                if ( TochnostVector<= EpsilonG && TochnostValue <= EpsilonLambda) break;
            }
        }

        double NormaVec(double[] vector)
        {
            return Math.Sqrt(vector.Select(_ => _ * _).Sum());
        }

        double AngleVec(double []v1, double[] v2)
        {
            return Math.Abs(Math.Acos ((Multiplicationd(v1,v2)/NormaVec(v1) / NormaVec(v2)   )));
        }
        static double[] Normalize(double[] vector)
        {
            return vector.Select(_ => _ / Math.Sqrt(vector.Select(__ => __ * __).Sum())).ToArray();
        }
        void GenerateTestMatrix()
        {
            EigenValues = new double[N];
            for (int i = 0; i < N; i++)
            {
                double z = rnd.Next() % 4 + 2 + rnd.NextDouble();
                while ( EigenValues.Select(_=>Math.Abs(_)).Contains(Math.Abs(z)) )
                {
                    z = rnd.Next() % 4 + 2 + rnd.NextDouble();
                }
                EigenValues[i] = z;
            }
            double[,] MatrixA = new double[N, N];
            for(int i=0;i<N;i++)
            {
                MatrixA[i, i] = EigenValues[i];
            }
            double[] vectorOmega = new double[N];
            vectorOmega = vectorOmega.Select(_ => GetRandom()+50d).ToArray();
            double nornaOmega = Math.Sqrt(vectorOmega.Select(_ => _ * _).Sum());
            vectorOmega = vectorOmega.Select(_ => _ / nornaOmega).ToArray();
            EigenVectors = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                EigenVectors[i, i] = 1;
            }

            EigenVectors = MatrixMinusMatrix(EigenVectors, Multiplication(Multiplicationm(vectorOmega, vectorOmega), 2));
            Matrix = Multiplication(EigenVectors, MatrixA);
            Matrix = Multiplication(Matrix, Transpose(EigenVectors));

            FirstValue = EigenValues.Max();
            for (int i = 0; i < N; i++)
            {
                if (Math.Abs(EigenValues[i]) > Math.Abs(FirstValue))
                {
                    FirstValue = EigenValues[i];
                }
            }


            OriginalValue = EigenValues.Where(_ => _ != FirstValue).Max();
            var peka = EigenValues.Where(_ => _ != FirstValue).ToArray();
            for (int i = 0; i < peka.Length; i++)
            {
                if (Math.Abs(peka[i]) > Math.Abs(OriginalValue))
                {
                    OriginalValue = peka[i];
                }
            }

            int oIndex=default;
            for (int i = 0;i<N;i++)
            {
                if(EigenValues[i] == OriginalValue)
                {
                    oIndex = i;
                }
            }
            OriginalVector = new double[N];
            for(int i=0;i<N;i++)
            {
                OriginalVector[i] = EigenVectors[i, oIndex];
            }
            for (int i = 0; i < N; i++)
            {
                if (EigenValues[i] == FirstValue)
                {
                    oIndex = i;
                }
            }
            FirstVector = new double[N];
            for (int i = 0; i < N; i++)
            {
                FirstVector[i] = EigenVectors[i, oIndex];
            }

        }
        double GetRandom()
        {
            return (double)((rnd.Next() % 20) - 10);
        }

        static double[,] Transpose(double[,] a)
        {
            double[,] r = new double[a.GetLength(1), a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    r[i, j] = a[j, i];
                }
            }
            return r;
        }

        static double[,] MatrixMinusMatrix(double[,] a, double[,] b)
        {
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    r[i, j] += a[i, j] - b[i, j];
                }
            }
            return r;
        }
        static double[,] Multiplication(double[,] a, double c)
        {
            double[,] r = new double[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    r[i, j] += a[i, j] * c;
                }
            }
            return r;
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
        static double[,] Multiplicationm(double[] a, double[] b)
        {
            double[,] r = new double[a.Length,b.Length];
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    r[i, j] += a[i] * b[j];
                }
            }
            return r;
        }
        static double Multiplicationd(double[] a, double[] b)
        {
            double r = 0;
            for (int i = 0; i < a.Length; i++)
            {
                r += a[i] * b[i];
            }
            return r;
        }
        static double[] Multiplicationl(double[,] a, double[] b)
        {
            double[] r = new double[b.Length];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int k = 0; k < b.Length; k++)
                {
                    r[i] += a[i, k] * b[k];
                }
            }
            return r;
        }
        static double[] Multiplicationr(double[,] a, double[] b)
        {
            double[] r = new double[b.Length];
            for (int i = 0; i < a.GetLength(1); i++)
            {
                for (int k = 0; k < b.Length; k++)
                {
                    r[i] += a[k, i] * b[k];
                }
            }
            return r;
        }
        public double[] Component()
        {
            double []result = new double[N];
            double[] kk = Multiplicationl(Matrix, FindedVector);
            double[] mm = FindedVector.Select(_ => _ * FindedValue).ToArray();
            for(int i=0;i<N;i++)
            {
                result[i] = Math.Abs( kk[i] - mm[i]);
            }
            return result;
        }
        public double Pogresh 
        { 
            get 
            {
                return Component().Max();
            } 
        }
    }
}
