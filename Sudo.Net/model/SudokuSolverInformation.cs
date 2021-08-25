using System;
using System.Collections.Generic;
using System.Text;

namespace Sudo.Net.model
{
    class SudokuSolverInformation
    {
        public int row { get; set; }
        public int column { get; set;  }
        public int number { get; set; }

        public SudokuSolverInformation(int row, int column, int number)
        {
            this.row = row;
            this.column = column;
            this.number = number;
        }
    }
}
