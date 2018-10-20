using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleARCore;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance { get; private set; }

	public string MainMenuScene;

	public ARCoreSession SessionPrefab;
	[Tooltip("Scenes where the manager should create AR session.")]
	public List<string> ARScenes;

	bool sessionCreated;

	void Start ()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
	
	public void LoadScene (string scene)
	{
		StartCoroutine(loadScene(scene));
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene(MainMenuScene);
		}
	}

	IEnumerator loadScene (string scene)
	{
		AsyncOperation load = SceneManager.LoadSceneAsync(scene);
		yield return new WaitUntil(() => load.isDone);

		if (!sessionCreated && ARScenes.Contains(scene))
		{
			var sessi = Instantiate(SessionPrefab, Vector3.zero, Quaternion.identity);
			DontDestroyOnLoad(sessi.gameObject);
			sessionCreated = true;
		}
	}
}
