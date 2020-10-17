using System;
using UnityEngine;

public class PlayerJumpAspect : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private InputControl input;
    [SerializeField] private JumpChecker groundCheker;
    [SerializeField] private JumpChecker headCheker;
    [SerializeField] private float ySpeed = 2;

    [SerializeField]
    private float jumpTime = 0.35f;
    private float currentJumpTime;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private bool isAbleJump;

    private void Update()
    {
        IsGrounded = Physics2D.OverlapBox(groundCheker.Checker.transform.position, new Vector2(groundCheker.CheckerSize, Mathf.Epsilon), 0f, groundCheker.Mask);
        var headCollisionTarget = Physics2D.OverlapBox(headCheker.Checker.transform.position, new Vector2(headCheker.CheckerSize, Mathf.Epsilon), 0f, headCheker.Mask);
        if (IsGrounded && !input.IsSpacePressed)
        {
            isAbleJump = true;
            currentJumpTime = jumpTime;
        }

        if (headCollisionTarget)
        {
            isAbleJump = false;
            var headCollisionLogic = headCollisionTarget.GetComponent<IHeadHitting>();
            if (headCollisionLogic != null)
                headCollisionLogic.Hit(gameObject);
        }

        if (input.IsSpacePressed && isAbleJump)
        {
            body.velocity = new Vector2(body.velocity.x, ySpeed);
            currentJumpTime -= Time.deltaTime;
            if (currentJumpTime <= 0)
                isAbleJump = false;
        }
    }
}

[Serializable]
public class JumpChecker
{
    public float CheckerSize;
    public LayerMask Mask;
    public Transform Checker;
}