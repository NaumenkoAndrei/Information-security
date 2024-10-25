using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = System.Random;

namespace Lab2
{
	public class PrimeNumbers
	{
		private static readonly string FilePath = Path.Combine(Application.dataPath, "Prime.txt");
		private const int MinPrime = 1000;
		private const int MaxPrime = 10000;

		private static bool IsPrime(int n)
		{
			if (n % 2 == 0 || n % 3 == 0)
				return false;

			for (int i = 5; i * i <= n; i += 6)
				if (n % i == 0 || n % (i + 2) == 0)
					return false;

			return true;
		}

		public static int GetPrimeNumber()
		{
			Random rand = new Random();
			List<int> primes = new List<int>();

			for (int number = MinPrime; number < MaxPrime + 1; number++)
				if (IsPrime(number))
					primes.Add(number);

			int primeIndex = rand.Next(0, primes.Count);

			return primes[primeIndex];
		}

		public static int GetPrimeMod4Numbers()
		{
			Random rand = new Random();
			List<int> primes = new List<int>();

			for (int number = MinPrime; number < MaxPrime + 1; number++)
				if (IsPrime(number) && number % 4 == 3)
					primes.Add(number);

			int primeIndex = rand.Next(0, primes.Count);

			return primes[primeIndex];
		}
	}
}
