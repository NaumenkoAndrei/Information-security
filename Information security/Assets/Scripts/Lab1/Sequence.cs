using System;
using System.IO;
using Random = System.Random;

namespace Lab1
{
	public class Sequence
	{
		private Random _random = new Random();
		private int[] _sequence;
		private readonly int _sequenceSize;

		public Sequence(int sequenceSize)
		{
			_sequenceSize = sequenceSize;
			_sequence = new int[_sequenceSize];
		}

		public void Generate()
		{
			for (int i = 0; i < _sequenceSize; i++)
				_sequence[i] = _random.Next(2);
		}
		
		public void WriteSequenceToFile() => 
			File.WriteAllLines(Constants.FilePath, Array.ConvertAll(_sequence, x => x.ToString()));
	}
}
