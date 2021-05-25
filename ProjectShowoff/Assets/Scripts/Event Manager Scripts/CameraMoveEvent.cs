
public class CameraMoveEvent : Event
{
	public enum CameraState
	{
		Planets,
		Packaging,
		Shipping
	}

	public CameraState NewState { get; }

	public CameraMoveEvent(CameraState state) : base(EventType.CameraMove)
	{
		NewState = state;
	}
}