using UnityEngine;

public class Gumba : MonoBehaviour
{
    [SerializeField] private bool isLookRight;
    [SerializeField] private EnemyDirectionAspect directionAspect;
    [SerializeField] private EnemyMovementAspect movementAspect;
    [SerializeField] private float speed;

    private void Awake()
    {
        directionAspect.LookAt(isLookRight);
    }

    public void Update()
    {
        movementAspect.Move(speed);
    }
}
