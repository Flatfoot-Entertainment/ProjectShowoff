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
        //conveyor stop button stuff, activate in ui or smth
    }
}
