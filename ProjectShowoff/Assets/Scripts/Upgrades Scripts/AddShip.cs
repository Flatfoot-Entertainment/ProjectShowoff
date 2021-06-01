using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TBD what this upgrade will actually be
public class AddShip : Upgrade
{
	public AddShip(int pCost) : base(UpgradeType.ShipQuantity, pCost)
	{

	}


	public override void IncreaseLevel(int cost)
	{
		if (Level <= 2)
		{
			base.IncreaseLevel(cost);
		}
	}
	public override void ApplyUpgrade()
	{
		//TODO finish up
		//EventScript.Instance.EventManager.InvokeEvent(new AddShipEvent());
		GameHandler.Instance.OnAddShip();
	}
}
