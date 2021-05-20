using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewToggle : MonoBehaviour
{
	[SerializeField] private CameraMoveEvent.CameraState stateA;
	[SerializeField] private CameraMoveEvent.CameraState stateB;
	CameraMoveEvent.CameraState current;
	// Start is called before the first frame update
	void Start()
	{
		EventScript.Instance.EventQueue.Subscribe(EventType.CameraMove, (e) =>
		{
			OnCamMove(((CameraMoveEvent)e).NewState);
		});
		current = stateA;
	}

	public void Toggle()
	{
		current = current == stateA ? stateB : stateA;
		EventScript.Instance.EventQueue.AddEvent(new CameraMoveEvent(current));
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
