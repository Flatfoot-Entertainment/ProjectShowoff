using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RequirementUIContainer : MonoBehaviour
{
    

    [SerializeField] private Sprite foodSprite, mechanicalPartsSprite, fuelSprite, medicineSprite;
    [SerializeField] private Image requirementImage;
    [SerializeField] private TextMeshProUGUI amountText;

    [SerializeField] private ItemType type;
    [SerializeField] private int amount;


    public void SetupRequirementContainer(ItemType itemType, int itemAmount){
        Sprite requirementSprite = null;
        switch(itemType){
            case ItemType.Food:
            requirementSprite = foodSprite;
            break;
            case ItemType.MechanicalParts:
            requirementSprite = mechanicalPartsSprite;
            break;
            case ItemType.Fuel:
            requirementSprite = fuelSprite;
            break;
            case ItemType.Medicine:
            requirementSprite = medicineSprite;
            break;
        }
        type = itemType;
        amount = itemAmount;
        if(requirementSprite != null){
            requirementImage.sprite = requirementSprite;
        }
        amountText.text = itemAmount.ToString();
    }

    public void UpdateAmount(int amount){
        amountText.text = amount.ToString();
    } 
}
