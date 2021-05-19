using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewSwitchingHandler : MonoBehaviour
{
	// TODO use the event handler for all of this
	[SerializeField] private GameObject[] packagingOnlyGameObjects;
	[SerializeField] private GameObject[] planetsOnlyGameObjects;
	private CameraTranslate.Mode mode = CameraTranslate.Mode.Packaging;

	private void Start()
	{
		ApplyMode();
	}

	public void ToggleMode()
	{
		mode = mode == CameraTranslate.Mode.Planets ? CameraTranslate.Mode.Packaging : CameraTranslate.Mode.Planets;

		ApplyMode();
	}

	private void ApplyMode()
	{
		switch (mode)
		{
			case CameraTranslate.Mode.Planets:
				foreach (GameObject gO in packagingOnlyGameObjects) gO.SetActive(false);
				foreach (GameObject gO in planetsOnlyGameObjects) gO.SetActive(true);
				break;
			case CameraTranslate.Mode.Packaging:
				foreach (GameObject gO in packagingOnlyGameObjects) gO.SetActive(true);
				foreach (GameObject gO in planetsOnlyGameObjects) gO.SetActive(false);
				break;
		}
	}
}
