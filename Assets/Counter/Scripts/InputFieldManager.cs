using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InputFieldManager : MonoBehaviour
{
	public Button submitButton;

	private GameManager gameManager;
	private EventSystem eventSystem;
	private TMP_InputField inputField;

	// Start is called before the first frame update
	void Start()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		inputField = gameObject.GetComponent<TMP_InputField>();
		if(!inputField)
		{
			Debug.Log("Input Field not found");
		}
	}

	void Update()
	{
		if (eventSystem.currentSelectedGameObject == gameObject && (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Return)))
		{
			submitButton.onClick.Invoke();
		}
	}

	public void SubmitScore()
	{
		if (inputField.text != "" && inputField.text != "TYPE YOUR NAME")
		{
			gameManager.SaveHighScore(inputField.text);
		}
		else
		{
			Debug.Log("Must input name");
			eventSystem.SetSelectedGameObject(gameObject);
		}
	}
}
