using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MainMenu : MonoBehaviour
{
	public TextMeshProUGUI[] names = new TextMeshProUGUI[5];
	public TextMeshProUGUI[] scores = new TextMeshProUGUI[5];

	public void StartNew()
	{
		SceneManager.LoadScene(1);
	}

	public void Exit()
	{
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
		Application.Quit()
#endif
	}

	private void Start()
	{
		for (int i = 0; i < 5 ; i++)
		{
			names[i].SetText(DataManager.Instance.ScoreBoard[i].name);
			scores[i].SetText(DataManager.Instance.ScoreBoard[i].score.ToString());
		}
	}

}
