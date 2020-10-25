using UnityEngine;
using System.Collections;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private PlayerStatusController playerStatusController;
    [SerializeField] private Transform PlayerGroundController;
    private bool freaze;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!freaze)
        {
            var bonusComponent = other.gameObject.GetComponentInParent<IBonus>();
            if (bonusComponent != null)
            {
                StartCoroutine(freazeCourutine());
                bonusComponent.Use(gameObject);
                return;
            }
        }
        if (playerStatusController.IsInvincible) return;
        else
        {
            var enemyComponent = other.gameObject.GetComponentInParent<IEnemy>();
            if (enemyComponent != null)
            {

                var deathPoint = other.gameObject.GetComponent<Transform>();
                float enemyDeathPoint = deathPoint.position.y;
                float playerFeetPosition = PlayerGroundController.position.y;
                if (playerFeetPosition >= enemyDeathPoint) { enemyComponent.Hit(); }
                else playerStatusController.Hit();
            }
        }
    }

    IEnumerator freazeCourutine()
    {
        freaze = true;
        yield return new WaitForFixedUpdate();
        freaze = false;
    }
}
