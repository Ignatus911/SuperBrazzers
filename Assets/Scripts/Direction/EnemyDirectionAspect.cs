using UnityEngine;

public class EnemyDirectionAspect : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private Transform collisionChecker;

    public bool IsLookRight { get; private set; } = true;

    public void Rotate()
    {
        LookAt(!IsLookRight);
    }

    public void LookAt(bool right)
    {
        if (IsLookRight == right)
            return;
        IsLookRight = right;
        sprite.flipX = !IsLookRight;
        collisionChecker.localPosition = new Vector3(Mathf.Abs(collisionChecker.localPosition.x) * (IsLookRight ? 1 : -1), collisionChecker.localPosition.y, collisionChecker.localPosition.z);
    }
}
