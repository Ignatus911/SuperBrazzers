using System.Collections;
using UnityEngine;

public class BoxDestroy : MonoBehaviour, BoxState
{
    private Animator animator;
    private bool isAlife = true;
    [SerializeField] private AudioClip destroyClip;

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
        AudioManager.Instance.Play(destroyClip);
        StartCoroutine(BoxExistCoroutine());
    }

    IEnumerator BoxExistCoroutine()
    {
        yield return new WaitForFixedUpdate();
        Destroy(GetComponent<BoxCollider2D>());
    }
}
