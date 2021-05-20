using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraTranslate : MonoBehaviour
{
	[SerializeField] private CinemachineBrain brain;
	private CinemachineVirtualCamera lastCam;
	[SerializeField] private CinemachineVirtualCamera planetVCam;
	[SerializeField] private CinemachineVirtualCamera packagingVCam;
	[SerializeField] private CinemachineVirtualCamera shippingVCam;
	[SerializeField] private CameraMoveEvent.CameraState initialView;

	private IEnumerable<CinemachineVirtualCamera> AllCams()
	{
		yield return planetVCam;
		yield return packagingVCam;
		yield return shippingVCam;
	}

	private void Start()
	{
		EventScript.Instance.EventQueue.Subscribe(EventType.CameraMove, (e) =>
		{
			OnCamMove(((CameraMoveEvent)e).NewState);
		});

		foreach (CinemachineVirtualCamera cam in AllCams()) cam.Priority = 0;

		// Hopefully this executes stuff one frame after start
		Sequence seq = DOTween.Sequence();
		seq.AppendCallback(() =>
		{
			EventScript.Instance.EventQueue.AddEvent(
				new CameraMoveEvent(initialView)
			);
		});
		OnCamMove(initialView);
	}

	private void OnCamMove(CameraMoveEvent.CameraState newState)
	{
		CinemachineVirtualCamera newCam = null;
		switch (newState)
		{
			case CameraMoveEvent.CameraState.Planets:
				newCam = planetVCam;
				break;
			case CameraMoveEvent.CameraState.Packaging:
				newCam = packagingVCam;
				break;
			case CameraMoveEvent.CameraState.Shipping:
			default:
				newCam = shippingVCam;
				break;
		}
		if (lastCam) lastCam.Priority = 0;
		newCam.Priority = 1000;
		lastCam = newCam;
	}
}
