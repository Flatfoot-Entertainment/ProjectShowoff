using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUIScript : MonoBehaviour
{
	[SerializeField] private CameraMoveEvent.CameraState stateA;
	[SerializeField] private CameraMoveEvent.CameraState stateB;
	// TODO use the event handler for all of this
	[SerializeField] private GameObject[] stateAGameObjects;
	[SerializeField] private GameObject[] stateBGameObjects;

	private void Start()
	{
		EventScript.Handler.Subscribe(EventType.CameraMove, (e) =>
		{
			OnCamMove(((CameraMoveEvent)e).NewState);
		});
	}

	private void OnCamMove(CameraMoveEvent.CameraState newState)
	{
		if (newState == stateA)
		{
			foreach (GameObject gO in stateBGameObjects) gO.SetActive(false);
			foreach (GameObject gO in stateAGameObjects) gO.SetActive(true);
		}
		else if (newState == stateB)
		{
			foreach (GameObject gO in stateBGameObjects) gO.SetActive(true);
			foreach (GameObject gO in stateAGameObjects) gO.SetActive(false);
		}
	}
}
