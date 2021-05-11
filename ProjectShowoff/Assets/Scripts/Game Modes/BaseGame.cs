using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//the base game class, don't even know what to put in here yet
public abstract class BaseGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private float money;
    public float Money
    {
        get => money;
        set => money = value;
    }
    //TODO trigger money update when sending boxes/whatever other things money is used for (events)


    // Start is called before the first frame update
    protected virtual void Start()
    {
        moneyText.text = "Money: " + money;
        BoxContainer.OnBoxSent += RemoveMoney;
        BoxContainer.OnBoxDelivered += AddMoney;
    }

	private void OnDestroy()
	{
		BoxContainer.OnBoxDelivered -= AddMoney;
		BoxContainer.OnBoxSent -= RemoveMoney;
		OnDestroyCallback();
	}

	private void AddMoney(float moneyToAdd)
    {
        money+=moneyToAdd;
        moneyText.text = "Money: " + money;
    }

    private void RemoveMoney(float moneyToRemove)
    {
        money -= moneyToRemove;
        moneyText.text = "Money: " + money;
    }

	protected virtual void OnDestroyCallback() { }

	// Update is called once per frame
	protected virtual void Update()
	{
	}
}
