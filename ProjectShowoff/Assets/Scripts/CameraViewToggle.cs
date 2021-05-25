using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO by the love of god, rename!
public class CameraViewToggle : MonoBehaviour
{
	[SerializeField] private CameraMoveEvent.CameraState stateA;
	[SerializeField] private CameraMoveEvent.CameraState stateB;
	CameraMoveEvent.CameraState current;
	// Start is called before the first frame update
	void Start()
	{
		EventScript.Handler.Subscribe(EventType.CameraMove, (e) =>
		{
			OnCamMove(((CameraMoveEvent)e).NewState);
		});
		current = stateA;
	}

	public void Toggle()
	{
		current = current == stateA ? stateB : stateA;
		EventScript.Handler.BroadcastEvent(new CameraMoveEvent(current));
	}

	private void OnCamMove(CameraMoveEvent.CameraState newState)
	{
		if (current == stateA)
		{
			if (newState == stateB) current = stateB;
		}
		else if (current == stateB)
		{
			if (newState == stateA) current = stateA;
		}
	}
}
