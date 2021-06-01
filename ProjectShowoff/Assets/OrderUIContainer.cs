using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderUIContainer : MonoBehaviour
{
    [SerializeField] private Image planetImage;

    [SerializeField] private Transform adaptiveField;
    [SerializeField] private GameObject requirementContainerPrefab;

    public void SetupContainer(Planet planet){
        planetImage.sprite = planet.PlanetImage;
        foreach(ItemType type in planet.needs.Keys){
            GameObject requirementContainer = Instantiate(requirementContainerPrefab, Vector3.zero, Quaternion.identity, adaptiveField);
            RequirementUIContainer requirementUIScript = requirementContainer.GetComponent<RequirementUIContainer>();
            requirementUIScript.SetupRequirementContainer(type, planet.needs[type]);
        }
    } 

}
