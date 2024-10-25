using System;
using System.IO;
using System.Linq;

namespace Lab1
{
	public class Task1
	{
		private int[] _sequence;
		private int[] _xi;
		private int _n;

		public double Task()
		{
			ReadSequenceFromFile();
			SequenceTransformation();
			return CountingS();
		}

		private double CountingS()
		{
			double xsum = _xi.Sum();

			double S = Math.Abs(xsum) / Math.Sqrt(_n);
			return S;
		}

		private void SequenceTransformation()
		{
			for (int i = 0; i < _n; i++)
				_xi[i] =(_sequence[i] * 2 - 1);
		}

		private void ReadSequenceFromFile()
		{
			string[] lines = File.ReadAllLines(Constants.FilePath);
			_n = lines.Length;
			_sequence = new int[_n];
			_xi = new int[_n];

			for (int i = 0; i < _n; i++)
				if (int.TryParse(lines[i], out int number))
					_sequence[i] = number;
		}
	}
}
