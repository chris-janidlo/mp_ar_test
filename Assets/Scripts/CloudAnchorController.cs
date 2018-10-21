using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.CrossPlatform;

public class CloudAnchorController : MonoBehaviour
{
	public GameObject AnchorVisual;

	public Anchor Anchor { get; private set; }

	bool isHosting;

	string guiText = "placeholder";

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if (isHosting)
		{
			return;
		}

		Touch touch;
		if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
		{
			return;
		}

		TrackableHit hit;
		if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
		{
			Anchor = hit.Trackable.CreateAnchor(hit.Pose);
		}

		Instantiate(AnchorVisual, Anchor.transform.position, Anchor.transform.rotation).transform.parent = Anchor.transform;

		host();
	}

	void host ()
	{
		isHosting = true;
		guiText = "started hosting";
		
		XPSession.CreateCloudAnchor(Anchor).ThenAction(result =>
		{
			if (result.Response != CloudServiceResponse.Success)
			{
				guiText = result.Response.ToString();
			}
			else
			{
				guiText = "success: " + result.Anchor.CloudId;
				GameManager.Instance.LoadScene("Lobby");
				StartCoroutine(createLobby(result.Anchor.CloudId));
			}
		});
	}

	IEnumerator createLobby (string cloudId)
	{
		yield return new WaitWhile(() => GameManager.Instance.LoadingScene);
		
		(GameObject.Find("LobbyManager")).GetComponent<LobbyManager>().CreateLobby(cloudId);
	}

	void OnGUI()
    {
		GUI.skin.label.fontSize = 100;
        GUI.Label(new Rect(10, 10, 2000, 400), guiText);
    }
}
