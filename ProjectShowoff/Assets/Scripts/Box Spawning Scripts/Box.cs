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
    //something to do when putting items - dictionary<itemtype, value>?
    Dictionary<ItemType, float> boxContents;

    public Box(BoxType pBoxType)
    {
        type = pBoxType;
        boxContents = new Dictionary<ItemType, float>();
    }

    public void AddItemToBox(Item item)
    {
        if (!boxContents.ContainsKey(item.Type))
        {
            boxContents.Add(item.Type, item.Value);
        }
        else
        {
            boxContents[item.Type] += item.Value;
        }
    }
}
