using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbleToMoveAspect : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float offset; 
    private float halfScreen;
    private float myPositionX;

    private void Start()
    {
        var height = 2f * mainCamera.GetComponent<Camera>().orthographicSize;
        var cameraWidth = height * mainCamera.GetComponent<Camera>().aspect;
        halfScreen = cameraWidth / 2;
        myPositionX = GetComponent<Transform>().position.x;
    }

    public bool countIsAbleToMove()
    {
        var distanceToMove = halfScreen + offset;
        var isAbleToMove =  myPositionX - mainCamera.transform.position.x < distanceToMove;
        return isAbleToMove;
    }
}
