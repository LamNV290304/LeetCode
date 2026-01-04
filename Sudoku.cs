using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    internal class Sudoku
    {
        public bool IsValidSudoku(char[][] board)
        {
            HashSet<string> seen = new HashSet<string>();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    char number = board[i][j];
                    if (number != '.')
                    {
                        string rowKey = $"row{i}-{number}";
                        string colKey = $"col{j}-{number}";
                        string boxKey = $"box{i / 3}-{j / 3}-{number}";
                        if (!seen.Add(rowKey) || !seen.Add(colKey) || !seen.Add(boxKey))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;

        }
    }
}
