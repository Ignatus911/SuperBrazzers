using UnityEngine;

public class GamePadInputControl : InputControl
{
    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal < -Mathf.Epsilon)
            CurrentDirection = ControlDirection.Left;
        else
        {
            if (horizontal > Mathf.Epsilon)
                CurrentDirection = ControlDirection.Right;
            else
                CurrentDirection = ControlDirection.NoInput;
        }

    }
}
