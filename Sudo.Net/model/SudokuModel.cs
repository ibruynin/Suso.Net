using System;
using System.Collections.Generic;
using System.Text;

namespace Sudo.Net.model
{
    class SudokuModel
    {
		/// first index = column, second index = row
		private SudokuSquare[][] model;

		public SudokuModel()
		{
			// init model with list 1..9 (possible values)
			model = new SudokuSquare[][] {
				new SudokuSquare[9], // 1
				new SudokuSquare[9],
				new SudokuSquare[9],
				new SudokuSquare[9],
				new SudokuSquare[9], // 5
				new SudokuSquare[9],
				new SudokuSquare[9],
				new SudokuSquare[9],
				new SudokuSquare[9]  // 9
			};

			for (int row = 0; row != model.Length; ++row)
			{
				for (int col = 0; col < model[row].Length; col++)
				{
					model[row][col] = new SudokuSquare();
				}
			}
		}

		public void setNumber(int x, int y, int number)
		{
			int col = x - 1;
			int row = y - 1;
			//		System.out.println("setting number " + number + "@" + x + "," + y);
			// set only value in model
			model[row][col].setNumber(number);

			// sudoku tactics here
			// 1. check vertical numbers
			scratchVer(x, y, number);
			// 2. check horizontal numbers
			scratchHor(x, y, number);
			// 3. check numbers in same 3x3 matrix
			scratchMatrix(x, y, number);
		}

		public void solve()
		{
			bool running = true;
			while (running)
			{
				running = this.solveImpl();
				this.printModel();
			}
		}

		/**
		 * This function implements the different tactics
		 * - 1) all submatrices will be checked for solved squares
		 * - 2) all rows will be checked for single solutions
		 * - 3) all columns will be checked for single solutions
		 * - 4) all search submatrices for non-unique solutions unique per row
		 * - 5) all search submatrices for non-unique solutions unique per column
		 * 
		 * @return true when a tactic found a solution
		 * @return false when all tactics failed to solve a cell
		 */
		private bool solveImpl()
		{
            Console.WriteLine("**********************************");
			if (searchSubMatrices()) return true;
			else if (searchHorizontal()) return true;
			else if (searchVertical()) return true;
			else if (searchVerticalSquare()) return true;
			else if (searchHorizontalSquare()) return true;
			else if (searchAllSquares()) return true;
			else return false;
		}



		/**
		 * This function searches for solutions in the 3x3 submatrices
		 * Per submatrix, all numbers 1..9 will be evaluated
		 * All possibilities will be added in the SSMS object
		 * @return
		 */
		private bool searchSubMatrices()
		{
			bool found = false;
			// submatrix 1..3 (rows)
			for (int m_row = 0; m_row < 3; m_row++)
			{
				// submatrix 1..3 (cols)
				for (int m_col = 0; m_col < 3; m_col++)
				{
					// checking for 1..9 at each 3x3 matrix available at [row,col]
					for (int n = 1; n <= 9; ++n)
					{
						SudokuSolver ssms = new SudokuSolver(n);
						for (int row = 0; row != 3; ++row)
						{
							for (int col = 0; col != 3; ++col)
							{
								ssms.addPossibilities(
										row + (3 * m_row),
										col + (3 * m_col),
										this.model[row + (3 * m_row)][col + (3 * m_col)].getAllPossibilities());
							}
						}
						SudokuSolverInformation info = ssms.getSolution();
						if (info != null)
						{
							if (!this.model[info.row][info.column].isSolved())
							{
								//							System.out.println("SM. FOUND NUMBER " + info.number + " at " + (info.column+1) + "," + (info.row+1));
								this.setNumber(info.column + 1, info.row + 1, info.number);
								found = true;
							}
						}
					}
				}
			}
			return found;
		}

		private bool searchHorizontal()
		{
			bool found = false;
			for (int row = 0; row < model.Length; row++)
			{
				// checking for 1..9 at each horizontal line at [row][1..9]
				for (int n = 1; n <= 9; ++n)
				{
					SudokuSolver ssms = new SudokuSolver(n);
					for (int col = 0; col != model[row].Length; ++col)
					{
						ssms.addPossibilities(row, col, this.model[row][col].getAllPossibilities());
					}
					SudokuSolverInformation info = ssms.getSolution();
					if (info != null)
					{
						if (!this.model[info.row][info.column].isSolved())
						{
							found = true;
							//						System.out.println("SH. FOUND NUMBER " + info.number + " at " + (info.column+1) + "," + (info.row+1));
							this.setNumber(info.column + 1, info.row + 1, info.number);
						}
					}
				}
			}
			return found;
		}

