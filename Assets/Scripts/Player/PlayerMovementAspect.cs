using UnityEngine;

public class PlayerMovementAspect : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private InputControl input;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float shiftMaxSpeed;
    [SerializeField] private float xIncreaseValue;
    [SerializeField] private float shiftXIncreaseValue;
    [SerializeField] private float xDecreaseValue;
    [SerializeField] private float changeDirectionValue;
    [SerializeField] private float shiftChangeDirectionValue;

    public float XVelocity
    {
        get { return body.velocity.x; }
    }

    public bool IsStopping
    {
        get
        {
            return XInput * body.velocity.x < 0;
        }
    }

    private bool IsShiftPressed => input.IsShiftPressed;

    public int XInput
    {
        get
        {
            var xInput = 0;
            switch (input.CurrentDirection)
            {
                case ControlDirection.Right:
                    xInput = 1;
                    break;
                case ControlDirection.Left:
                    xInput = -1;
                    break;
            }
            return xInput;
        }
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var xInput = XInput;
        if (xInput == 0)
            body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, 0, Time.deltaTime * xDecreaseValue), body.velocity.y);
        else
        {
            var currentMaxSpeed = IsShiftPressed ? shiftMaxSpeed : maxSpeed;
            var increaseValue = IsShiftPressed ? shiftXIncreaseValue : xIncreaseValue;
            if (IsStopping)
                increaseValue = IsShiftPressed ? shiftChangeDirectionValue : changeDirectionValue;
            body.velocity = new Vector2(Mathf.MoveTowards(body.velocity.x, currentMaxSpeed * xInput, Time.deltaTime * increaseValue), body.velocity.y);
        }
    }
}
