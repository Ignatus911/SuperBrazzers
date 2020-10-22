using UnityEngine;

public class PlayerDirectionAspect : MonoBehaviour
{
    [SerializeField] private InputControl control;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private PlayerJumpAspect jumpAspect;

    public bool IsLookRight { get; set; } = true;

    public void LookAt(bool right)
    {
        if (IsLookRight == right)
            return;
        IsLookRight = right;
        //if (jumpAspect.IsGrounded)
            sprite.flipX = !IsLookRight;
    }

    private void Update()
    {
        switch (control.CurrentDirection)
        {
            case ControlDirection.Left:
                LookAt(false);
                break;
            case ControlDirection.Right:
                LookAt(true);
                break;
        }
    }
}
