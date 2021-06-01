using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorStop : Upgrade
{
	public ConveyorStop(int pCost) : base(UpgradeType.ConveyorStop, pCost)
	{

	}

	public override void ApplyUpgrade()
	{
		//look at the method below's comment
		GameHandler.Instance.EnableConveyorButton();
	}

	public override void IncreaseLevel(int cost)
	{
		if (Level < 1)
		{
			base.IncreaseLevel(cost);
		}
	}
}
