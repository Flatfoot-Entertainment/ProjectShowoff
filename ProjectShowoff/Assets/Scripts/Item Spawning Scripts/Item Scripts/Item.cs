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
    public int Price => price;

    //prefab to be instantiated in the factory
    public GameObject ItemPrefab { get => itemPrefab; set => itemPrefab = value; }
    //additional properties to add: sprite representation, change the isFragile to additional states?
    private readonly ItemType type;
    private readonly float value;
    private readonly bool isFragile;
    private readonly int price;
    private GameObject itemPrefab;

    public Item(ItemType pType, float pValue, bool pIsFragile, int pPrice)
    {
        type = pType;
        value = pValue;
        isFragile = pIsFragile;
        price = pPrice;
    }

    public override string ToString()
    {
        return $"Item: {type}, value: {value}, isFragile: {isFragile}, price: {price}";
    }
}
