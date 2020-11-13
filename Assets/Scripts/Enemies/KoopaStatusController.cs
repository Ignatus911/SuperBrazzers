using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaStatusController : MonoBehaviour
{
    public KoopaStatus Status { get; private set; }
    [SerializeField] private Transform deathCollider;
    private BoxCollider2D boxCollider;
    private bool isWaiting;
    [SerializeField] private Animator selfAnimator;
    [SerializeField] private AnimationClip walking;
    [SerializeField] private AnimationClip inShellAnimation;
    [SerializeField] private AnimationClip projectileShell;

    private void Awake()
    {
        boxCollider = deathCollider.GetComponent<BoxCollider2D>();
    }
    public void GetOutOfTheShell()
    {
        Status = KoopaStatus.Usual;
        selfAnimator.Play(walking.name);
    }

    public void HideInShell()
    {
        Status = KoopaStatus.InShell;
        StartCoroutine(WaitForPush());
        selfAnimator.Play(inShellAnimation.name);
    }

    public void ProjectileShell()
    {
        Status = KoopaStatus.Projectile;
        ChangeIfWaiting();
        selfAnimator.Play(projectileShell.name);
    }

    private void ChangeIfWaiting()
    {
        if (isWaiting)
        {
            deathCollider.position = new Vector2(deathCollider.position.x, deathCollider.position.y + 100);
            boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y-100);
            isWaiting = false;
        }
    }

    IEnumerator WaitForPush()
    {
        deathCollider.position = new Vector2(deathCollider.position.x, deathCollider.position.y - 100);
        boxCollider.offset = new Vector2(boxCollider.offset.x, boxCollider.offset.y + 100);
        isWaiting = true;
        yield return new WaitForSeconds(5);
        if (isWaiting)
            GetOutOfTheShell();
        ChangeIfWaiting();
    }
}
