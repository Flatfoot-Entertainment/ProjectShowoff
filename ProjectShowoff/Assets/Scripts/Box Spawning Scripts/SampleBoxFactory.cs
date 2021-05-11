using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// box factory inherited from BoxFactory
public class SampleBoxFactory : MonoBehaviour, BoxFactory
{
	[SerializeField] private GameObject[] boxPrefabs;

	public ItemBox CreateRandomBox()
	{
		ItemBox box = null;
		int rand = UnityEngine.Random.Range(0, 3);
		switch (rand)
		{
			case 0:
				box = CreateBox1();
				break;
			case 1:
				box = CreateBox2();
				break;
			case 2:
				box = CreateBox3();
				break;
			default:
				break;
		}
		if (box == null)
		{
			throw new NullReferenceException("Box is no.");
		}
		return box;
	}

	public ItemBox CreateBox1()
	{
		return new ItemBox(BoxType.Type1);
	}

	public ItemBox CreateBox2()
	{
		return new ItemBox(BoxType.Type2);
	}

	public ItemBox CreateBox3()
	{
		return new ItemBox(BoxType.Type3);
	}
}
