using UnityEngine;

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

    public void Hit(GameObject hitter)
    {
        if (!IsAlife)
            return;
        AnimationClip animation;
        IsAlife = false;
        Destroy(deatPoint);

        //switch (hitter.tag)
        //{
        //    case ("Turtle"):
        //        animation = kickedUpAnimation;
        //        ScoreController.Instance.AddScore(scoreByTurtleKilling, false, true);
        //        break;
        //    case ("Player"):
        //        hitter.GetComponent<PlayerJumpAspect>().BounceOnEnemy();
        //        animation = smashedAnimation;
        //        ScoreController.Instance.AddScore(scoreByPlayerKilling, true, false);
        //        break;
        //    default:
        //        animation = kickedUpAnimation;
        //        ScoreController.Instance.AddScore(scoreByPlayerKilling, true, false);
        //        break;
        //}
        if (hitter.tag == playerTag)
        {
            hitter.GetComponent<PlayerJumpAspect>().BounceOnEnemy();
            animation = smashedAnimation;
        }
        else
            animation = kickedUpAnimation;
        ScoreController.Instance.AddScore(scoreByPlayerKilling, hitter.tag, transform);
        GetComponent<Animator>().Play(animation.name);
        AudioManager.Instance.PlaySound(dieClip);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Push(GameObject pusher)
    {
        deathFromPushedBlock = true;
        Hit(pusher);
    }
}
