using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudoku_solver.other_classes;
using System.Diagnostics;

namespace sudoku_solver.sudoku_classes
{
    public class Sudoku
    {
        private int[,] grid= new int[9,9];
        private int[,] solution = new int[9, 9];

        public Sudoku(int[,] grid1)
        {
            Debug.Assert(CheckSize(grid1));
            this.grid = grid1;
        }

        public void Print()
        { 
            for(int i=0; i<9; i++)
            {
                Array_Ext.Print(Array_Ext.GetRow(grid, i));
            }
        }
        public void PrintSolution()
        {
            if (Solve())
            {
                for (int i = 0; i < 9; i++)
                {
                    Array_Ext.Print(Array_Ext.GetRow(solution, i));
                }
            }
            else
                Console.WriteLine("Cannot be solved");
        }
        private void PrintPartialSolution()
        {
            for (int i = 0; i < 9; i++)
            {
                Array_Ext.Print(Array_Ext.GetRow(solution, i));
            }
        }

        public static bool CheckSize(int[,] array)
        {
            if(array.GetLength(0) != 9 || array.GetLength(1)!= 9)
                return false;
            return true;
        }

        public bool CheckCorrectness()
        {
            for(int i=0; i<9; i++)
            {
                for(int j=0; j<9; j++)
                {
                    if ( ! CheckCorrectness(i, j))
                        return false;
                }
            }
            return true;
        }
        private bool CheckCorrectness(int row, int col)
        {
            Debug.Assert(row>=0 && row<9 && col>=0 && col<9);
            // if element == 0 then it is not filled
            if (grid[row, col] == 0)
                return true;
            // check in row
            for(int i=0; i<9; i++)
            {
                if (col != i && grid[row, col] == grid[row, i])
                    return false;
            }
            // check in col
            for (int i = 0; i < 9; i++)
            {
                if (row != i && grid[row, col] == grid[i, col])
                    return false;
            }
            //
            // check in 3x3 square
            //
            // square number:
            // (r,c)
            // (0,0) (0,1) (0,2)
            // (1,0) (1,1) (1,2) 
            // (2,0) (2,1) (2,2)
            //
            // r = floor(row/3), c = floor(col/3)
            //
            int r = (int) row / 3;
            int c = (int) col / 3;
            int elem_number = 0;
            int[] square = new int[9];
            for(int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    square[i*3 + j] = grid[r*3 + i, c*3 + j];
                    if (r * 3 + i == row && c * 3 + j == col)
                        elem_number = i * 3 + j;
                }
            }
            // check
            for(int i=0; i<square.Length; i++)
            {
                if (i != elem_number && square[i] == square[elem_number])
                    return false;
            }

            return true;
        }
        private bool CheckSolution()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!CheckSolution(i, j))
                        return false;
                }
            }
            return true;
        }
        private bool CheckSolution(int row, int col)
        {
            Debug.Assert(row >= 0 && row < 9 && col >= 0 && col < 9);
            // if element == 0 then it is not filled
            if (solution[row, col] == 0)
                return true;
            // check in row
            for (int i = 0; i < 9; i++)
            {
                if (col != i && solution[row, col] == solution[row, i])
                    return false;
            }
            // check in col
            for (int i = 0; i < 9; i++)
            {
                if (row != i && solution[row, col] == solution[i, col])
                    return false;
            }
            //
            // check in 3x3 square
            // same like in CheckCorrectness
            int r = (int)row / 3;
            int c = (int)col / 3;
            int elem_number = -1;
            int[] square = new int[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    square[i * 3 + j] = solution[r * 3 + i, c * 3 + j];
                    if (r * 3 + i == row && c * 3 + j == col)
                        elem_number = i * 3 + j;
                }
            }
            Debug.Assert(elem_number >= 0);
            // check
            for (int i = 0; i < square.Length; i++)
            {
                if (i != elem_number && square[i] == square[elem_number])
                    return false;
            }

            return true;
        }


        private int[] PreviousIndex(int row, int col)
        {
            if (col > 0)
                return new int[] { row, col - 1 };
            else if (row > 0)
                return new int[] { row - 1, 8 };
            else
                return new int[] { -1, -1 }; // there is no previous
        }
        private int[] NextIndex(int row, int col)
        {
            if (col < 8)
                return new int[] { row, col + 1 };
            else if (row < 8)
                return new int[] { row + 1, 0 };
            else
                return new int[] { -1, -1 }; // there is no next
        }

        // false - cannot be solved, true - solved correctly
        private bool Solve()
        {
            // check correctness
            if (!CheckCorrectness())
                return false;
            //int[,] solution = new int[9, 9];
            bool solved = false;
            int row = 0;
            int col = 0;
            // copy grid to solution
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    solution[i, j] = grid[i, j];

            while(solved == false)
            {
                if (grid[row, col] == 0)
                {
                    solution[row, col] += 1;
                    if(solution[row, col] > 9)
                    {
                        // not number can be assigned - cannot be solved or somwhere before was fault
                        solution[row, col] = 0;
                        // must find previous not solved element in grind
                        int[] previous = new int[2];
                        do
                        {
                            previous = PreviousIndex(row, col);
                            // if no previous element
                            if (previous[0] < 0)
                                return false;
                            Debug.Assert(previous[0] < 9 && previous[1] < 9);
                            if (col > 0)
                            {
                                Debug.Assert(previous[0] == row);
                                Debug.Assert(previous[1] + 1 == col);
                            }
                            else
                            {
                                Debug.Assert(previous[0] + 1 == row);
                                Debug.Assert(previous[1] == 8);
                            }
                            row = previous[0];
                            col = previous[1];

                        } while (grid[row, col] != 0); // while element is initial condition
                        Debug.Assert(grid[row, col] == 0);
                        continue;
                        
                    }
                }
                if (grid[row, col] != 0)
                    Debug.Assert(grid[row, col] == solution[row, col]);
                if (CheckSolution())
                {
                    // temporary subsolution may be correct
                    // if entire sudoku is solved, solution is found
                    if (row == 8 && col == 8)
                        solved = true;
                    else
                    {
                        // next element
                        int[] next = new int[2];
                        do
                        {
                            next = NextIndex(row, col);
                            if (col < 8)
                            {
                                Debug.Assert(next[0] == row);
                                Debug.Assert(next[1] - 1 == col);
                            }
                            else
                            {
                                Debug.Assert(next[0] - 1 == row);
                                Debug.Assert(next[1] == 0);
                            }
                            // if no next element
                            if (next[0] < 0)
                                return false;
                            Debug.Assert(next[0] < 9 && next[1] < 9);
                            row = next[0];
                            col = next[1];

                        } while (grid[row, col] != 0); // while element is initial condition
                        Debug.Assert(grid[row, col] == 0);
                    }
                }


                // endwhile
            }

            return solved;
        }



    }
}
