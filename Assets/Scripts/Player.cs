﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float maxMovementSpeedInPeace = 5f;
    [SerializeField] float movementIncrease = 5f;
    [SerializeField] float movementDecrease = 18f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject groundChecker;
    [SerializeField, Range(0.1f,1f)] float boxSide = 0.84f;
    float maxMovementSpeed;
    bool onGround;
    float xVelocity;
    Rigidbody2D body;
    Vector2 playerVelocity;
    Animator animator;

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
        }

        body.velocity = playerVelocity;

        animator.SetBool("isRunning", playerVelocity.x != 0 & onGround);
        animator.SetBool("isStoping", xVelocity * playerVelocity.x < 0);
        animator.SetBool("isJumping", !onGround);

        // delete
        Debug.Log(maxMovementSpeed);
        Debug.Log(playerVelocity.x);
        // delete
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
