using TMPro;
using UnityEngine;
public class TimeTrialScript : BaseGame
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private int timeLeft;

    public int TimeLeft
    {
        get => timeLeft;
        set => timeLeft = value;
    }

    //TODO update with events through
    // Start is called before the first frame update
    protected override void Start()
    {
        timeText.text = "Time: " + timeLeft;
        InvokeRepeating("UpdateTime", 1f, 1f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    private void UpdateTime()
    {
        timeLeft--;
        timeText.text = "Time: " + timeLeft;
    }
}
