using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    public RectTransform HitMarker
    {
        get => hitMarker;
        set => hitMarker = value;
    }

    public CanvasScaler Scaler
    {
        get => scaler;
        set => scaler = value;
    }


    [SerializeField] private RectTransform hitMarker;
    [SerializeField] private CanvasScaler scaler;
    [SerializeField] private PlanetUI ui;
    [SerializeField] private UnityEvent<Planet> OnClick;
    public Dictionary<ItemType, int> needs { get; private set; } = new Dictionary<ItemType, int>();

    private void Start()
    {
        // For now randomly create planet properties
        InitRandom();
        ui.Contents = needs;
    }

    public void Deliver(ContainerData box)
    {
        int money = 0;
        foreach (ItemBoxData b in box.Contents)
        {
            foreach (Item i in b.Contents)
            {
                if (needs.ContainsKey(i.Type))
                {
                    needs[i.Type] -= 1;
                    if (needs[i.Type] <= 0)
                    {
                        needs.Remove(i.Type);
                    }
                    // Only add money if the planet actually needed it
                    money += i.Price;
                }
            }
        }
        // Add the money to the player
        EventScript.Handler.BroadcastEvent(new ManageMoneyEvent(money));
        if (needs.Count <= 0)
        {
            InitRandom();
        }
        ui.Contents = needs;
    }

    public void Deselect()
    {
        hitMarker.gameObject.SetActive(false);
    }

    public void Select()
    {
        hitMarker.gameObject.SetActive(true);
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPos /= scaler.scaleFactor;
        hitMarker.transform.position = screenPos;
    }

    private void OnMouseDown()
    {
        OnClick?.Invoke(this);
    }

    private void InitRandom()
    {
        needs.Clear();
        int numProps = Random.Range(7, 10);
        for (int i = 0; i < numProps; i++)
        {
            ItemType t = Extensions.RandomEnumValue<ItemType>();
            if (needs.ContainsKey(t)) needs[t] += 1;
            else needs[t] = 1;
        }
    }
}
