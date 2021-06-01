using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePlanetRequirementEvent : Event
{
    public int AmountToSubtract {
        get => amountToSubtract;
        set => amountToSubtract = value;
    }
    private int amountToSubtract;

    public ManagePlanetRequirementEvent(int pAmountToSubtract) : base(EventType.ManagePlanetRequirement){
        amountToSubtract = pAmountToSubtract;
    }
}
