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
    float xVelocity;
    Rigidbody2D body;
    Vector2 playerVelocity;
    Animator animator;

    private void FixedUpdate()
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
        bool a = Physics2D.OverlapCircle(groundChecker.transform.position, 0.5f, LayerMask.GetMask("Ground"));
        Debug.Log(a);
        body.velocity = playerVelocity;

    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //Run();
        //ChangeSpriteDirection();
        //Jump();
    }

    //private void Run()
    //{
    //    xVelocity = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
    //    GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, 0f);
    //    GetComponent<Animator>().SetBool("isRunning", xVelocity != 0);
    //}

    private void ChangeSpriteDirection()
    {
        GetComponent<Transform>().localScale = new Vector3
            (Mathf.Sign(xVelocity), transform.localScale.y);
    }

    private void Jump()
    {
        //if (Input.GetButtonDown("Jump"))
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("press Jump");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, jumpForce * Time.deltaTime);
        }

    }
}
