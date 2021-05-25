using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMoneyEvent : Event
{
	public int Amount => amount;
	private int amount;

	public ManageMoneyEvent(int pAmount) : base(EventType.ManageMoney)
	{
		amount = pAmount;
	}
}
