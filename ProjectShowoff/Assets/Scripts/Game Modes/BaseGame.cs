using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//the base game class, don't even know what to put in here yet
public abstract class BaseGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int money;
    public int Money
    {
        get => money;
        set => money = value;
    }
    //TODO trigger money update when sending boxes/whatever other things money is used for (events)


    // Start is called before the first frame update
    protected virtual void Start()
    {
        moneyText.text = "Money: " + money;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        //put in a seperate method later on (check the todo above)
        money++;
        moneyText.text = "Money: " + money;
    }
}
