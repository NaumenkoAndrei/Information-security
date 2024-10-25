using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using Random = System.Random;

namespace Lab2
{
	public class BBSGenerator
	{
		private static string FilePath = Path.Combine(Application.dataPath, "Sequence.txt");

		private readonly int _m;
		private readonly int[] _sequence;

		private int _q;
		private int _p;
		private int _n;
		private int _s = 1;

		public BBSGenerator(int m)
		{
			_m = m;
			_sequence = new int[_m];
		}

		public void Main()
		{
			GeneratePQ();
			CalculateN();
			GenerateS();
			GenerateSequence();
			WriteSequenceToFile();
		}

		private void GenerateSequence()
		{
			BigInteger u0 = ((BigInteger)_s * _s) % _n;
			BigInteger ui = (u0 * u0) % _n;
			_sequence[0] = (int)(ui & 1);
			
			for (int i = 1; i < _m; i++)
			{
				ui = (ui * ui) % _n;
				_sequence[i] = (int)(ui & 1);
			}
		}

		private void GenerateS()
		{
			Random random = new Random();
			List<int> candidates = new List<int>();

			for (int x = 1; x < _n; x += 2)
				if (GetNOD(_n, x) != 1)
					candidates.Add(x);

			int sIndex = random.Next(0, candidates.Count);

			_s = candidates[sIndex];
		}

		private long GetNOD(int a, int b)
		{
			while (b != 0)
			{
				int temp = b;
				b = a % b;
				a = temp;
			}

			return a;
		}

		private void CalculateN() => _n = _p * _q;

		private void GeneratePQ()
		{
			_q = PrimeNumbers.GetPrimeMod4Numbers();

			do
			{
				_p = PrimeNumbers.GetPrimeMod4Numbers();
			}
			while (_p == _q);
		}

		private void WriteSequenceToFile() => File.WriteAllLines(FilePath, Array.ConvertAll(_sequence, x => x.ToString()));
	}
}
