using UnityEngine;

public class JumpingAI : MonoBehaviour, IAiState
{
    [SerializeField]
    private float ySpeed;

    [SerializeField] private float jumpDelay = 0.5f;
    private float currentJumpDelayValue = 0;

    [SerializeField] private EnemyMovementAspect movementAspect;

    public void DoLogic()
    {
        currentJumpDelayValue -= Time.deltaTime;
        if (currentJumpDelayValue < 0)
        {
            movementAspect.Jump(ySpeed);
            currentJumpDelayValue = jumpDelay;
        }
    }
}