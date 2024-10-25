using Lab2;
using System;
using System.IO;
using UnityEngine;

namespace Lab3
{
	public class CodingSequence
	{
		private static readonly string XorSequencePath = Path.Combine(Application.dataPath, "Sequence.txt");
		
		private static readonly string SequenceForCodingPath = Path.Combine(Application.dataPath, "SequenceForEncoding.txt");
		private static readonly string DecodingSequencePath = Path.Combine(Application.dataPath, "DecodedSequence.txt");
		private static readonly string CodingSequencePath = Path.Combine(Application.dataPath, "CodingSequence.txt");

		public int LengthSequenceForCoding;
		public int LengthSequence;
		public int LengthCodingSequence;
		
		private int[] _sequenceForCoding;
		private int[] _XorSequence;
		private int[] _decodingSequence;
		private int[] _codingSequence;

		public void Coding()
		{
			_sequenceForCoding = ReadSequenceFromFile(out LengthSequenceForCoding, SequenceForCodingPath);
			_XorSequence = ReadSequenceFromFile(out LengthSequence, XorSequencePath);
			
			_codingSequence = OperationXOR(_sequenceForCoding, _XorSequence);
			WriteSequenceToFile(CodingSequencePath, _codingSequence);
		}

		public void Decoding()
		{
			_codingSequence = ReadSequenceFromFile(out LengthCodingSequence, CodingSequencePath);
			_XorSequence = ReadSequenceFromFile(out LengthSequence, XorSequencePath);
			
			_decodingSequence = OperationXOR(_codingSequence, _XorSequence);
			WriteSequenceToFile(DecodingSequencePath, _decodingSequence);
		}
		
		private int[] OperationXOR(int[] sequenceOne, int[] sequenceTwo)
		{
			int[] resultSequence = new int[sequenceOne.Length];
			int length = Math.Min(sequenceOne.Length, sequenceTwo.Length);
			
			for (int i = 0; i < length; i++)
			{
				resultSequence[i] = sequenceOne[i] ^ sequenceTwo[i];
			}
			
			return resultSequence;
		}

		private int[] ReadSequenceFromFile(out int lengthFile, string sequencePath)
		{
			string[] lines = File.ReadAllLines(sequencePath);
			lengthFile = lines.Length;
			int[] sequence = new int[lengthFile];

			for (int i = 0; i < lengthFile; i++)
				if (int.TryParse(lines[i], out int number))
					sequence[i] = number;
			
			return sequence;
		}
		
		private void WriteSequenceToFile(string filePath, int[] sequence) => 
			File.WriteAllLines(filePath, Array.ConvertAll(sequence, x => x.ToString()));
	}
}
