using System;
using System.IO;

namespace Lab1
{
	public class Task3
	{
		private int[] _sequence;
		private int[] _xi;
		private int[] _sn;
		private int _n;
		private int[] _sHatch;
		private int[] _j = new int[]{ -9, -8, -7, -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		private int[] _eJ = new int[18];
		private int _l = 0;
		private double[] _yJ = new double[18];

		public bool Task()
		{
			ReadSequenceFromFile();
			SequenceTransformation();
			CountingSn();
			CountingSHatch();
			CountingL();
			CountingE();
			CountingYj();

			return Result();
		}

		public string ResultY()
		{
			string result = "";

			for (int i = 0; i < _yJ.Length; i++)
			{
				result += $"Y{i}={_yJ[i]}\t";

				if (i % 2 == 0)
					result += "\t\t";
				else
					result += "\n";
			}

			return result;
		}

		private bool Result()
		{
			bool result = true;

			for (int i = 0; i < _yJ.Length; i++)
				if (_yJ[i] > Constants.S)
					result = false;

			return result;
		}

		private void CountingYj()
		{
			for (int i = 0; i < _yJ.Length; i++)
			{
				int yTop = Math.Abs(_eJ[i] - _l);
				double yDown = Math.Sqrt(2 * _l * (4 * Math.Abs(_j[i]) - 2));

				_yJ[i] = yTop / yDown;
			}
		}

		private void CountingE()
		{
			foreach (int s in _sHatch)
				for (int j = 0; j < _j.Length; j++)
					if (s == _j[j])
						_eJ[j]++;
		}

		private void CountingL()
		{
			for (int i = 0; i < _sHatch.Length; i++)
				if (_sHatch[i] == 0)
					_l++;
		}

		private void CountingSHatch()
		{
			_sHatch[0] = 0;
			_sHatch[_n + 1] = 0;

			for (int i = 1; i < _n + 1; i++)
				_sHatch[i] = _sn[i - 1];
		}

		private void CountingSn()
		{
			for (int i = 0; i < _n; i++)
				for (int j = 0; j <= i; j++)
					_sn[i] += _sequence[j];
		}

		private void SequenceTransformation()
		{
			for (int i = 0; i < _n; i++)
				_xi[i] = (_sequence[i] * 2 - 1);
		}

		private void ReadSequenceFromFile()
		{
			string[] lines = File.ReadAllLines(Constants.FilePath);
			_n = lines.Length;
			_sequence = new int[_n];
			_xi = new int[_n];
			_sn = new int[_n];
			_sHatch = new int[_n + 2];

			for (int i = 0; i < _n; i++)
				if (int.TryParse(lines[i], out int number))
					_sequence[i] = number;
		}
	}
}
