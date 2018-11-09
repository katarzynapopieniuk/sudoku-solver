using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudoku_solver.sudoku_classes;

namespace sudoku_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] problem = new int[,] { { 5, 1, 7, 6, 0, 0, 0, 3, 4 },{ 2, 8, 9, 0, 0, 4, 0, 0, 0 },{ 3, 4, 6, 2, 0, 5, 0, 9, 0 },{ 6, 0, 2, 0, 0, 0, 0, 1, 0 },{ 0, 3, 8, 0, 0, 6, 0, 4, 7 },{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },{ 0, 9, 0, 0, 0, 0, 0, 7, 8 },{ 7, 0, 3, 4, 0, 0, 5, 6, 0 },{ 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            int[,] invalid = new int[,] { { 5, 3, 4, 6, 7, 8, 9, 1, 2 }, { 6, 7, 2, 1, 9, 5, 3, 4, 8 }, { 1, 9, 8, 3, 8, 2, 5, 6, 7 }, { 8, 5, 9, 7, 6, 1, 4, 2, 3 }, { 4, 2, 6, 8, 5, 3, 7, 9, 1 }, { 7, 1, 3, 9, 2, 4, 8, 5, 6 }, { 9, 6, 1, 5, 3, 7, 2, 8, 4 }, { 2, 8, 7, 4, 1, 9, 6, 3, 5 }, { 3, 4, 5, 2, 0, 6, 1, 7, 9 } };
            int[,] hardsudoku = new int[,] { { 1, 0, 0, 0, 0, 7, 0, 9, 0 }, { 0, 3, 0, 0, 2, 0, 0, 0, 8 }, { 0, 0, 9, 6, 0, 0, 5, 0, 0 }, { 0, 0, 5, 3, 0, 0, 9, 0, 0 }, { 0, 1, 0, 0, 8, 0, 0, 0, 2 }, { 6, 0, 0, 0, 0, 4, 0, 0, 0 }, { 3, 0, 0, 0, 0, 0, 0, 1, 0 }, { 0, 4, 0, 0, 0, 0, 0, 0, 7 }, { 0, 0, 7, 0, 0, 0, 3, 0, 0 } };

            Sudoku sudoku = new Sudoku(hardsudoku);
            Console.WriteLine("Original: \n");
            sudoku.Print();
            Console.WriteLine("\nSolved: \n");
            sudoku.PrintSolution();

            Console.ReadKey();
        }
    }
}
