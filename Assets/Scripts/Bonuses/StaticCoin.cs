using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCoin : MonoBehaviour, IBonus
{
    [SerializeField] private AudioClip coinClip;

    private void Awake()
    {
        GetComponent<Animator>().Play("StaticCoin");
    }

    public void Use(GameObject user)
    {
        AudioManager.Instance.Play(coinClip);
        ScoreController.Instance.AddScore(200);
        ScoreController.Instance.IncreaseCoins();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
