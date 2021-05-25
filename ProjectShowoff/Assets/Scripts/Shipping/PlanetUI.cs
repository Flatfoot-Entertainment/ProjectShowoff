using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetUI : MonoBehaviour
{
	[SerializeField] private TMP_Text foodText;
	[SerializeField] private TMP_Text fuelText;
	[SerializeField] private TMP_Text medicineText;
	[SerializeField] private TMP_Text mechanicalText;

	public Dictionary<ItemType, int> Contents
	{
		set
		{
			foreach (var kvp in value)
			{
				switch (kvp.Key)
				{
					case ItemType.Food:
						foodText.text = kvp.Value.ToString();
						break;
					case ItemType.MechanicalParts:
						mechanicalText.text = kvp.Value.ToString();
						break;
					case ItemType.Medicine:
						medicineText.text = kvp.Value.ToString();
						break;
					case ItemType.Fuel:
						fuelText.text = kvp.Value.ToString();
						break;
				}
			}
		}
	}
}
