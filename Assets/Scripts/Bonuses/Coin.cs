using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;
    private void Awake()
    {
        GetComponent<Animator>().Play("CoinAnimation");
        AudioManager.Instance.Play(coinClip);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
