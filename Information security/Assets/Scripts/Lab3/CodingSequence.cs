using Lab2;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Lab3
{
	public class CodingSequence
	{
		private static readonly string XorPath = Path.Combine(Application.dataPath, "Sequence.txt");
		
		private static readonly string ForCodingPath = Path.Combine(Application.dataPath, "ForCoding.txt");
		private static readonly string DecodingPath = Path.Combine(Application.dataPath, "Decoding.txt");
		private static readonly string CodingPath = Path.Combine(Application.dataPath, "Coding.txt");
		
		public int LengthXor;
		private int[] _xor;

		public void Coding()
		{
			ReadXor();
			
			string coding = ReadFile(ForCodingPath);
			int[] codingAsciiBinary = StringToAsciiBinary(coding);
			
			int[] resultAsciiBinary = OperationXOR(codingAsciiBinary, _xor);
			string resultCodingString = AsciiBinaryToString(resultAsciiBinary);
			
			WriteStringToFile(resultCodingString, CodingPath);
		}

		public void Decoding()
		{
			ReadXor();
			
			string decoding = ReadFile(CodingPath);
			int[] decodingAsciiBinary = StringToAsciiBinary(decoding);
			
			int[] resultAsciiBinary = OperationXOR(decodingAsciiBinary, _xor);
			string resultDecodingString = AsciiBinaryToString(resultAsciiBinary);
			
			WriteStringToFile(resultDecodingString, DecodingPath);
		}
		
		// Метод для преобразования строки в массив ASCII
		public static int[] StringToAsciiBinary(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return Array.Empty<int>();
			}

			int[] asciiBinary = new int[input.Length];

			for (int i = 0; i < input.Length; i++)
			{
				// Преобразуем каждый символ в его ASCII-код и сохраняем в массив
				asciiBinary[i] = (int)input[i];
			}

			return asciiBinary;
		}
		
			// Метод для преобразования массива ASCII в строку
			public static string AsciiBinaryToString(int[] asciiBinary)
			{
				if (asciiBinary == null || asciiBinary.Length == 0)
				{
					return string.Empty;
				}

				char[] chars = new char[asciiBinary.Length];

				for (int i = 0; i < asciiBinary.Length; i++)
				{
					// Преобразуем каждый ASCII-код обратно в символ
					chars[i] = (char)asciiBinary[i];
				}

				return new string(chars);
			}

		private static string ReadFile(string filePath)
		{
			return File.ReadAllText(filePath);
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

		private void ReadXor()
		{
			string[] lines = File.ReadAllLines(XorPath);
			LengthXor = lines.Length;
			_xor = new int[LengthXor];

			for (int i = 0; i < LengthXor; i++)
				if (int.TryParse(lines[i], out int number))
					_xor[i] = number;
		}
		
		// Метод для записи строки в файл
		static void WriteStringToFile(string content, string filePath)
		{
			// Используем StreamWriter для записи в файл
			using (StreamWriter writer = new StreamWriter(filePath, append: false))
			{
				writer.WriteLine(content); // Записываем строку в файл
			}
		}
	}
}
