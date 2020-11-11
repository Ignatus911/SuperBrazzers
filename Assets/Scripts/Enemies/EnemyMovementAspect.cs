using UnityEngine;

public class EnemyMovementAspect : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] private EnemyDirectionAspect directionAspect;
    [SerializeField] private EnemyAbleToMoveAspect enemyAbleToMoveAspect;
    [SerializeField] private Transform collisionsChecker;
    [SerializeField] private LayerMask collisionsMask;
    private bool isAbleToMove;
 
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed)
    {
        if (isAbleToMove)
        {
            body.velocity = new Vector2(speed * (directionAspect.IsLookRight ? 1 : -1), body.velocity.y);

            if (Physics2D.OverlapCircle(collisionsChecker.transform.position, 0.01f, collisionsMask))
                directionAspect.Rotate();
        }
        else
            isAbleToMove = enemyAbleToMoveAspect.countIsAbleToMove();
    }

    public void Jump(float speed)
    {
        body.velocity = new Vector2(body.velocity.x, speed);
    }
}