		/**
		 * 
		 */
		private bool searchVertical()
		{
			bool found = false;
			for (int col = 0; col < model[0].Length; col++)
			{
				// checking for 1..9 at each vertical line at [0..8,col]
				for (int n = 1; n <= 9; ++n)
				{
					SudokuSolver ssms = new SudokuSolver(n);
					for (int row = 0; row != model.Length; ++row)
					{
						ssms.addPossibilities(row, col, this.model[row][col].getAllPossibilities());
					}
					SudokuSolverInformation info = ssms.getSolution();
					if (info != null)
					{
						if (!this.model[info.row][info.column].isSolved())
						{
							found = true;
							//						System.out.println("SV. FOUND NUMBER " + info.number + " at " + (info.column+1) + "," + (info.row+1));
							this.setNumber(info.column + 1, info.row + 1, info.number);
						}
					}
				}
			}
			return found;
		}

		/**
		 * Searches for unique vertical solutions in a single square
		 * This means that the rest of the row may be scratched
		 * @return
		 */
		private bool searchVerticalSquare()
		{
			bool found = false;
			// checking for 1..9 in each 3x3 matrix
			// submatrix 1..3 (rows)
			for (int m_row = 0; m_row < 3; m_row++)
			{
				// submatrix 1..3 (cols)
				for (int m_col = 0; m_col < 3; m_col++)
				{
					// checking for 1..9 at each 3x3 matrix available at [row,col]
					for (int n = 1; n <= 9; ++n)
					{
						int[] cnt = new int[3];
						// loop over each row in submatrix
						for (int r = 0; r < 3; ++r)
						{
							for (int c = 0; c < 3; ++c)
							{
								//							System.out.print(this.model[m_row*3 + r][m_col*3 + c].getValue() + " ");
								if (this.model[m_row * 3 + r][m_col * 3 + c].isPossible(n)) cnt[r]++;
							}
							//						System.out.print(";");
						}
						int sum = cnt[0] + cnt[1] + cnt[2];
						if (sum != 0 && ((cnt[0] == sum) || (cnt[1] == sum) || (cnt[2] == sum)))
						{
							//						System.out.println("VS. checked: " + n + "[" + (m_row+1) + "," + (m_col+1) +"]" + ":" + cnt[0] + " " + cnt[1] + " " + cnt[2]);
							for (int xx = 0; xx < 3; ++xx)
							{
								if (cnt[xx] != 0)
								{
									//								System.out.println("VS.[" + xx + "] removing " + n + " in rows " + ((xx+1) + (3 * m_row)) );
									found |= scratchRow(xx + (3 * m_row), m_col, n);
								}
							}
						}

					}
				}
			}
			return found;
		}

		private bool searchHorizontalSquare()
		{
			bool found = false;
			// checking for 1..9 in each 3x3 matrix
			// submatrix 1..3 (rows)
			for (int m_row = 0; m_row < 3; m_row++)
			{
				// submatrix 1..3 (cols)
				for (int m_col = 0; m_col < 3; m_col++)
				{
					// TODO here
				}
			}
			return found;
		}

		/**
		 * search all squares with 1 solution but without solved flag
		 * setNumber will trigger the solved flag
		 * @return true when such squares are found
		 */
		private bool searchAllSquares()
		{
			bool found = false;
			for (int m_row = 0; m_row < 9; m_row++)
			{
				for (int m_col = 0; m_col < 9; m_col++)
				{
					if (this.model[m_row][m_col].isSolved() && !this.model[m_row][m_col].isFlaggedAsSolved())
					{
						//					System.out.println("AS. FOUND NUMBER " + this.model[m_row][m_col].getValue() + " at " + (m_row+1) + "," + (m_col+1));
						this.setNumber(m_col + 1, m_row + 1, this.model[m_row][m_col].getValue());
						found = true;
					}
				}
			}
			return found;
		}

