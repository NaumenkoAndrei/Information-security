using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using Random = System.Random;

namespace Lab2
{
	public class RSAGenerator
	{
		private static readonly string FilePath = Path.Combine(Application.dataPath, "Sequence.txt");

		private readonly long _m;
		private int _p;
		private int _q;

		private long _n;
		private long _fN;
		private long _k;
		private long _u0;
		private int[] _sequence;

		public RSAGenerator(int m)
		{
			_m = m;
			_sequence = new int[_m];
		}

		public void Main()
		{
			GeneratePQ();
			CalculateNfN();
			FiendK();
			FiendU0();
			GenerateSequence();
			WriteSequenceToFile();
		}

		private void WriteSequenceToFile() => 
			File.WriteAllLines(FilePath, Array.ConvertAll(_sequence, x => x.ToString()));

		private void GenerateSequence()
		{
			BigInteger ui = BigInteger.ModPow(value: _u0, exponent: _k, modulus: _n);

			_sequence[0] = (int)(ui & 1);

			for (int i = 1; i < _m; i++)
			{
				ui = BigInteger.ModPow(value: ui, exponent: _k, modulus: _n);
				_sequence[i] = (int)(ui & 1);
			}
		}

		private void FiendU0()
		{
			Random random = new Random();
			long min = 1L;
			long max = _n - 1;
			long range = max - min;
			_u0 = (long)(random.NextDouble() * range) + min;
		}

		private void FiendK()
		{
			Random random = new Random();
			List<long> candidates = new List<long>();

			for (long x = 2; x < _fN; x++)
				if (GetNOD(_fN, x) == 1)
					candidates.Add(x);

			int randomIndex = random.Next(0, candidates.Count);
			_k = candidates[randomIndex];
		}

		private BigInteger GetNOD(BigInteger a, long b)
		{
			while (b != 0)
			{
				long temp = b;
				b = (long)(a % b);
				a = temp;
			}

			return a;
		}

		private void CalculateNfN()
		{
			_n = (long)_p * _q;
			_fN = (long)(_p - 1) * (_q - 1);
		}

		private void GeneratePQ()
		{
			_q = PrimeNumbers.GetPrimeNumber();

			do {
				_p = PrimeNumbers.GetPrimeNumber();
			} while (_p == _q);
		}
	}
}
