using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTranslate : MonoBehaviour
{
    [SerializeField] private CinemachineBrain brain;
    [SerializeField] private CinemachineVirtualCamera[] cameras;


    private void Start()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }

    public void MoveCamera()
    {
        if (!brain.IsBlending)
        {
            if(cameras[0].Priority < cameras[1].Priority)
            {
                //TODO dont use magic variables;
                cameras[0].Priority += 3;
            }
            else
            {
                cameras[0].Priority -= 3;
            }
        }

    }
}
