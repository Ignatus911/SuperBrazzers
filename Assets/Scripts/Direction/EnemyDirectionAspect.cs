using UnityEngine;

public class EnemyDirectionAspect : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public bool IsLookRight { get; set; } = true;

    public void LookAt(bool right)
    {
        if (IsLookRight == right)
            return;
        IsLookRight = right;
        sprite.flipX = !IsLookRight;
    }
}
