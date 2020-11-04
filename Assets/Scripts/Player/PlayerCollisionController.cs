using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private PlayerStatusController playerStatusController;
    [SerializeField] private Transform PlayerGroundController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bonusComponent = other.gameObject.GetComponentInParent<IBonus>();
        if (bonusComponent != null)
        {
            bonusComponent.Use(gameObject);
            return;
        }
        if (playerStatusController.IsInvincible)
            return;
        var enemyComponent = other.gameObject.GetComponentInParent<IEnemy>();
        if (enemyComponent != null)
        {
            var deathPoint = other.gameObject.GetComponent<Transform>();
            float enemyDeathPoint = deathPoint.position.y;
            float playerFeetPosition = PlayerGroundController.position.y;
            if (playerFeetPosition >= enemyDeathPoint) { enemyComponent.Hit(gameObject); }
            else playerStatusController.Hit();
        }
    }
}
