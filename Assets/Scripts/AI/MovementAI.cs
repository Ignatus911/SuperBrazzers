using UnityEngine;

public class MovementAI : MonoBehaviour
{
    [SerializeField]
    private float xSpeed;

    [SerializeField] private EnemyMovementAspect movementAspect;

    public void DoLogic()
    {
        movementAspect.Move(xSpeed);
    }
}
