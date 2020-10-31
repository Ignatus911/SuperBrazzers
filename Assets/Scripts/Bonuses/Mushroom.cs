using JetBrains.Annotations;
using UnityEngine;

public class Mushroom: BonusCommonLogic
{
    [SerializeField] private Animator mushroomAnimator;
    private bool isMoving = false;
    [SerializeField] private AudioClip mushroomClip;
    [SerializeField] private AudioClip PowerupClip;

    [SerializeField]
    private EnemyMovementAspect movementLogic;
    [SerializeField] private float speed;

    private void Awake()
    {
        mushroomAnimator.Play("Show");
        AudioManager.Instance.PlaySound(mushroomClip);
    }

    private void Update()
    {
        if (!isMoving)
            return;
        movementLogic.Move(speed);
    }

    public override void UseImplementation(GameObject user)
    {
        if (!isMoving)
            return;
        isAlife = false;
        user.GetComponent<PlayerStatusController>().BecomeBig();
        AudioManager.Instance.PlaySound(PowerupClip);
        ScoreController.Instance.AddScore(1000);
        Destroy(gameObject);
    }

    /// <summary>
    /// Вызывается в анимации, позволяет грибу двигаться
    /// </summary>
    [UsedImplicitly]
    public void AllowToMove()
    {
        isMoving = true;
    }
}
