using UnityEngine;

public class EnemyMovementAspect : MonoBehaviour
{
    private Rigidbody2D body;

    [SerializeField] private EnemyDirectionAspect directionAspect;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed)
    {
        body.velocity = new Vector2(speed * (directionAspect.IsLookRight ? 1 : -1), body.velocity.y);
    }

    public void Jump(float speed)
    {
        body.velocity = new Vector2(body.velocity.x, speed);
    }
}
