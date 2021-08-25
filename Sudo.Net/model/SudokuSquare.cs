using System;
using System.Collections.Generic;
using System.Text;

namespace Sudo.Net.model
{
    class SudokuSquare
    {
        private List<int> contents;
        private bool solvedFlag;

		public SudokuSquare()
		{
			this.contents = new List<int>(9);
			for (int i = 0; i != 9; ++i)
			{
				this.contents.Add(i + 1);
			}
			this.solvedFlag = false;
		}

		/**
		 * This funtion set all contents to 0
		 * except for the number to set (i)
		 * @param i the number to set in this square
		 */
		public void setNumber(int number)
		{
			for (int i = 0; i != contents.Count; ++i)
			{
				int element = contents[i];
				if ((element != number) && (element != 0))
				{
					this.contents[i] = 0;
				}
			}
			this.setFlaggedAsSolved();
			//assert this.getPossibilities() == 1;
		}

		/**
		 * @param number the number to scratch
		 * @return true when number is scratched, false otherwise
		 */
		public bool scratchNumber(int number)
		{
			for (int i = 0; i != contents.Count; ++i)
			{
				int element = contents[i];
				if (element == (number) && element != 0)
				{
					this.contents[i] = 0;
					return true;
				}
			}
			return false;
		}

		public bool isPossible(int number)
		{
			if (this.isSolved()) return false;
			for (int i = 0; i != contents.Count; ++i)
			{
				int element = contents[i];
				if (element == (number))
				{
					return true;
				}
			}
			return false;
		}

		/**
		 * Returns the amount of possibilities (values != 0)
		 * @return
		 */
		public int getPossibilities()
		{
			int possibilities = 0;
			foreach (var iter in contents)
			{				
				if (iter != 0)
				{
					++possibilities;
				}
			}
			return possibilities;
		}

		/**
		 * The square is solved when the possibilities are narrow down to 1
		 * @return true if possibilities is equal to 1
		 * @return false otherwise
		 */
		public bool isSolved()
		{
			return (this.getPossibilities() == 1);
		}

		public bool isFlaggedAsSolved()
		{
			return this.solvedFlag;
		}

		public void setFlaggedAsSolved()
		{
			this.solvedFlag = true;
		}

		/**
		 * Returns the first non-zero value
		 * Pre-condition: isSolved must be true
		 * @return the first non-zero value
		 * @return -1 when all value are zero (error)
		 */
		public int getValue()
		{
			foreach (var iter in contents)
			{				
				if (iter != 0)
				{
					return iter;
				}
			}
			return -1;
		}

		public List<int> getAllPossibilities()
		{
			List<int> v = new List<int>();
			foreach (var iter in contents)
			{				
				if (iter != 0) v.Add(iter);
			}
			return v;
		}

		public String getPrintPossibilities()
		{
			String s = "[";
			for (int i = 0; i != this.contents.Count; ++i)
			{
				if ((this.contents[i] == 0)
					|| (this.isSolved())) s += ".";
				else s += (i + 1);
			}
			s += "]";
			return s;
		}
	}
}
