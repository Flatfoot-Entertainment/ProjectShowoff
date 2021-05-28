using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    MechanicalParts,
    Medicine,
    Fuel
}

public class Item
{
    // TODO make value always be 1 -> only price differs
    public ItemType Type => type;
    public int Value => 1;
    public int Price => price;

    //prefab to be instantiated in the factory
    // public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
    //additional properties to add: sprite representation, change the isFragile to additional states?
    private readonly ItemType type;
    private readonly bool isFragile;
    private readonly int price;
    // private GameObject itemPrefab;

    public Item(ItemType pType, bool pIsFragile, int pPrice)
    {
        type = pType;
        isFragile = pIsFragile;
        price = pPrice;
    }

    public override string ToString()
    {
        return $"Item: {type}, value: {Value}, isFragile: {isFragile}, price: {price}";
    }
}
