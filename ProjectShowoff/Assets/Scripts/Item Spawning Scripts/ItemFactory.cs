using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//item factory to inherit from

public interface ItemFactory
{
    public Item CreateRandomItem();

    public ConsumerGoods CreateConsumerGoods();
    public Food CreateFood();
    public Fuel CreateFuel();
    public MechanicalParts CreateMechanicalParts();
    public Medicine CreateMedicine();
    public People CreatePeople();
}
