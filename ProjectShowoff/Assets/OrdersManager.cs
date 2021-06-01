using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    [SerializeField] private Planet[] planets;
    [SerializeField] private Transform planetCenter;

    [SerializeField] private Transform ordersContainer;
    [SerializeField] private GameObject orderUIContainerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        planetCenter = GameObject.Find("Planet Center").transform;
        planets = planetCenter.GetComponentsInChildren<Planet>();
        foreach(Planet planet in planets){
            AddOrder(planet);
        }
    }

    private void AddOrder(Planet planet){
        GameObject orderUIContainer = Instantiate(orderUIContainerPrefab, Vector3.zero, Quaternion.identity, ordersContainer);
        OrderUIContainer orderUIScript = orderUIContainer.GetComponent<OrderUIContainer>();
        orderUIScript.SetupContainer(planet);
    }
}
