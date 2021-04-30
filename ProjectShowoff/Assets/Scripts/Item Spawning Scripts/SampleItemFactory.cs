using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleItemFactory : ItemFactory
{
    public const int NUMBER_OF_UNIQUE_ITEMS = 6;

    public Item CreateRandomItem()
    {
        Item item = null;
        int rand = UnityEngine.Random.Range(0, NUMBER_OF_UNIQUE_ITEMS);
        switch (rand)
        {
            case 0:
                item = CreateConsumerGoods();
                break;
            case 1:
                item = CreateFood();
                break;
            case 2:
                item = CreateFuel();
                break;
            case 3:
                item = CreateMechanicalParts();
                break;
            case 4:
                item = CreateMedicine();
                break;
            case 5:
                item = CreatePeople();
                break;
            default:
                break;
        }
        if(item is null)
        {
            throw new NullReferenceException("dafuq is this item it doesnt exist");
        }
        return item;
    }

    public ConsumerGoods CreateConsumerGoods()
    {
        return new ConsumerGoods(45f, 100);
    }

    public Food CreateFood()
    {
        return new Food(45f, 100);
    }

    public Fuel CreateFuel()
    {
        return new Fuel(45f, 100);
    }

    public MechanicalParts CreateMechanicalParts()
    {
        return new MechanicalParts(45f, 100);
    }

    public Medicine CreateMedicine()
    {
        return new Medicine(45f, 100);
    }

    public People CreatePeople()
    {
        return new People(45f, 100);
    }
}
