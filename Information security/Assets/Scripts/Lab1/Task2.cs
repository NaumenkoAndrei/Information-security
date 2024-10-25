using System;
using System.IO;
using System.Linq;

namespace Lab1
{
	public class Task2
	{
		private int[] _sequence;
		private int _n;

		public double Task()
		{
			ReadSequenceFromFile();
			return CountingS();
		}

		private double CountingS()
		{
			int Vn = CountingVn();
			double p = _sequence.Sum() / (double)_n;
			double Sup = Math.Abs(Vn - 2 * _n * p * (1 - p));
			double Sdown = 2 * Math.Sqrt(2 * _n) * p * (1 - p);
			double S = Sup / Sdown;

			return S;
		}

		private int CountingVn()
		{
			int Vn = 1;

			for (int i = 0; i < (_n - 1); i++)
				if (_sequence[i] != _sequence[i + 1])
					Vn++;

			return Vn;
		}

		private void ReadSequenceFromFile()
		{
			string[] lines = File.ReadAllLines(Constants.FilePath);
			_n = lines.Length;
			_sequence = new int[_n];

			for (int i = 0; i < _n; i++)
				if (int.TryParse(lines[i], out int number))
					_sequence[i] = number;
		}
	}
}
