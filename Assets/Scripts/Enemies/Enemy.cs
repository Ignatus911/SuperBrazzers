﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D player;
    private Rigidbody2D body;
    private Animator animator;
    [SerializeField] private float distantionForAction = 14f;
    [SerializeField] private float speed = 400f;
    [SerializeField] private GameObject collisionsChecker;
    [SerializeField] private GameObject deathCollider;
    [SerializeField] private float deathColliderSize = 0.8f;
    [SerializeField] private SpriteRenderer sprite;
    private float direction = -1f;
    private float distantion;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        distantion = player.position.x - body.position.x;
        Debug.Log(distantion);
        Debug.Log("player.position.x = " + player.position.x);
        Debug.Log("body.position.x = " + body.position.x);
        if (Mathf.Abs(distantion) <= distantionForAction)
        {
            body.velocity = new Vector2(speed, 0f) * direction * Time.deltaTime;
            if (Physics2D.OverlapCircle(collisionsChecker.transform.position, 0.01f, LayerMask.GetMask("Environment")))
            {
                sprite.flipX = true;
                direction = -direction;
                if (direction < 0)
                {
                    collisionsChecker.transform.position = new Vector2(collisionsChecker.transform.position.x - 1.1f, collisionsChecker.transform.position.y);
                }
                else
                {
                    collisionsChecker.transform.position = new Vector2(collisionsChecker.transform.position.x + 1.1f, collisionsChecker.transform.position.y);
                }

            }

            if (Physics2D.OverlapBox(deathCollider.transform.position, new Vector2(0f, deathColliderSize), 0f, LayerMask.GetMask("Player")))
            {
                speed = 0f;
                animator.SetBool("isDead", true);
                Destroy(gameObject, 1f);
            }
        }
    } 

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(deathCollider.transform.position, new Vector3(deathColliderSize, Mathf.Epsilon, 0f));
    }

}
