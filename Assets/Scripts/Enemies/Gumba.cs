﻿using UnityEngine;

public class Gumba : MonoBehaviour, IEnemy, IBlockPushable
{
    public bool IsAlife { get; private set; } = true;

    [SerializeField] private bool isLookRight;
    [SerializeField] private EnemyDirectionAspect directionAspect;
    [SerializeField] private EnemyMovementAspect movementAspect;
    [SerializeField] private float speed;
    [SerializeField] private GameObject deatPoint;
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private int scoreByPlayerKilling = 100;
    [SerializeField] private int scoreByTurtleKilling = 500;
    [SerializeField] private AnimationClip smashedAnimation;
    [SerializeField] private AnimationClip kickedUpAnimation;
    private string playerTag = "Player";
    private string turtleTag = "Turtle";
    private bool deathFromPushedBlock = false;

    private void Awake()
    {
        directionAspect.LookAt(isLookRight);
    }

    public void Update()
    {
        if (IsAlife)
            movementAspect.Move(speed);
        else
        {
            movementAspect.Move(0);
        }
    }

    public void Hit(GameObject hitter, bool hitterDirection)
    {
        if (!IsAlife)
            return;
        AnimationClip animation;
        IsAlife = false;
        Destroy(deatPoint);
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        if (hitter.GetComponent<PlayerStatusController>() != null)
        {
            if (hitter.GetComponent<PlayerStatusController>().IsSuper)
                animation = kickedUpAnimation;
            else
            {
                hitter.GetComponent<PlayerJumpAspect>().BounceOnEnemy();
                animation = smashedAnimation;
            }
        }
        else
            animation = kickedUpAnimation;
        if (!hitterDirection)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        ScoreController.Instance.AddScore(scoreByPlayerKilling, hitter.tag, transform);
        GetComponent<Animator>().Play(animation.name);
        AudioManager.Instance.PlaySound(dieClip);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void DieFromSuperPlayer(GameObject hitter, bool hitterDirection)
    {
        Hit(hitter, hitterDirection);
    }

    public void Push(GameObject pusher, bool hitterDirection)
    {
        deathFromPushedBlock = true;
        Hit(pusher, hitterDirection);
    }
}
