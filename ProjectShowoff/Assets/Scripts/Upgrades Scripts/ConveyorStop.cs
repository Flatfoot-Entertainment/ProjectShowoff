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
        //EventScript.Instance.EventManager.InvokeEvent(new ConveyorStopButtonUpgradeEvent());
        BaseGame.Instance.EnableConveyorButton();
    }

    public override void IncreaseLevel()
    {
        if (Level < 1)
        {
            base.IncreaseLevel();
        }
    }
}
