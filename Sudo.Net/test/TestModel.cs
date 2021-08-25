using Sudo.Net.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sudo.Net.test
{
    public class TestModel
    {
		public static void run()
		{
			Console.WriteLine("start");
			SudokuModel model = new SudokuModel();
			// first column
			// second column
			model.setNumber(2, 7, 8);
			//		/*
			model.setNumber(2, 8, 2);
			// third column
			model.setNumber(3, 1, 8);
			model.setNumber(3, 2, 7);
			model.setNumber(3, 9, 3);
			// fourth column
			model.setNumber(4, 6, 5);
			// fifth column
			model.setNumber(5, 2, 3);
			model.setNumber(5, 3, 5);
			model.setNumber(5, 8, 9);
			// sixth column
			model.setNumber(6, 3, 7);
			model.setNumber(6, 5, 6);
			model.setNumber(6, 6, 2);
			model.setNumber(6, 9, 4);
			// seventh column
			model.setNumber(7, 1, 1);
			model.setNumber(7, 4, 6);
			model.setNumber(7, 5, 7);
			// eighth column
			model.setNumber(8, 3, 6);
			model.setNumber(8, 4, 2);
			model.setNumber(8, 6, 1);
			model.setNumber(8, 8, 5);
			model.setNumber(8, 9, 8);
			// nineth column
			model.setNumber(9, 5, 4);
			model.setNumber(9, 6, 8);
			model.setNumber(9, 7, 3);
			model.setNumber(9, 8, 6);
			//		*/
			model.solve();
			//		model.printModel();
		}
    }
}
