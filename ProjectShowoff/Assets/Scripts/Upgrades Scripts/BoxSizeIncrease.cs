using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxSizeIncrease : Upgrade
{
    public BoxSizeIncrease(int pCost, Sprite pSprite) : base(UpgradeType.BoxSizeIncrease, pCost, pSprite)
    {

    }

    public override void ApplyUpgrade()
    {
        //increase box size stuff
    }
}
