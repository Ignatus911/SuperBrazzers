using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxMovementSpeedInPeace = 5f;
    [SerializeField] private float movementIncrease = 5f;
    [SerializeField] private float movementDecrease = 18f;
    [SerializeField] private float maxJumpForce = 14f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float addJumpForce = 1f; 
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject groundChecker;
    [SerializeField, Range(0.1f,1f)] private float boxSide = 0.84f;
    private float maxMovementSpeed;
    private bool justJump = false;
    private bool onGround;
    private float xVelocity;
    private Rigidbody2D body;
    private Vector2 playerVelocity;
    private Animator animator;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            xVelocity = -1;
            if (onGround)
            {
                sprite.flipX = true;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xVelocity = 1;
            if (onGround)
            {
                sprite.flipX = false;
            }
        }
        else xVelocity = 0;

        playerVelocity.x += xVelocity * movementIncrease * Time.fixedDeltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxMovementSpeed = maxMovementSpeedInPeace * 2;
        }
        else  maxMovementSpeed = maxMovementSpeedInPeace;
        if (playerVelocity.x > maxMovementSpeed)
        {
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, maxMovementSpeed, 0.7f * movementDecrease * Time.deltaTime) ;
        }
        else if(playerVelocity.x < -maxMovementSpeed)
        {
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, -maxMovementSpeed, 0.7f * movementDecrease * Time.deltaTime );
        }

        if(xVelocity == 0 | xVelocity * playerVelocity.x < 0)
        {
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, 0, movementDecrease * Time.deltaTime);
        }

        playerVelocity.y = body.velocity.y;
        onGround = Physics2D.OverlapBox(groundChecker.transform.position, new Vector2(boxSide, Mathf.Epsilon), 0f, LayerMask.GetMask("Ground"));

        if (onGround & Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y = jumpForce;
            justJump = true;
        }

        if (Input.GetKey(KeyCode.Space) & justJump & playerVelocity.y >= 0)
        {
            Debug.Log("Space pressed");
            playerVelocity.y += addJumpForce ;
            if(playerVelocity.y >= maxJumpForce)
            {
                justJump = false ;
            }
            Debug.Log(playerVelocity.y);
        }

        body.velocity = playerVelocity;

        animator.SetBool("isRunning", playerVelocity.x != 0 & onGround);
        animator.SetBool("isStoping", xVelocity * playerVelocity.x < 0);
        animator.SetBool("isJumping", !onGround);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundChecker.transform.position, new Vector3(boxSide, Mathf.Epsilon, 0f));
    }
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

}
