using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetUI : MonoBehaviour
{
	[SerializeField] private TMP_Text text;
	public Dictionary<ItemType, float> Contents
	{
		set
		{
			text.text = value.ToBeautifulString();
		}
	}
}
