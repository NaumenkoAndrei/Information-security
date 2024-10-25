using Lab2;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lab3
{
	public class Main : MonoBehaviour
	{
		[SerializeField] private Text _outputText;
		[SerializeField] private InputField _inputString;
		private readonly CodingSequence _codingSequence = new CodingSequence();
		private long _keyLcg;

		public void GenerateHashButton()
		{
			try
			{
				string hash = Hash.GetHash(_inputString.text);
				string keyHash = Hash.GetKeyHash(hash);
				_keyLcg = Convert.ToInt64(keyHash, 16);
				
				LCGenerator lcg = new LCGenerator(100000, _keyLcg);
				lcg.Main();

				_outputText.text = $"Хеш: ({hash})\n";
				_outputText.text += $"Шестнадцатиричный ключ: ({keyHash})\n";
				_outputText.text += $"Десятичный ключ: ({_keyLcg})\n";
				_outputText.text += "Генерация файла для кодирования прошла успешно!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}
		
		public void CodingButton()
		{
			try
			{
				_codingSequence.Coding();
				_outputText.text = "Кодирование прошло успешно!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}

		public void DecodingButton()
		{
			try
			{
				_codingSequence.Decoding();
				_outputText.text = "Декодирование прошло успешно!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}
	}
}
