using System;
using UnityEngine;
using UnityEngine.UI;

namespace Lab2
{
	public class Main : MonoBehaviour
	{
		[SerializeField] private Text _outputText;
		[SerializeField] private InputField _inputM;
		private int _m;

		public void RSAButton()
		{
			_outputText.text = "";
			try
			{
				_m = int.Parse(_inputM.text);
				
				RSAGenerator rsa = new RSAGenerator(_m);
				rsa.Main();
				
				_outputText.text = "Генерация RSA прошла успешно!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}
		
		public void BBSButton()
		{
			_outputText.text = "";
			try
			{
				_m = int.Parse(_inputM.text);
				
				BBSGenerator bbs = new BBSGenerator(_m);
				bbs.Main();
				
				_outputText.text = "Генерация BBS прошла успешно!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}
		
		public void LCButton()
		{
			_outputText.text = "";
			try
			{
				_m = int.Parse(_inputM.text);
				
				LCGenerator lcg = new LCGenerator(_m);
				lcg.Main();
				
				_outputText.text = "Генерация LC прошла успешно!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}
	}
}
