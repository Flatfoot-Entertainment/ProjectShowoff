using TMPro;
using UnityEngine;
public class TimeTrialScript : BaseGame
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private int timeLeft;
    [SerializeField] private int timeToAdd;

    public int TimeLeft
    {
        get => timeLeft;
        set => timeLeft = value;
    }

    //TODO update with events through
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timeText.text = "Time: " + timeLeft;
        BoxContainer.OnBoxDelivered += AddTime;
        InvokeRepeating("UpdateTime", 1f, 1f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        BoxContainer.OnBoxDelivered -= AddTime;
    }

    private void AddTime()
    {
        timeLeft+=timeToAdd;
        timeText.text = "Time: " + timeLeft;
    }

    private void UpdateTime()
    {
        timeLeft--;
        timeText.text = "Time: " + timeLeft;
    }
}
