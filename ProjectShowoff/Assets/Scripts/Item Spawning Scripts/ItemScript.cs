using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//item script to show debug info in the inspector
[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class ItemScript : MonoBehaviour
{
    public Item Item { get => item; set => item = value; }
    private Item item;

    [SerializeField] private ItemType itemType;
    [SerializeField] private float itemValue;
    [SerializeField] private int price;

    private void Start()
    {
        itemType = item.Type;
        itemValue = item.Value;
        price = item.Price;
    }
}
