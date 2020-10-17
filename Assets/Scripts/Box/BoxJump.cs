using UnityEngine;

public class BoxJump : MonoBehaviour, BoxState
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DoLogic()
    {
        animator.Play("BoxJump");
    }
}
