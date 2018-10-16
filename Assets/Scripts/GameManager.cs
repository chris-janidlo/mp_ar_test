using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public string MainMenuScene;

	void Start ()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	public void LoadScene (string scene)
	{
		SceneManager.LoadScene(scene);
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene(MainMenuScene);
		}
	}
}
