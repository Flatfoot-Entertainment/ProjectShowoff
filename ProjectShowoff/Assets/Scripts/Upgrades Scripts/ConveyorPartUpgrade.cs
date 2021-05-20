using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorPartUpgrade : Upgrade
{
    public ConveyorPartUpgrade(int pCost, Sprite pSprite) : base(UpgradeType.ConveyorUpgrade, pCost, pSprite)
    {

    }

    public override void ApplyUpgrade()
    {
        BaseGame.Instance.UpgradeConveyorBelt(Level);
    }

    public override void IncreaseLevel()
    {
        if (Level <= 3)
        {
            base.IncreaseLevel();
        }
    }
}