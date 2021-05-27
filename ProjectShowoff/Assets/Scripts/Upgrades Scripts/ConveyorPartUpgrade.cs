using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorPartUpgrade : Upgrade
{
	public ConveyorPartUpgrade(int pCost) : base(UpgradeType.ConveyorUpgrade, pCost)
	{

	}

    public override void ApplyUpgrade()
    {
        EventScript.Handler.BroadcastEvent(new ConveyorUpgradeEvent(Level));
    }

	public override void IncreaseLevel(int cost)
	{
		if (Level <= 3)
		{
			base.IncreaseLevel(cost);
		}
	}
}
