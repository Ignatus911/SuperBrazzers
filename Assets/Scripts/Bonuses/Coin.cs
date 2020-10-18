using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private void Awake()
    {
        GetComponent<Animator>().Play("CoinAnimation");
        GetComponent<AudioSource>().Play();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
