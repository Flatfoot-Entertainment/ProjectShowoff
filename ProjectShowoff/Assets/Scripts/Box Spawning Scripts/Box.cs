using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BoxType
{
    Type1,
    Type2,
    Type3
}

public class Box 
{
    public BoxType Type => type;
    private readonly BoxType type;

    //sorting items by their item type
    private Dictionary<ItemType, float> boxContents;
    public Dictionary<ItemType, float> BoxContents => boxContents;

    //cost for the box?

    public Box(BoxType pBoxType)
    {
        type = pBoxType;
        boxContents = new Dictionary<ItemType, float>();
    }

    public void AddItemToBox(Item item)
    {
        if (!boxContents.ContainsKey(item.Type)) //does the box already contain the item type?
        {
            boxContents.Add(item.Type, item.Value);
        }
        else
        {
            boxContents[item.Type] += item.Value;
        }
    }

    public void RemoveItemFromBox(Item item)
    {
        boxContents.Remove(item.Type);
    }

    public float GetBoxContentsValue()
    {
        float contentsSum = 0f;
        foreach(ItemType itemType in boxContents.Keys.ToList())
        {
            contentsSum += boxContents[itemType];
        }
        return contentsSum;
    }

    public void ShowBoxContents()
    {
        Debug.Log("Contents: ");
        foreach (ItemType itemType in boxContents.Keys.ToList())
        {
            Debug.Log(itemType.ToString() + ": " + boxContents[itemType]);
        }
    }
}
