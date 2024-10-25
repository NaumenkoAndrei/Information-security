using UnityEngine;
using UnityEngine.UI;
using System;

namespace Lab1
{
	public class Main : MonoBehaviour
	{
		[SerializeField] private Text _outputText;
		[SerializeField] private InputField _inputField;

		private int _sequenceSize;

		private void SaveSequenceSize() => 
			_sequenceSize = int.Parse(_inputField.text);

		public void GenerateButton()
		{
			try
			{
				SaveSequenceSize();
				SaveSequenceSize();
				Sequence sequence = new Sequence(_sequenceSize);
				sequence.Generate();
				sequence.WriteSequenceToFile();
				_outputText.text = "Генерация прошла успешно!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}

		public void Task1Button()
		{
			try
			{
				Task1 task1 = new Task1();
				double sTask = task1.Task();

				_outputText.text = "S = " + sTask + "\n";
				_outputText.text += (sTask <= Constants.S) ? "Тест пройден успешно!" : "Последовательность не случайна!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}

		public void Task2Button()
		{
			try
			{
				Task2 task2 = new Task2();
				double sTask = task2.Task();

				_outputText.text = "S = " + sTask + "\n";

				_outputText.text += (sTask <= Constants.S) ? "Тест пройден успешно!" : "Последовательность не случайна!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}

		public void Task3Button()
		{
			try
			{
				Task3 task3 = new Task3();
				bool resultTask3 = task3.Task();

				_outputText.text = task3.ResultY() + "\n";
				_outputText.text += resultTask3 ? "Тест пройден успешно!" : "Последовательность не случайна!";
			}
			catch (Exception ex)
			{
				_outputText.text = "Произошла ошибка\n" + ex.ToString();
			}
		}
	}
}
