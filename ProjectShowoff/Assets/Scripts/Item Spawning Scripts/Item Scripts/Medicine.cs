using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : Item
{
    public Medicine(float pValue, int pPrice) : base(ItemType.Medicine, pValue, true, pPrice)
    {

    }
}
