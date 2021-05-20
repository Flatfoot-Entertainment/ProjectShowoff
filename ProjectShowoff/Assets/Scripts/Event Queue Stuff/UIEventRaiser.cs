using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventRaiser : MonoBehaviour
{
	public void CameraMoveEvent(CameraMoveEvent.CameraState newState)
	{
		EventScript.Instance.EventQueue.AddEvent(new CameraMoveEvent(newState));
	}

	// TODO for others
}
