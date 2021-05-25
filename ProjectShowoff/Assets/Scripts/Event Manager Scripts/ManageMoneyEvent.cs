using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMoneyEvent : Event
{
    public float Amount => amount;
    private float amount;

    public ManageMoneyEvent(float pAmount) : base(EventType.ManageMoney)
    {
        amount = pAmount;
    }
}
