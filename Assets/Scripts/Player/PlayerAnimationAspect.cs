using JetBrains.Annotations;
using UnityEngine;

public class PlayerAnimationAspect : MonoBehaviour
{
    private Animator selfAnimator;

    [SerializeField] private PlayerMovementAspect movementAspect;
    [SerializeField] private PlayerSeatingAspect seatingAspect;
    [SerializeField] private PlayerJumpAspect playerJumpAspect;
    [SerializeField] private PlayerStatusController playerStatusController;
    [SerializeField] private float animationSpeedCorrector = 1;

    [SerializeField] private PlayerAnimationsSet animationSets;

    private AnimationClip lastClip;
    private bool freezeUpdateLogic;

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
        if (isRunning || TimeController.PlayerSetPause)
            selfAnimator.updateMode = AnimatorUpdateMode.Normal;
        else
            selfAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void OnChangeStatus(PlayerStatus status)
    {
        var currentAnimation = PlayerAnimationState.NoState;
        selfAnimator.enabled = true;
        switch (status)
        {
            case PlayerStatus.Big:
                currentAnimation = PlayerAnimationState.BecomeBig;
                freezeUpdateLogic = true;
                break;
            case PlayerStatus.Small:
                currentAnimation = PlayerAnimationState.BecomeBig;
                freezeUpdateLogic = true;
                break;
            case PlayerStatus.Dead:
                currentAnimation = PlayerAnimationState.Dead;
                freezeUpdateLogic = true;
                break;
        }
        FinishAnimate(currentAnimation);
    }

    [UsedImplicitly]
    private void UnFreezeUpdateLogic()
    {
        freezeUpdateLogic = false;
    }

    private void FinishAnimate(PlayerAnimationState currentState)
    {
        var currentClip = animationSets.GetClip(currentState, playerStatusController.Status == PlayerStatus.Big);
        if (currentClip != null && lastClip != currentClip)
        {
            lastClip = currentClip;
            selfAnimator.Play(currentClip.name, -1, 0);
        }
    }

    private void Update()
    {
        if (freezeUpdateLogic)
            return;
        selfAnimator.speed = GetAnimationSpeed();

        PlayerAnimationState currentAnimation = PlayerAnimationState.Idle;

        if (seatingAspect.IsSeating)
        {
            currentAnimation = PlayerAnimationState.Sit;
            FinishAnimate(currentAnimation);
            return;
        }

        if (!playerJumpAspect.IsGrounded)
        {
            currentAnimation = PlayerAnimationState.Jump;
            FinishAnimate(currentAnimation);
            return;
        }

        if (IsRunning && playerJumpAspect.IsGrounded)
        {
            currentAnimation = movementAspect.IsStopping ? PlayerAnimationState.Stopping : PlayerAnimationState.Run;
            FinishAnimate(currentAnimation);
            return;
        }

        FinishAnimate(currentAnimation);
    }

    private float GetAnimationSpeed()
    {
        if (IsRunning)
            return Mathf.Clamp(Mathf.Abs(movementAspect.XVelocity) / animationSpeedCorrector, 0.5f, 1f);
        return 1;
    }
}
