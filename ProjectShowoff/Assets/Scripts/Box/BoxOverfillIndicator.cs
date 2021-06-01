using UnityEngine;
using DG.Tweening;

public class BoxOverfillIndicator : MonoBehaviour
{
	[SerializeField] private Renderer indicator;

	[SerializeField] private Color validColor;
	[SerializeField] private Color invalidColor;
	[SerializeField] private float transitionTime = 0.1f;
	[SerializeField] private Ease transitionEase = Ease.Linear;

	public void ChangeState(bool valid)
	{
		indicator.material.DOKill();
		indicator.material.DOColor(valid ? validColor : invalidColor, transitionTime).SetEase(transitionEase);
	}
}
