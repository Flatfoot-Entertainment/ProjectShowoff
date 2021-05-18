using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipSelectionUI : MonoBehaviour
{
	public event System.Action OnSelect;
	[SerializeField] private TMP_Text propertyText;

	public Ship Ship
	{
		set
		{
			propertyText.text = value.box.GetItems().ToBeautifulString();
		}
	}

	public void ButtonCallback()
	{
		OnSelect?.Invoke();
	}
}
