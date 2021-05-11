using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FasterDelivery : Upgrade
{
    public FasterDelivery(int pCost, Sprite pSprite) : base(UpgradeType.FasterDelivery, pCost, pSprite)
    {

    }

    public override void ApplyUpgrade()
    {
        //faster delivery stuff
    }
}
