using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerCollider : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collisionObject = collision.gameObject;
        if (collisionObject.GetComponent<PlayerStatusController>() != null)
            collisionObject.GetComponent<PlayerStatusController>().BecomeDead();
        else Destroy(collisionObject);
    }
}
