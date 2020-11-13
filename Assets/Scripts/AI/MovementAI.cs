using UnityEngine;

public class MovementAI : MonoBehaviour
{
    private float xSpeed;

    [SerializeField] private EnemyMovementAspect movementAspect;

    public void DoLogic()
    {
        movementAspect.Move(xSpeed);
    }
}
