using System;
using UnityEngine;

public class Star : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private Animator starAnimator;
    [SerializeField] private AudioClip invictableMusic;
    [SerializeField] private AudioClip bonusClip;
    private bool isMoving = false;
    [SerializeField] private float speed;
    [SerializeField] private float bounceForce;
    [SerializeField] private EnemyMovementAspect movementLogic;
    [SerializeField] private GroundChecker groundCheker;
    private bool onGround;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        starAnimator.Play("Show");
        AudioManager.Instance.PlaySound(bonusClip);
    }

    private void Update()
    {
        if (!isMoving)
            return;
        onGround = Physics2D.OverlapBox(groundCheker.Checker.transform.position,
            new Vector2(groundCheker.CheckerSize, Mathf.Epsilon), 0f, groundCheker.Mask);
        movementLogic.Move(speed);
        if (onGround)
            Bounce(bounceForce);
    }

    public void AllowToMove()
    {
        isMoving = true;
        starAnimator.Play("StarAnimation");
    }

    private void Bounce(float bounceForce)
    {
        body.velocity =  new Vector2(body.velocity.x, bounceForce );
    }

}
[Serializable]
public class GroundChecker
{
    public float CheckerSize;
    public LayerMask Mask;
    public Transform Checker;
}
