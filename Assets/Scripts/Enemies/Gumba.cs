using UnityEngine;

public class Gumba : MonoBehaviour, IEnemy
{
    public bool IsAlife { get; private set; } = true;

    [SerializeField] private bool isLookRight;
    [SerializeField] private EnemyDirectionAspect directionAspect;
    [SerializeField] private EnemyMovementAspect movementAspect;
    [SerializeField] private float speed;
    [SerializeField] private GameObject deatPoint;
    [SerializeField] private AudioClip dieClip;
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

    public void Hit()
    {
        IsAlife = false;
        Destroy(deatPoint);
        GetComponent<Animator>().SetBool("isDead", true);
        AudioManager.Instance.PlaySound(dieClip);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
