using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int score = 200;
    [SerializeField] private AudioClip coinClip;
    private void Awake()
    {
        GetComponent<Animator>().Play("CoinAppearsAnimation");
        AudioManager.Instance.PlaySound(coinClip);
        ScoreController.Instance.AddScore(score);
        ScoreController.Instance.IncreaseCoins();
    }

    private void ShowScoreForCoin()
    {
        ScoreController.Instance.AddScore(score, transform);
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
