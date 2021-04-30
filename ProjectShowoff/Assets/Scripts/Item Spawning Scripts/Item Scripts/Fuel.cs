using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Item
{
    public Fuel(float pValue, int pPrice) : base(ItemType.Fuel, pValue, true, pPrice)
    {

    }
}
