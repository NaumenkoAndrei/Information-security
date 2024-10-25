using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
	private const int MainScene = 0;
	private const int Lab1Scene = 1;
	private const int Lab2Scene = 2;
	private const int Lab3Scene = 3;
	private const int Lab4Scene = 4;
	private const int Lab5Scene = 5;

	public void TransitionMainScene() => 
		SceneManager.LoadScene(MainScene);

	public void TransitionLab1Scene() => 
		SceneManager.LoadScene(Lab1Scene);

	public void TransitionLab2Scene() => 
		SceneManager.LoadScene(Lab2Scene);

	public void TransitionLab3Scene() => 
		SceneManager.LoadScene(Lab3Scene);

	public void TransitionLab4Scene() => 
		SceneManager.LoadScene(Lab4Scene);

	public void TransitionLab5Scene() => 
		SceneManager.LoadScene(Lab5Scene);
}
