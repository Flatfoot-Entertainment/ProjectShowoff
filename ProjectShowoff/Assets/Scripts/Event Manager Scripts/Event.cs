using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EventType
{
    ManageMoney,
    ManageUpgrade,
    ManageTime,
    ManageConveyors,
    ManageBoxSelect,
    ConveyorUpgrade,
    ConveyorStopButtonUpgrade,
    AddShip,
    CameraMove
}
public abstract class Event
{
    public readonly EventType type;

    public Event(EventType pType)
    {
        type = pType;
    }
}
