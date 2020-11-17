using UnityEngine;

public class BoxJump : MonoBehaviour, BoxState
{
    private Animator animator;

    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private JumpChecker headCheker;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DoLogic()
    {
        animator.Play("BoxJump");
        AudioManager.Instance.PlaySound(jumpClip);

        var headCollisionTarget = Physics2D.OverlapBox(headCheker.Checker.transform.position,
            new Vector2(headCheker.CheckerSize, Mathf.Epsilon), 0f, headCheker.Mask);
        if (headCollisionTarget != null)
        {
            var pushable = headCollisionTarget.GetComponent<IBlockPushable>();
            pushable?.Push(gameObject, player.GetComponent<PlayerDirectionAspect>().IsLookRight);
        }
    }
}
