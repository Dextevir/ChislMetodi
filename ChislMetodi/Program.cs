using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChislMetodi
{
    class Program
    {
        static void Main(string[] args)
        {
            //int n = 10;
            //int k = 5;
            //var solver = new SimmHaleckySolver(8, 4);
            //var matrix = solver.ToMatrix();
            //PrintMatrix(matrix);
            //Console.WriteLine();
            ////PrintMatrix(solver.UpperPart);
            //solver.Solve();
            //PrintMatrix(solver.MatrixB);
            //Console.WriteLine();
            //PrintMatrix(solver.MatrixC);
            //Console.WriteLine();
            //solver.SolveStep2();
            //PrintVector(solver.VectorX);
            //Console.WriteLine();
            //PrintVector(solver.VectorY);
            //Console.WriteLine();
            //PrintVector(solver.VectorXOrig);
            try
            {
                Console.WriteLine($"sredpogr = {SredPogr(10, 140, 15)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        static void PrintVector(double[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                Console.Write($"{Math.Round(vector[i],5)} ");
            }
            Console.WriteLine();
        }
        static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }
        static double SredPogr(int count, int n, int l)
        {
            double[] vectorPogr = new double[count];
            for(int i=0;i<count;i++)
            {
                var solver = new SimmHaleckySolver(n, l);
                solver.Solve();
                solver.SolveStep2();
                vectorPogr[i] = solver.Pogr();
            }
            return vectorPogr.Average();            
        }


    }
}
