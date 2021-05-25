using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageUpgradeEvent : Event
{
    public Upgrade Upgrade => upgrade;
    private Upgrade upgrade;

    public ManageUpgradeEvent(Upgrade pUpgrade) : base(EventType.ManageUpgrade)
    {
        upgrade = pUpgrade;
    }
}
