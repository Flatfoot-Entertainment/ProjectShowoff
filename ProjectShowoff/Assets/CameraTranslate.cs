using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTranslate : MonoBehaviour
{
    [SerializeField] private Transform[] cameraPositions;
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private Vector3 currentTargetPosition;

    private Tweener tweener;

    private void Start()
    {
        currentTargetPosition = cameraPositions[0].position;
        Camera.main.transform.position = currentTargetPosition;
    }

    public void MoveCamera()
    {
        if (tweener == null || !(tweener.IsActive() && tweener.IsPlaying()))
        {
            if (Camera.main.transform.position == cameraPositions[0].position)
            {
                currentTargetPosition = cameraPositions[1].position;
            }
            else if (Camera.main.transform.position == cameraPositions[1].position)
            {
                currentTargetPosition = cameraPositions[0].position;
            }
            tweener = Camera.main.transform.DOMove(currentTargetPosition, cameraMoveSpeed);
        }

    }
}
