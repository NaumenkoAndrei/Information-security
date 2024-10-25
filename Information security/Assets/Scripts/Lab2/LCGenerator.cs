using System;
using System.IO;
using UnityEngine;
using Random = System.Random;

namespace Lab2
{
	public class LCGenerator
	{
		private static readonly string FilePath = Path.Combine(Application.dataPath, "Sequence.txt");

		private long _n;

		private const int A = 4096;
		private const int B = 150889;
		private const int M = 714025;
		private long _keyX0 = -1;

		private long[] _sequenceNumbers;
		private long[] _sequence;

		public LCGenerator(int n)
		{
			Initialization(n);
		}

		public LCGenerator(int n, long x)
		{
			Initialization(n);
			_keyX0 = x;
		}

		private void Initialization(int n)
		{
			_n = n;
			_sequence = new long[_n];
			_sequenceNumbers = new long[_n];
		}

		public void Main()
		{
			if (_keyX0 == -1)
			{
				Random random = new Random();
				_keyX0 = random.Next(1, 100000);
			}
			Task();
			GenerateSequence();
			WriteSequenceToFile();
		}

		private void GenerateSequence()
		{
			for (int i = 0; i < _n; i++)
			{
				_sequence[i] = (_sequenceNumbers[i] & 1);
			}
		}

		private void Task()
		{
			long xi = (A * _keyX0 + B) % M;
			_sequenceNumbers[0] = xi;
			
			for (int i = 1; i < _n; i++)
			{
				xi = (A * xi + B) % M;
				_sequenceNumbers[i] = xi;
			}
		}

		private void WriteSequenceToFile() => 
			File.WriteAllLines(FilePath, Array.ConvertAll(_sequence, x => x.ToString()));
	}
}
