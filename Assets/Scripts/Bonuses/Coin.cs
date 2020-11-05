using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;
    private void Awake()
    {
        GetComponent<Animator>().Play("CoinAppearsAnimation");
        AudioManager.Instance.PlaySound(coinClip);
        ScoreController.Instance.AddScore(200);
        ScoreController.Instance.IncreaseCoins();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
