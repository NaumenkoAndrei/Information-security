﻿using System.Text;

namespace Lab3
{
	public static class Hash
	{
		public static string GetHash(string inputString)
		{
			byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
			string hash = MD4.Hash(inputBytes);
			return hash;
		}

		public static string GetKeyHash(string hash)
		{
			string keyHash = "";

			for (int i = 0; i < hash.Length; i++)
			{
				if (i % 5 == 0) 
					keyHash += hash[i];
			}
			
			return keyHash;
		}
	}
}
