using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class PlanetUI : MonoBehaviour
{
	private TMP_Text text;

	private void Awake()
	{
		text = GetComponent<TMP_Text>();
	}

	public Dictionary<ItemType, float> Contents
	{
		set
		{
			text.text = value.ToBeautifulString();
		}
	}
}
