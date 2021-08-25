using Sudo.Net.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudo.Net.test
{
    public class TestModel3
    {
		public static void run()
		{
			Console.WriteLine("start");
			SudokuModel model = new SudokuModel();
			// first column
			model.setNumber(1, 2, 1);
			model.setNumber(1, 3, 2);
			model.setNumber(1, 5, 5);
			model.setNumber(1, 7, 3);
			
			// second column
			model.setNumber(2, 6, 7);
			model.setNumber(2, 7, 9);
			model.setNumber(2, 8, 1);
			
			// third column
			model.setNumber(3, 5, 8);
			model.setNumber(3, 9, 7);
			// fourth column
			model.setNumber(4, 1, 2);
			model.setNumber(4, 3, 6);
			model.setNumber(4, 6, 9);
			model.setNumber(4, 8, 5);
			// fifth column
			model.setNumber(5, 2, 4);
			model.setNumber(5, 8, 9);
			// sixth column
			model.setNumber(6, 2, 5);
			model.setNumber(6, 4, 3);
			model.setNumber(6, 7, 6);
			model.setNumber(6, 9, 4);
			// seventh column
			model.setNumber(7, 1, 6);
			model.setNumber(7, 5, 1);
			// eighth column
			model.setNumber(8, 2, 2);
			model.setNumber(8, 3, 7);
			model.setNumber(8, 4, 5);
			// nineth column
			model.setNumber(9, 3, 4);
			model.setNumber(9, 5, 7);
			model.setNumber(9, 7, 1);
			model.setNumber(9, 8, 6);

			model.solve();
			//		model.printModel();
		}
    }
}
