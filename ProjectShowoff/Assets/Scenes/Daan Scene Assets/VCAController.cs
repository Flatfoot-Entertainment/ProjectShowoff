using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VCAController : MonoBehaviour
{
    private FMOD.Studio.VCA VcaController;
    public string VcaName;

    private Slider slider;

    private void Start()
    {
        VcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + VcaName);
        slider = GetComponent<Slider>();
    }

    public void SetVolume(float volume)
    {
        VcaController.setVolume(volume);
    }

}
