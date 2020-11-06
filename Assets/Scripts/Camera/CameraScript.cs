using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private InputControl input;
    [SerializeField] private float deltaScroll;
    [SerializeField] private float distanceToCamera = 4;
    private float xCamera;
    private float xPlayer;
    private float xPlayerPreviousFrame;
    private float xTarget;

    void Update()
    {
        xPlayer = player.transform.position.x;
        xCamera = transform.position.x;
        if (xCamera <= xPlayer)
            xTarget = xPlayer;
        else
        {
            var distance = Mathf.Abs(xPlayer - xCamera);
            if (distance <= distanceToCamera && Mathf.Abs(xPlayer - xPlayerPreviousFrame) > Mathf.Epsilon)
            {
                if (input.CurrentDirection == ControlDirection.Right)
                    xTarget = xPlayer + distance + deltaScroll * Time.deltaTime;
            }
        }
        transform.position = new Vector3(xTarget, 0f, -10f);
        xPlayerPreviousFrame = xPlayer;
    }
}
