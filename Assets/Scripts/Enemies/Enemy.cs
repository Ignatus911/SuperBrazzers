using System.Collections;
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
    private bool isWaiting = true;
    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>().GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        distantion = player.position.x - body.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
        {
            distantion = player.position.x - body.position.x;
            if (Mathf.Abs(distantion) <= distantionForAction)
                isWaiting = false;
        }

        else
        if (isAlive)
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

            var collisionWithPlayer = Physics2D.OverlapBox(deathCollider.transform.position, new Vector2(0f, deathColliderSize), 0f, LayerMask.GetMask("Player"));
            if (collisionWithPlayer)
            {
                animator.SetBool("isDead", true);
                body.isKinematic = true;
                GetComponent<EdgeCollider2D>().enabled = false;
                Destroy(deathCollider);
                Destroy(gameObject, 0.5f);
                collisionWithPlayer.GetComponent<Player>().BounceOnEnemy();
                isAlive = false;
            }
        }
    } 

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(deathCollider.transform.position, new Vector3(deathColliderSize, Mathf.Epsilon, 0f));
    }

}
