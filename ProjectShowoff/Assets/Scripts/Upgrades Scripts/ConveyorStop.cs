using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConveyorStop : Upgrade
{
    public ConveyorStop(int pCost, Sprite pSprite) : base(UpgradeType.ConveyorStop, pCost, pSprite)
    {

    }

    public override void ApplyUpgrade()
    {
        //look at the method below's comment
        BaseGame.Instance.EnableConveyorButton();
    }
}
