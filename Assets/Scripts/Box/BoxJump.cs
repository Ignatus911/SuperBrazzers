using UnityEngine;

public class BoxJump : MonoBehaviour, BoxState
{
    private Animator animator;

    [SerializeField] private AudioClip jumpClip;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DoLogic()
    {
        animator.Play("BoxJump");
        AudioManager.Instance.Play(jumpClip);
    }
}
