using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private AILogic logic;
    private IAiState currentState;
    [SerializeField] private MovementAI movementAI;
    [SerializeField] private JumpingAI jumpingAi;

    private void Update()
    {
        switch (logic)
        {
            case AILogic.Moving:
                currentState = movementAI;
                break;
            case AILogic.Jumping:
                currentState = jumpingAi;
                break;
        }
        currentState.DoLogic();
    }
}

public enum AILogic
{
    Moving,
    Jumping
}
