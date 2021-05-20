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
		if (!text) text = GetComponent<TMP_Text>();
	}

	public Dictionary<ItemType, int> Contents
	{
		set
		{
			if (!text) text = GetComponent<TMP_Text>();
			text.text = value.ToBeautifulString();
		}
	}
}
