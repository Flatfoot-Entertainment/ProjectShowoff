using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Rename
public class UIViewSwitchingHandler : MonoBehaviour
{
	// TODO use the event handler for all of this
	[SerializeField] private GameObject[] packagingOnlyGameObjects;
	[SerializeField] private GameObject[] planetsOnlyGameObjects;

	private void Start()
	{
		EventScript.Instance.EventQueue.Subscribe(EventType.CameraMove, (e) =>
		{
			OnCamMove(((CameraMoveEvent)e).NewState);
		});
	}

	private void OnCamMove(CameraMoveEvent.CameraState newState)
	{
		switch (newState)
		{
			case CameraMoveEvent.CameraState.Planets:
				foreach (GameObject gO in packagingOnlyGameObjects) gO.SetActive(false);
				foreach (GameObject gO in planetsOnlyGameObjects) gO.SetActive(true);
				break;
			case CameraMoveEvent.CameraState.Packaging:
			case CameraMoveEvent.CameraState.Shipping:
				foreach (GameObject gO in packagingOnlyGameObjects) gO.SetActive(true);
				foreach (GameObject gO in planetsOnlyGameObjects) gO.SetActive(false);
				break;
		}
	}
}
