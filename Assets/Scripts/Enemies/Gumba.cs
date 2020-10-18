using UnityEngine;

public class Gumba : MonoBehaviour, IEnemy
{
    public bool IsAlife { get; private set; } = true;

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
        if (!IsAlife)
            return;

        movementAspect.Move(speed);
    }

    public void Hit()
    {
        throw new System.NotImplementedException();
    }
}
