using System.Collections;
using System.Collections.Generic;
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

    public Box(BoxType pBoxType)
    {
        type = pBoxType;
    }
}
