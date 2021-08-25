using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudo.Net.model
{
    class SudokuSolver
    {
		private int n;
		private List<SudokuSolverInformation> amounts;

		public SudokuSolver(int n)
		{
			this.n = n;
			amounts = new List<SudokuSolverInformation>();
		}

		public void addPossibilities(int row, int col, List<int> newPossibilities)
		{
			// get current possibilities in submatrix
			foreach (var iter in newPossibilities)
			{				
				if (iter == this.n)
				{
					amounts.Add(new SudokuSolverInformation(row, col, n));
				}
			}
		}

		public SudokuSolverInformation getSolution()
		{
			if (amounts.Count == 1) return amounts.FirstOrDefault();
			else return null;
		}

		public int getSolutions()
		{
			return amounts.Count;
		}

		public String getSolutionString()
		{
			string s = "";
			foreach (SudokuSolverInformation name in amounts)
			{
				s += name.number + "[" + name.column + "," + name.row + "] ";
			}
			return s;
		}
	}
}
