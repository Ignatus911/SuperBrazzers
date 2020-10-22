using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip coinClip;
    private void Awake()
    {
        GetComponent<Animator>().Play("CoinAppearsAnimation");
        AudioManager.Instance.Play(coinClip);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
