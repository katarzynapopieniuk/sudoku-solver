using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoku_solver.other_classes
{
    public static class Array_Ext
    {
        public static int[] GetRow(int[,] array, int row)
        {
            if (array == null)
                throw new ArgumentNullException("array is null");
            //int len = array.GetLength(1);
            int len = array.GetUpperBound(1) + 1;
            int[] result = new int[len];
            for(int i = 0; i < len; i++)
            {
                result[i] = array[row, i];
            }
            return result;
        }
        public static int[] GetCol(int[,] array, int col)
        {
            if (array == null)
                throw new ArgumentNullException("array is null");
            //int len = array.GetLength(1);
            int len = array.GetUpperBound(0) + 1;
            int[] result = new int[len];
            for (int i = 0; i < len; i++)
            {
                result[i] = array[i, col];
            }
            return result;
        }


        public static void Print(int[] row)
        {
            string result = "";
            foreach(int element in row)
            {
                result = result + element.ToString() + " ";
            }
            Console.WriteLine(result);
        }

    }
}
