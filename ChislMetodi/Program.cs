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
            TriDiagMatrixSolver solver = new TriDiagMatrixSolver(n,k);
            var matrix = solver.ToMatrix();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }
            //Console.WriteLine("DiagDown:");
            //PrintVector(solver.DiagDown);

            //Console.WriteLine("DiagMain:");
            //PrintVector(solver.DiagMain);

            //Console.WriteLine("DiagUp:");
            //PrintVector(solver.DiagUp);

            //Console.WriteLine("Gorizontal1:");
            //PrintVector(solver.Gorizontal1);

            //Console.WriteLine("Gorizontal2:");
            //PrintVector(solver.Gorizontal2);

            //Console.WriteLine("VectorX:");
            //PrintVector(solver.VectorX);

            //Console.WriteLine("VectorF:");
            //PrintVector(solver.VectorF);
            Console.WriteLine();
            try
            {
                solver.SolveStep1();
                solver.SolveStep2();
                solver.SolveStep3();
                solver.SolveStep4();
                solver.SolveStep5();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }

 
            matrix = solver.ToMatrix();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{Math.Round(matrix[i, j],3)} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("VectorX:");
            PrintVector(solver.VectorX);
            Console.WriteLine();
            Console.WriteLine("VectorF:");
            PrintVector(solver.VectorF);
            Console.WriteLine();
            Console.WriteLine(solver.Pogr());
            Console.WriteLine();
            Console.WriteLine("VectorX1:");
            PrintVector(solver.VectorX1);
            Console.WriteLine();
            Console.WriteLine("VectorF1:");
            PrintVector(solver.VectorF1);
            Console.WriteLine();
            Console.WriteLine(solver.Pogr1());

            Console.ReadKey();
        }

        static void PrintVector(double[] vector)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                Console.Write($"{vector[i]} ");
            }
            Console.WriteLine();
        }
    }
}
