using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO RENAME, PLEASE!
public class SingularCameraViewToggle : MonoBehaviour
{
	[SerializeField] private CameraMoveEvent.CameraState targetState;
	CameraMoveEvent.CameraState current;
	CameraMoveEvent.CameraState last;
	// Start is called before the first frame update
	void Start()
	{
		EventScript.Handler.Subscribe(EventType.CameraMove, (e) =>
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
			EventScript.Handler.BroadcastEvent(new CameraMoveEvent(current));
		}
		else
		{
			current = last;
			EventScript.Handler.BroadcastEvent(new CameraMoveEvent(current));
		}
	}

	private void OnCamMove(CameraMoveEvent.CameraState newState)
	{
		current = newState;
	}
}
