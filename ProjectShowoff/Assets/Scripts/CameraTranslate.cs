using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTranslate : MonoBehaviour
{
	[SerializeField] private CinemachineBrain brain;
	[SerializeField] private CinemachineVirtualCamera[] cameras;

	private void Start()
	{
		brain = Camera.main.GetComponent<CinemachineBrain>();
	}

	public void MoveCamera()
	{
		if (!brain.IsBlending)
		{
			if (cameras[0].Priority < cameras[1].Priority)
			{
				//TODO dont use magic variables;
				cameras[0].Priority += 3;
			}
			else
			{
				cameras[0].Priority -= 3;
			}
		}

	}

	public enum Mode
	{
		Planets,
		Packaging
	}
	[SerializeField] private Camera planetCamera;

	private Mode mode = Mode.Packaging;

	// TODO this might not be necessary, if we use cinemachine
	public void ToggleMode()
	{
		mode = mode == Mode.Planets ? Mode.Packaging : Mode.Planets;

		switch (mode)
		{
			case Mode.Packaging:
				brain.gameObject.SetActive(true);
				planetCamera.gameObject.SetActive(false);
				break;
			case Mode.Planets:
				brain.gameObject.SetActive(false);
				planetCamera.gameObject.SetActive(true);
				break;
		}
	}
}
