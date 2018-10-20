using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoader : MonoBehaviour
{
	public string Scene;

	void Start ()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	void OnClick ()
	{
		GameManager.Instance.LoadScene(Scene);
	}
}
