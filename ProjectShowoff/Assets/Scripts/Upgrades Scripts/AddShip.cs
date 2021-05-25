using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TBD what this upgrade will actually be
public class AddShip : Upgrade
{
    public AddShip(int pCost, Sprite pSprite) : base(UpgradeType.ShipQuantity, pCost, pSprite)
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
        //TODO finish up
        //EventScript.Instance.EventManager.InvokeEvent(new AddShipEvent());
        BaseGame.Instance.OnAddShip();
    }
}
