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
            int n = 10;
            int k = 5;
            var solver = new SimmHaleckySolver(10, 3);
            var matrix = solver.ToMatrix();
            PrintMatrix(matrix);
            PrintMatrix(solver.UpperPart);


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
    }
}
