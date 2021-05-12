using TMPro;
using UnityEngine;
public class TimeTrialScript : BaseGame
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
		ItemBoxContainer.OnBoxDelivered += AddTime;
		InvokeRepeating("UpdateTime", 1f, 1f);
	}

	protected override void OnDestroyCallback()
	{
		base.OnDestroyCallback();
		ItemBoxContainer.OnBoxDelivered -= AddTime;
	}

	private void AddTime(float timeToAdd)
	{
		timeLeft += (int)timeToAdd / timeModifier;
		timeText.text = "Time: " + timeLeft;
	}

	private void UpdateTime()
	{
		timeLeft--;
		timeText.text = "Time: " + timeLeft;
	}
}
