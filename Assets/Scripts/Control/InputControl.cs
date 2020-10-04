using UnityEngine;

public abstract class InputControl : MonoBehaviour
{
    public ControlDirection CurrentDirection { get; protected set; } = ControlDirection.Right;

    public bool IsShiftPressed { get; protected set; } = false;

    public bool IsSpacePressed { get; protected set; } = false;
}
