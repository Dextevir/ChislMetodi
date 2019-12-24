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
            //var solver = new EigenValuesSolver(10, 50000, Math.Pow(10, -5), Math.Pow(10, -5));
            //PrintMatrix(solver.Matrix);
            //Console.WriteLine();
            //PrintMatrix(solver.EigenVectors);
            //Console.WriteLine();
            //PrintVector(solver.EigenValues);
            //Console.WriteLine();
            //Console.WriteLine($"Lambda = {solver.OriginalValue}");
            //Console.WriteLine($"Lambda0 = {solver.FirstValue}");
            //Console.WriteLine();
            //PrintVector(solver.OriginalVector);
            //Console.WriteLine();
            //PrintVector(solver.FirstVector);

            //solver.Solve();

            //Console.WriteLine();
            //PrintVector(solver.FindedVector);
            //Console.WriteLine();
            //Console.WriteLine($"findLambda = {solver.FindedValue}");

            //Console.WriteLine($"itCount = {solver.IterationsCount}");
            //Console.WriteLine();
            //PrintVector(solver.Component());

            Test(50, 5000, Math.Pow(10, -8), 100);


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

        static void Test(int n, int maxIter, double epsilon, int countTests)
        {
            int[] iterationsCountVector = new int[countTests];
            double[] valuePogrVector = new double[countTests];
            double[] vectorPogrVector = new double[countTests];
            double[] tochnPogrVector = new double[countTests];
            for (int i=0;i<countTests; i++)
            {
                var solver = new EigenValuesSolver(n, maxIter, epsilon, epsilon);
                solver.Solve();
                if(solver.IterationsCount == maxIter)
                {
                    i--;
                }else
                {
                    iterationsCountVector[i] = solver.IterationsCount;
                    valuePogrVector[i] = solver.TochnostValue;
                    vectorPogrVector[i] = solver.TochnostVector;
                    tochnPogrVector[i] = solver.Pogresh;
                }
            }
            Console.WriteLine($"Average iterations count = {iterationsCountVector.Average()}");
            Console.WriteLine($"Average value accuracy  = {valuePogrVector.Average()}");
            Console.WriteLine($"Average vector accuracy  = {vectorPogrVector.Average()}");
            Console.WriteLine($"Average measure of accuracy  = {tochnPogrVector.Average()}");

        }


    }
}
