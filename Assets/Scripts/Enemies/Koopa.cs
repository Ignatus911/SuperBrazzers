using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour, IEnemy, IBlockPushable
{
    public bool IsAlife { get; private set; } = true;
    [SerializeField] private bool isLookRight;
    [SerializeField] private EnemyDirectionAspect directionAspect;
    [SerializeField] private EnemyMovementAspect movementAspect;
    [SerializeField] private GameObject player;
    private PlayerDirectionAspect playerDirection;
    [SerializeField] private float koopaSpeed;
    [SerializeField] private float projectileKoopaSpeed;
    [SerializeField] private KoopaStatusController status;
    [SerializeField] private GameObject deatPoint;
    [SerializeField] private AudioClip dieClip;
    [SerializeField] private int getInShellScore = 400;
    [SerializeField] private int projectileScore = 1000;
    [SerializeField] private AnimationClip dieAnimation;


    private void Awake()
    {
        directionAspect.LookAt(isLookRight);
        playerDirection = player.GetComponent<PlayerDirectionAspect>();
    }

    public void Update() //ThinkAboutIt
    {
        if (IsAlife)
        {
            switch (status.Status)
            {
                case KoopaStatus.Usual:
                    movementAspect.Move(koopaSpeed);
                    break;
                case KoopaStatus.InShell:
                    movementAspect.Move(0);
                    break;
                case KoopaStatus.Projectile:
                    movementAspect.Move(projectileKoopaSpeed);
                    break;
            }
        }
        else
        {
            movementAspect.Move(0);
        }
    }

    public void Hit(GameObject hitter)
    {
        var score = getInShellScore;
        if (!IsAlife)
            return;
        AnimationClip animation;
        if (hitter.GetComponent<PlayerStatusController>() != null)
        {
            switch (status.Status)
            {
                case KoopaStatus.Usual:
                    status.HideInShell();
                    break;
                case KoopaStatus.InShell:
                    score = projectileScore;
                    CheckDirection();
                    status.ProjectileShell();
                    break;
                case KoopaStatus.Projectile:
                    score = projectileScore;
                    status.HideInShell();
                    break;
            }
            hitter.GetComponent<PlayerJumpAspect>().BounceOnEnemy();

        }
        else
            DieFromSuperPlayer(hitter);
        ScoreController.Instance.AddScore(score);
    }

    private void CheckDirection()
    {
        if (playerDirection.IsLookRight != directionAspect.IsLookRight)
            directionAspect.Rotate();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void DieFromSuperPlayer(GameObject hitter)
    {
        AudioManager.Instance.PlaySound(dieClip);//kickup score add
    }

    public void Push(GameObject pusher)
    {
        DieFromSuperPlayer(pusher);
    }
}
