using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudioHover : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Event;
    public bool PlayOnAwake;

    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
    }

    void Start()
    {
        if (PlayOnAwake)
            PlayOneShot();
    }

}
