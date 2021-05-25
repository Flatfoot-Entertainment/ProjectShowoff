using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBoxSelectEvent : Event
{
    public GameObject BoxToSpawn => boxToSpawn;
    private GameObject boxToSpawn;
    public ManageBoxSelectEvent(GameObject pBoxToSpawn) : base(EventType.ManageBoxSelect)
    {
        boxToSpawn = pBoxToSpawn;
    }
}
