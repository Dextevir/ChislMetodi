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
        public double[] VectorX;
        Random rnd;
        public SimmHaleckySolver(int n, int l)
        {
            rnd = new Random();
            N = n;
            L = l;
            UpperPart = new double[N, L];
            for(int i=0;i<N;i++)
            {
                for(int j=0;j<L;j++)
                {
                    UpperPart[i, j] = GetRandom();
                }
            }
        }

        double GetRandom()
        {
            return (double)((rnd.Next() % 9)+1);
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
    }
}
