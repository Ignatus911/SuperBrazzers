using UnityEngine;

public class PlayerAnimationAspect : MonoBehaviour
{
    private Animator selfAnimator;

    [SerializeField] private PlayerMovementAspect movementAspect;
    [SerializeField] private PlayerSeatingAspect seatingAspect;
    [SerializeField] private PlayerJumpAspect playerJumpAspect;
    [SerializeField] private PlayerStatusController playerStatusController;
    [SerializeField] private float animationSpeedCorrector = 1;

    private bool IsRunning
    {
        get { return Mathf.Abs(movementAspect.XVelocity) > Mathf.Epsilon; }
    }

    private void Awake()
    {
        selfAnimator = GetComponent<Animator>();
        PlayerStatusController.OnChangeStatus += OnChangeStatus;
        TimeController.OnTimeChanged += OnTimeChanged;
        PlayerJumpAspect.OnBeginFallFromCorner += OnBeginFallFromCorner;
    }

    private void OnDestroy()
    {
        PlayerStatusController.OnChangeStatus -= OnChangeStatus;
        TimeController.OnTimeChanged -= OnTimeChanged;
        PlayerJumpAspect.OnBeginFallFromCorner -= OnBeginFallFromCorner;
    }

    private void OnBeginFallFromCorner(bool value)
    {
        selfAnimator.enabled = !value;
    }

    private void OnTimeChanged(bool isRunning)
    {
        if (isRunning)
            selfAnimator.updateMode = AnimatorUpdateMode.Normal;
        else
            selfAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void OnChangeStatus(PlayerStatus status)
    {
        selfAnimator.enabled = true;
        switch (status)
        {
            case PlayerStatus.Big:
                selfAnimator.SetTrigger("becomeBig");
                break;
            case PlayerStatus.Small:
                selfAnimator.SetTrigger("becomeSmall");
                break;
            case PlayerStatus.Dead:
                selfAnimator.SetTrigger("isDead");
                break;
        }
    }

    private void Update()
    {
        selfAnimator.speed = GetAnimationSpeed();
        selfAnimator.SetBool("isRunning", IsRunning && playerJumpAspect.IsGrounded);
        selfAnimator.SetBool("isStoping", movementAspect.IsStopping);
        selfAnimator.SetBool("isJumping", !playerJumpAspect.IsGrounded);
        selfAnimator.SetBool("isSeating", seatingAspect.IsSeating);
    }

    private float GetAnimationSpeed()
    {
        if (IsRunning)
            return Mathf.Clamp(Mathf.Abs(movementAspect.XVelocity) / animationSpeedCorrector, 0.5f, 1f);
        return 1;
    }
}
