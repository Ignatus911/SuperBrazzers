using UnityEngine;

public class MovementAI : MonoBehaviour, IAiState
{
    [SerializeField]
    private float xSpeed;

    [SerializeField] private EnemyMovementAspect movementAspect;

    public void DoLogic()
    {
        movementAspect.Move(xSpeed);
    }
}
