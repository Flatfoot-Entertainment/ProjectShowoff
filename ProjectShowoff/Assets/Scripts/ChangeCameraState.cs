using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraState : MonoBehaviour
{
    [SerializeField] private CameraMoveEvent.CameraState targetState;
    CameraMoveEvent.CameraState current;
    CameraMoveEvent.CameraState last;
    // Start is called before the first frame update
    void Start()
    {
        EventScript.Instance.EventManager.Subscribe(EventType.CameraMove, (e) =>
        {
            OnCamMove(((CameraMoveEvent)e).NewState);
        });
    }

    public void Toggle()
    {
        if (current != targetState)
        {
            last = current;
            current = targetState;
            EventScript.Instance.EventManager.InvokeEvent(new CameraMoveEvent(current));
        }
        else
        {
            current = last;
            EventScript.Instance.EventManager.InvokeEvent(new CameraMoveEvent(current));
        }
    }

    private void OnCamMove(CameraMoveEvent.CameraState newState)
    {
        current = newState;
    }
}
