using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetUI : MonoBehaviour
{
    public TMP_Text FoodText
    {
        get => foodText;
        set => foodText = value;
    }

    public TMP_Text FuelText
    {
        get => fuelText;
        set => fuelText = value;
    }

    public TMP_Text MedicineText
    {
        get => medicineText;
        set => medicineText = value;
    }

    public TMP_Text MechanicalText
    {
        get => mechanicalText;
        set => mechanicalText = value;
    }

    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text fuelText;
    [SerializeField] private TMP_Text medicineText;
    [SerializeField] private TMP_Text mechanicalText;

    public Dictionary<ItemType, int> Contents
    {
        set
        {
            foodText.text = value.ValueOr(ItemType.Food, 0).ToString();
            fuelText.text = value.ValueOr(ItemType.Fuel, 0).ToString();
            mechanicalText.text = value.ValueOr(ItemType.MechanicalParts, 0).ToString();
            medicineText.text = value.ValueOr(ItemType.Medicine, 0).ToString();
        }
    }
}
