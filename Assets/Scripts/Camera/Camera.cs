using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private InputControl input;
    [SerializeField] private float deltaScroll;
    private Transform camera;
    private float xCamera;
    private float xPlayer;
    private float xPlayerPreviousFrame;
    private float xTarget;

    private void Awake()
    {
        camera = GetComponent<Transform>();
    }



    void Update()
    {
        xPlayer = player.transform.position.x;
        xCamera = camera.transform.position.x;
        var distance = Mathf.Abs(xPlayer - xCamera);
        if (xCamera <= xPlayer)
            xTarget = xPlayer;
        else if (distance <= 4)
        {
            if (Mathf.Abs(xPlayer - xPlayerPreviousFrame) > Mathf.Epsilon && input.CurrentDirection == ControlDirection.Right )
                xTarget = xPlayer + distance + deltaScroll * Time.deltaTime;
        }
        camera.transform.position = new Vector3(xTarget, 0f, -10f);
        //Debug.Log("xPlayer: " + xPlayer);
        //Debug.Log("xCamera: " + xCamera);
        //Debug.Log("xTarget: " + xTarget);
        //Debug.Log("Distance: " + (xCamera - xPlayer));
        xPlayerPreviousFrame = xPlayer;
    }
}
