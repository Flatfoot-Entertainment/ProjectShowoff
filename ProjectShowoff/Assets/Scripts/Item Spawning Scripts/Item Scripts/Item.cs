using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    MechanicalParts,
    Medicine,
    People,
    ConsumerGoods,
    Fuel
}

public class Item
{
    public ItemType Type => type;
    public float Value => value;
    //additional properties to add: sprite representation, change the isFragile to additional states?
    private readonly ItemType type;
    private readonly float value;
    private readonly bool isFragile;

    public Item(ItemType pType, float pValue, bool pIsFragile)
    {
        type = pType;
        value = pValue;
        isFragile = pIsFragile;
    }

    public override string ToString()
    {
        return $"Item: {type}, value: {value}, isFragile: {isFragile}";
    }
}
