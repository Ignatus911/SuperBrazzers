using UnityEngine;

public class KeyboardInputControl : InputControl
{
    private PlayerJumpAspect jumpAspect;
    public PlayerStatusController playerStatus;

    private void Awake()
    {
        jumpAspect = GetComponent<PlayerJumpAspect>();
        playerStatus = GetComponent<PlayerStatusController>();
    }

    private void Update()
    {
        if (TimeController.PlayerSetPause) return;
        if (playerStatus.Status == PlayerStatus.Big && (jumpAspect.IsGrounded || IsSeatKeyPressed))
            IsSeatKeyPressed = Input.GetKey(KeyCode.S);
        else
            IsSpacePressed = false;
        if (IsSeatKeyPressed && jumpAspect.IsGrounded)
        {
            DetectStates();
            CurrentDirection = ControlDirection.NoInput;
            return;
        }
        if (Input.GetKey(KeyCode.A))
            CurrentDirection = ControlDirection.Left;
        else
        {
            if (Input.GetKey(KeyCode.D))
                CurrentDirection = ControlDirection.Right;
            else
                CurrentDirection = ControlDirection.NoInput;
        }
        DetectStates();
    }

    private void DetectStates()
    {
        IsShiftPressed = Input.GetKey(KeyCode.LeftShift);
        IsSpacePressed = Input.GetKey(KeyCode.Space);
    }
}
