using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaCollisionController : MonoBehaviour
{
    [SerializeField] private KoopaStatusController status;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (status.Status != KoopaStatus.Projectile)
            return;
        var enemyComponent = collision.gameObject.GetComponentInParent<IEnemy>();
        if (enemyComponent != null)
            enemyComponent.Hit(gameObject);
    }
}
