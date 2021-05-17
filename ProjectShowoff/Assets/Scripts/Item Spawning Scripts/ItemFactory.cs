using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//item factory to inherit from

[CreateAssetMenu(menuName = "Scriptable Objects/Item Factory")]
public abstract class ItemFactory : ScriptableObject
{
    public abstract Item CreateRandomItem();

    public abstract ConsumerGoods CreateConsumerGoods();
    public abstract Food CreateFood();
    public abstract Fuel CreateFuel();
    public abstract MechanicalParts CreateMechanicalParts();
    public abstract Medicine CreateMedicine();
    public abstract People CreatePeople();
}
