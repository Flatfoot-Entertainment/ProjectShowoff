using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorUpgradeEvent : Event
{
	public int Level => level;
	private int level;
	public ConveyorUpgradeEvent(int pLevel) : base(EventType.ConveyorUpgrade)
	{
		level = pLevel;
	}
}
