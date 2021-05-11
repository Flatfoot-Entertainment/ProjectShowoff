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
		timeText.text = "Time: " + timeLeft;
		BoxContainer.OnBoxDelivered += AddTime;
		InvokeRepeating("UpdateTime", 1f, 1f);
	}

	// Update is called once per frame
	protected override void Update()
	{

	}

	private void AddTime(Box box)
	{
		timeLeft += timeToAdd;
		timeText.text = "Time: " + timeLeft;
	}

	private void UpdateTime()
	{
		timeLeft--;
		timeText.text = "Time: " + timeLeft;
	}
}
