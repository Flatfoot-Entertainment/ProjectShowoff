using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventRaiser : MonoBehaviour
{
	public void CameraMoveEvent(CameraMoveEvent.CameraState newState)
	{
		EventScript.Handler.BroadcastEvent(new CameraMoveEvent(newState));
	}

	public void ManageMoneyEvent(int value)
	{
		EventScript.Handler.BroadcastEvent(new ManageMoneyEvent(value));
	}

	public void ManageTimeEvent(float value)
	{
		EventScript.Handler.BroadcastEvent(new ManageTimeEvent(value));
	}

	// ManageUpgradeEvent can't really be called from a button
}
