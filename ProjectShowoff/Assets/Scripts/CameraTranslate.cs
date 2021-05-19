using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTranslate : MonoBehaviour
{
	public enum Mode
	{
		Planets,
		Packaging
	}
	[SerializeField] private Camera mainCamera;
	[SerializeField] private Camera planetCamera;
	[SerializeField] private Transform[] cameraPositions;
	[SerializeField] private float cameraMoveSpeed;
	[SerializeField] private Vector3 currentTargetPosition;

	private Mode mode = Mode.Packaging;

	private Tweener tweener;

	private void Start()
	{
		currentTargetPosition = cameraPositions[0].position;
		mainCamera.transform.position = currentTargetPosition;
	}

	public void MoveCamera()
	{
		if (tweener == null || !(tweener.IsActive() && tweener.IsPlaying()))
		{
			if (mainCamera.transform.position == cameraPositions[0].position)
			{
				currentTargetPosition = cameraPositions[1].position;
			}
			else if (mainCamera.transform.position == cameraPositions[1].position)
			{
				currentTargetPosition = cameraPositions[0].position;
			}
			tweener = mainCamera.transform.DOMove(currentTargetPosition, cameraMoveSpeed);
		}

	}

	// TODO this might not be necessary, if we use cinemachine
	public void ToggleMode()
	{
		mode = mode == Mode.Planets ? Mode.Packaging : Mode.Planets;

		switch (mode)
		{
			case Mode.Packaging:
				if (tweener.IsActive()) tweener.Kill();
				mainCamera.transform.position = currentTargetPosition;
				mainCamera.gameObject.SetActive(true);
				planetCamera.gameObject.SetActive(false);
				break;
			case Mode.Planets:
				mainCamera.gameObject.SetActive(false);
				planetCamera.gameObject.SetActive(true);
				break;
		}
	}
}
