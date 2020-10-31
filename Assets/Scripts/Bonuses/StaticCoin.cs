using UnityEngine;

public class StaticCoin : BonusCommonLogic
{
    [SerializeField] private AudioClip coinClip;

    private void Awake()
    {
        GetComponent<Animator>().Play("StaticCoin");
    }

    public override void UseImplementation(GameObject user)
    {
        isAlife = false;
        AudioManager.Instance.PlaySound(coinClip);
        ScoreController.Instance.AddScore(200);
        ScoreController.Instance.IncreaseCoins();
        Destroy(gameObject);
    }
}
