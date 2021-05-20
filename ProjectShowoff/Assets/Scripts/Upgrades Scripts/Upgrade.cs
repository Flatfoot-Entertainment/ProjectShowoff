using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType
{
    ConveyorStop,
    ConveyorUpgrade,
    FasterDelivery,
    DeliveryShipSpeed
}

//need to set the sprite to be specific for each type of upgrade, instead of setting it in the prefab
//getting the path of the sprite?

//TODO maybe convert the upgrade to a scriptable object?

public abstract class Upgrade
{
    public UpgradeType Type => type;
    public int Level => level;
    public int Cost => cost;
    public Sprite Sprite => sprite;

    private readonly UpgradeType type;
    private int level;
    private int cost;
    private Sprite sprite;

    public Upgrade(UpgradeType pType, int pCost, Sprite pSprite)
    {
        level = 0;
        type = pType;
        cost = pCost;
        sprite = pSprite;
    }

    public abstract void ApplyUpgrade();

    public virtual void IncreaseLevel()
    {
        level++;
        cost *= level;
    }

}
