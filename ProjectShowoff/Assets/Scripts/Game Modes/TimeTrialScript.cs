using TMPro;
using UnityEngine;
public class TimeTrialScript : GameHandler
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private int timeLeft;
    [SerializeField] private int timeModifier; //to not make the time adding too op (probably to be deprecated)

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
		InvokeRepeating("UpdateTime", 1f, 1f);
		EventScript.Handler.Subscribe(EventType.ManageTime, ManageTime);
	}

	protected override void OnDestroyCallback()
	{
		base.OnDestroyCallback();
		EventScript.Handler.Unsubscribe(EventType.ManageTime, ManageTime);
	}

    private void ManageTime(Event e)
    {
        ManageTimeEvent manageTimeEvent = e as ManageTimeEvent;
        timeLeft += (int)manageTimeEvent.TimeAmount / timeModifier;
        timeText.text = "Time: " + timeLeft;
    }

    private void UpdateTime()
    {
        timeLeft--;
        timeText.text = "Time: " + timeLeft;
    }

    protected override void OnBoxDelivered(Event e)
    {
        base.OnBoxDelivered(e);
    }


}
