using UnityEngine;

public class KeyboardInputControl : InputControl
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
            CurrentDirection = ControlDirection.Left;
        else
        {
            if (Input.GetKey(KeyCode.D))
                CurrentDirection = ControlDirection.Right;
            else
                CurrentDirection = ControlDirection.NoInput;
        }

    }
}
