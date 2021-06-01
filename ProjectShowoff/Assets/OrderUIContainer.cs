using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class OrderUIContainer : MonoBehaviour
{
    [SerializeField] private Planet planet;
    [SerializeField] private Image planetImage;

    [SerializeField] private Transform adaptiveField;
    [SerializeField] private GameObject requirementContainerPrefab;

    [SerializeField] private List<RequirementUIContainer> requirementUIContainers = new List<RequirementUIContainer>();


    //link this shit somehow per planet
    public void SetupContainer(Planet pPlanet){
        planet = pPlanet;
        planetImage.sprite = planet.PlanetImage;
        foreach(ItemType type in planet.needs.Keys){
            GameObject requirementContainer = Instantiate(requirementContainerPrefab, Vector3.zero, Quaternion.identity, adaptiveField);
            RequirementUIContainer requirementUIScript = requirementContainer.GetComponent<RequirementUIContainer>();
            requirementUIScript.SetupRequirementContainer(type, planet.needs[type]);
            requirementUIContainers.Add(requirementUIScript);
        }
    } 

    public void UpdateRequirementUIContainers(){
        for(int i = 0; i < planet.needs.Count; i++){
            var requirementUI = requirementUIContainers[i];
            
            //BAD BAD NO REMOVE THIS ASAP
            var type = planet.needs.Keys.ToList()[i];
            requirementUI.UpdateAmount(planet.needs[type]);
        }
    }

}
