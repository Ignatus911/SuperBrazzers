using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxMovementSpeedInPeace = 5f;
    [SerializeField] private float movementIncrease = 5f;
    [SerializeField] private float movementDecrease = 18f;
    [SerializeField] private float changeDirectionDecrease = 18f;
    [SerializeField] private float brakingDecreaseToWalk = 13f;
    [SerializeField] private float maxJumpForce = 15.3f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float jumpFromEnemy = 15f;
    [SerializeField] private float addJumpForce = 100f; 
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject groundChecker;
    [SerializeField] private GameObject upHitChecker;
    [SerializeField, Range(0.1f,1f)] private float groundCheckerSize = 0.84f;
    [SerializeField, Range(0.1f,1f)] private float upperCheckerSize = 0.84f;
    private int enemiesLayer = 10;
    private float maxMovementSpeed;
    private bool isAlive = true;
    private bool justJump = false;
    private bool onGround;
    private bool hitOnJump;
    private float xVelocity;
    private Rigidbody2D body;
    private Animator animator;

    private void Update()
    {
        if (isAlive)
        {
            var playerVelocity = body.velocity;
            if (Input.GetKey(KeyCode.A))
                xVelocity = -1;
            else
            {
                if (Input.GetKey(KeyCode.D))
                    xVelocity = 1;
                else
                    xVelocity = 0;
            }

            if (onGround && Mathf.Abs(xVelocity) > 0)
                sprite.flipX = xVelocity < 0;

            playerVelocity.x += xVelocity * movementIncrease * Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftShift))
                maxMovementSpeed = maxMovementSpeedInPeace * 2;
            else
                maxMovementSpeed = maxMovementSpeedInPeace;

            if (Mathf.Abs(playerVelocity.x) > Mathf.Abs(maxMovementSpeed))
                playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, maxMovementSpeed, brakingDecreaseToWalk * Time.deltaTime);

            if (xVelocity == 0)
            {
                playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, 0, movementDecrease * Time.deltaTime);
            }
            else
            {
                if (xVelocity * playerVelocity.x < 0)
                    playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, 0, changeDirectionDecrease * Time.deltaTime);
            }

            playerVelocity.y = body.velocity.y;

            onGround = Physics2D.OverlapBox(groundChecker.transform.position, new Vector2(groundCheckerSize, Mathf.Epsilon), 0f, LayerMask.GetMask("Ground", "Environment"));
            hitOnJump = Physics2D.OverlapBox(upHitChecker.transform.position, new Vector2(upperCheckerSize, Mathf.Epsilon), 0f, LayerMask.GetMask("Environment"));

            if (onGround && Input.GetKeyDown(KeyCode.Space))
            {
                playerVelocity.y = jumpForce;
                justJump = true;
            }

            if (Input.GetKey(KeyCode.Space) && justJump)
            {
                Debug.Log("Space pressed");
                playerVelocity.y += addJumpForce * Time.deltaTime;
                if (playerVelocity.y >= maxJumpForce || hitOnJump)
                    justJump = false;
            }

            if (Input.GetKeyUp(KeyCode.Space))
                justJump = false;

            body.velocity = playerVelocity;

            animator.SetBool("isRunning", playerVelocity.x != 0 && onGround);
            animator.SetBool("isStoping", xVelocity * playerVelocity.x < 0);
            animator.SetBool("isJumping", !onGround);
        }
    }

    public void BounceOnEnemy()
    {
        body.velocity = new Vector2(0f, jumpFromEnemy);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == enemiesLayer)
            PlayerDeath();

    }

    private void PlayerDeath()
    {
        isAlive = false;
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.SetBool("isDead", true);
        Time.timeScale = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundChecker.transform.position, new Vector3(groundCheckerSize, Mathf.Epsilon, 0f));
    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

}
