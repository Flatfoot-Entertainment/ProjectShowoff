using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTimeEvent : Event
{
	public float TimeAmount => timeAmount;
	private float timeAmount;
	public ManageTimeEvent(float pTimeAmount) : base(EventType.ManageTime)
	{
		timeAmount = pTimeAmount;
	}
}
