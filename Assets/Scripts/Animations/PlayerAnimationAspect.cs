using UnityEngine;

public class PlayerAnimationAspect : MonoBehaviour
{
    private Animator selfAnimator;

    [SerializeField] private PlayerMovementAspect movementAspect;
    [SerializeField] private PlayerJumpAspect playerJumpAspect;
    [SerializeField] private float animationSpeedCorrector = 1;

    private bool IsRunning
    {
        get { return Mathf.Abs(movementAspect.XVelocity) > Mathf.Epsilon; }
    }

    private void Awake()
    {
        selfAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        selfAnimator.speed = GetAnimationSpeed();
        selfAnimator.SetBool("isRunning", IsRunning && playerJumpAspect.IsGrounded);
        selfAnimator.SetBool("isStoping", movementAspect.IsStopping);
        selfAnimator.SetBool("isJumping", !playerJumpAspect.IsGrounded);
    }

    private float GetAnimationSpeed()
    {
        if (IsRunning)
            return Mathf.Clamp(Mathf.Abs(movementAspect.XVelocity) / animationSpeedCorrector, 0.5f, 1f);
        return 1;
    }
}
