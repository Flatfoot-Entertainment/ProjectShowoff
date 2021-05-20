using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingularCameraViewToggle : MonoBehaviour
{
	[SerializeField] private CameraMoveEvent.CameraState targetState;
	CameraMoveEvent.CameraState current;
	CameraMoveEvent.CameraState last;
	// Start is called before the first frame update
	void Start()
	{
		EventScript.Instance.EventQueue.Subscribe(EventType.CameraMove, (e) =>
		{
			OnCamMove(((CameraMoveEvent)e).NewState);
		});
	}

	public void Toggle()
	{
		if (current != targetState)
		{
			last = current;
			current = targetState;
			EventScript.Instance.EventQueue.AddEvent(new CameraMoveEvent(current));
		}
		else
		{
			current = last;
			EventScript.Instance.EventQueue.AddEvent(new CameraMoveEvent(current));
		}
	}

	private void OnCamMove(CameraMoveEvent.CameraState newState)
	{
		current = newState;
	}
}