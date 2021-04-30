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
    private BoxType boxType;
    //something to do when putting items - dictionary<itemtype, value>?

    public Box(BoxType pBoxType)
    {
        boxType = pBoxType;
    }
}