		/**
		 * Scratches number vertically (i.e. in each row) for column x
		 * @param x the column identifier
		 * @param y the row identifier
		 * @param number the number to scratch
		 */
		private void scratchVer(int x, int y, int number)
		{
			//		System.out.println("ScratchVer@" + x + "," + y + " for " + number);
			int col = x - 1;
			int row = y - 1;
			for (int i_row = 0; i_row != model.Length; ++i_row)
			{
				if (row != i_row)
				{
					bool scratched = model[i_row][col].scratchNumber(number);
					//				if (scratched) System.out.println("scratched " + number + " at " + (i_row+1) + "," + (col+1));
				}
			}
		}

		private bool scratchRow(int row, int avoid, int number)
		{
			bool found = false;
			for (int i = 0; i < 3; ++i)
			{
				for (int j = 0; j < 3; ++j)
				{
					if (j != avoid)
					{
						int col = i + 3 * j;
						if (!this.model[row][col].isSolved())
						{
							if (this.model[row][col].isPossible(number))
							{
                                Console.WriteLine("scratching " + number + " at " + row + "," + col + " avoiding " + avoid);
								this.model[row][col].scratchNumber(number);
								found = true;
							}
						}
					}
				}
			}
			//		this.printModel();
			return found;
		}

		/**
		 * Scratches number horizontally (i.e. in each column) for row y
		 * @param x
		 * @param y
		 * @param number
		 */
		private void scratchHor(int x, int y, int number)
		{
			//		System.out.println("ScratchHor@" + x + "," + y + " for " + number);
			int col = x - 1;
			int row = y - 1;
			for (int i_col = 0; i_col != model[row].Length; ++i_col)
			{
				if (col != i_col)
				{
					bool scratched = model[row][i_col].scratchNumber(number);
					//				if (scratched) System.out.println("scratched " + number + " at " + (row+1) + "," + (i_col+1));
				}
			}
		}

		private void scratchMatrix(int x, int y, int number)
		{
			//		System.out.println("ScratchMat@" + x + "," + y + " for " + number);
			int a_col = x - 1;
			int a_row = y - 1;
			int m_row = (int)a_row / 3;
			int m_col = (int)a_col / 3;
			for (int row = 0; row != 3; ++row)
			{
				for (int col = 0; col != 3; ++col)
				{
					if ((!model[row + (3 * m_row)][col + (3 * m_col)].isSolved()))
					{
						bool scratched = model[row + (3 * m_row)][col + (3 * m_col)].scratchNumber(number);
					}
				}
			}
		}

		public void printModel()
		{
            Console.WriteLine("==== Possibilities ====");
            Console.WriteLine("|----------------------|");
			for (int row = 0; row < model.Length; row++)
			{
                Console.Write("|");
				for (int col = 0; col < model[row].Length; col++)
				{
					int choices = model[row][col].getPossibilities();
                    Console.Write(choices + ((col + 1) % 3 == 0 ? " | " : " "));
				}
                Console.WriteLine((row + 1) % 3 == 0 ? "\n|----------------------|" : "");
			}
            Console.WriteLine("==== Known  values ====");
            Console.WriteLine("|----------------------|");
			for (int row = 0; row != model.Length; ++row)
			{
                Console.Write("|");
				for (int col = 0; col < model[row].Length; ++col)
				{
					if (model[row][col].isSolved() == true)
                    {
                        Console.Write(model[row][col].getValue() + " ");
                    }
					else
                    {
                        Console.Write(((model[row][col].getPossibilities() < 1 ? "x" : ".")) + ((col + 1) % 3 == 0 ? " | " : " "));
                    }
				}
                Console.WriteLine((row + 1) % 3 == 0 ? "\n|----------------------|" : "");
			}
            Console.WriteLine("==== Possible values ====");
            Console.WriteLine("|-----------------------------------------------------------------------------------------------------------|");
			for (int row = 0; row != model.Length; ++row)
			{
                Console.Write("| ");
				for (int col = 0; col < model[row].Length; ++col)
				{
                    Console.Write(model[row][col].getPrintPossibilities());
                    if ((col + 1) % 3 == 0) Console.Write(" | ");
				}
                Console.WriteLine((row + 1) % 3 == 0 ? "\n|-----------------------------------------------------------------------------------------------------------|" : "");
			}
		}
	}
}
