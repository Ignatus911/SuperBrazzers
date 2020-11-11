using System;
using UnityEngine;

public class PlayerJumpAspect : MonoBehaviour
{
    public static Action<bool> OnBeginFallFromCorner { get; set; }

    private Rigidbody2D body;
    [SerializeField] private InputControl input;
    [SerializeField] private PlayerDirectionAspect playerDirectionAspect;
    [SerializeField] private float ySpeed = 2;
    [SerializeField] private JumpChecker groundCheker;
    [SerializeField] private JumpChecker headCheker;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField, Range(0.5f,30f)] private float jumpFromEnemy = 2f;

    [SerializeField] private float jumpTime = 0.35f;
    private float currentJumpTime;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerDirectionAspect = GetComponent<PlayerDirectionAspect>();
    }

    private bool isAbleJump;
    private bool isBeginJump;

    public void BounceOnEnemy()
    {
        body.velocity = new Vector2(body.velocity.x, jumpFromEnemy);
    }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapBox(groundCheker.Checker.transform.position,
            new Vector2(groundCheker.CheckerSize, Mathf.Epsilon), 0f, groundCheker.Mask);
        var headCollisionTarget = Physics2D.OverlapBox(headCheker.Checker.transform.position,
            new Vector2(headCheker.CheckerSize, Mathf.Epsilon), 0f, headCheker.Mask);
        if (IsGrounded && !input.IsSpacePressed)
        {
            isAbleJump = true;
            isBeginJump = true;
            currentJumpTime = jumpTime;
            OnBeginFallFromCorner?.Invoke(false);
        }

        if (headCollisionTarget && !IsGrounded && body.velocity.y > 0)
        {
            isAbleJump = false;
            var headCollisionLogic = headCollisionTarget.GetComponent<IHeadHitting>();
            if (headCollisionLogic != null)
                headCollisionLogic.Hit(gameObject);
        }

        if (!IsGrounded && !input.IsSpacePressed && isAbleJump)
        {
            isAbleJump = false;
            if (Math.Abs(currentJumpTime - jumpTime) < Mathf.Epsilon)
                OnBeginFallFromCorner?.Invoke(true);
        }

        if (input.IsSpacePressed && isAbleJump)
        {
            if (isBeginJump)
            {
                AudioManager.Instance.PlaySound(jumpClip);
                isBeginJump = false;
            }

            body.velocity = new Vector2(body.velocity.x, ySpeed);
            currentJumpTime -= Time.deltaTime;
            if (currentJumpTime <= 0)
                isAbleJump = false;
        }

        if (playerDirectionAspect.IsLookRight)
            groundCheker.Checker.localPosition = new Vector3(-groundCheker.offsetChecker, -1, 0);
        else
            groundCheker.Checker.localPosition = new Vector3(groundCheker.offsetChecker, -1, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(headCheker.Checker.transform.position, headCheker.CheckerSize);
        Gizmos.DrawCube(groundCheker.Checker.transform.position, new Vector3(groundCheker.CheckerSize,0.1f,0.1f));
    }
}

[Serializable]
public class JumpChecker
{
    public float CheckerSize;
    public LayerMask Mask;
    public Transform Checker;
    public float offsetChecker;
}