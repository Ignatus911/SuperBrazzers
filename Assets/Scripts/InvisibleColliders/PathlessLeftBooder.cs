using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathlessLeftBooder : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private GameObject pathlessColliderPrefab;
    GameObject pathlessCollider;
    private float halfScreen;

    private void Start()
    {
        pathlessCollider = Instantiate(pathlessColliderPrefab);
    }

    private void Update()
    {
        var height = 2f * GetComponent<Camera>().orthographicSize;
        var cameraWidth = height * GetComponent<Camera>().aspect;
        halfScreen = cameraWidth / 2;
        var xPositioin = transform.position.x - halfScreen + offset;
        pathlessCollider.transform.position = new Vector3(xPositioin, transform.position.y, 0);
    }
}
