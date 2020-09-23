using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxMovementSpeed = 100f;
    [SerializeField] float movementIncrease = 10f;
    [SerializeField] float movementDecrease = 10f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject groundChecker;
    [SerializeField, Range(0.1f,1f)] float boxSide = 0.81f;
    float xVelocity;
    Rigidbody2D body;
    Vector2 playerVelocity;
    Animator animator;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            xVelocity = -1;
            sprite.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xVelocity = 1;
            sprite.flipX = false;
        }
        else xVelocity = 0;

        playerVelocity.x += xVelocity * movementIncrease * Time.fixedDeltaTime;
        if(playerVelocity.x > maxMovementSpeed)
        {
            playerVelocity.x = maxMovementSpeed;
        }
        else if(playerVelocity.x < -maxMovementSpeed)
        {
            playerVelocity.x = -maxMovementSpeed;
        }

        animator.SetBool("isRunning", playerVelocity.x != 0);
        animator.SetBool("isStoping", xVelocity * playerVelocity.x < 0);

        if(xVelocity == 0)
        {
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, 0, movementDecrease * Time.deltaTime);
        }

        playerVelocity.y = body.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerVelocity.y = jumpForce;
            Debug.Log("Spase pressed " + playerVelocity);
        }
        bool a = Physics2D.OverlapBox(groundChecker.transform.position, new Vector2(boxSide, boxSide), LayerMask.GetMask("Ground"));
        bool b = Physics2D.OverlapCircle(groundChecker.transform.position, 1f, LayerMask.GetMask("Ground"));
        Debug.Log("box collision is " + a);
        Debug.Log("circle collision is "+b);

        body.velocity = playerVelocity;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundChecker.transform.position, new Vector3(boxSide, boxSide, 0f));
    }
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

}
