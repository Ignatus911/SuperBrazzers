using System.Collections;
using UnityEngine;

public class BoxDestroy : MonoBehaviour, BoxState
{
    private Animator animator;
    private bool isAlife = true;
    [SerializeField] private AudioClip destroyClip;
    [SerializeField] private JumpChecker headCheker;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void DoLogic()
    {
        if (!isAlife)
            return;
        isAlife = false;
        animator.Play("BoxDestroying");
        var headCollisionTarget = Physics2D.OverlapBox(headCheker.Checker.transform.position,
            new Vector2(headCheker.CheckerSize, Mathf.Epsilon), 0f, headCheker.Mask);
        if (headCollisionTarget != null)
        {
            var pushable = headCollisionTarget.GetComponent<IBlockPushable>();
            pushable?.Push(gameObject);
        }
        AudioManager.Instance.PlaySound(destroyClip);
        StartCoroutine(BoxExistCoroutine());
    }

    IEnumerator BoxExistCoroutine()
    {
        yield return new WaitForFixedUpdate();
        Destroy(GetComponent<BoxCollider2D>());
    }
}
