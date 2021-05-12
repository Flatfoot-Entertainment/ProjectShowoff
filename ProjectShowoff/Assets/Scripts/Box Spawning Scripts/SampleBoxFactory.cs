using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// box factory inherited from BoxFactory
public class SampleBoxFactory : MonoBehaviour, BoxFactory
{
	[SerializeField] private GameObject[] boxPrefabs;

	public ItemBoxData CreateRandomBox()
	{
		ItemBoxData box = null;
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

	public ItemBoxData CreateBox1()
	{
		return new ItemBoxData(BoxType.Type1);
	}

	public ItemBoxData CreateBox2()
	{
		return new ItemBoxData(BoxType.Type2);
	}

	public ItemBoxData CreateBox3()
	{
		return new ItemBoxData(BoxType.Type3);
	}
}
