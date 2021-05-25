using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TBD what this upgrade will actually be
public class ShipQuantity : Upgrade
{
	public ShipQuantity(int pCost, Sprite pSprite) : base(UpgradeType.ShipQuantity, pCost, pSprite)
	{

	}


	public override void IncreaseLevel()
	{
		if (Level <= 2)
		{
			base.IncreaseLevel();
		}
	}
	public override void ApplyUpgrade()
	{
		GameHandler.Instance.UpgradeShipQuantity();
	}
}
