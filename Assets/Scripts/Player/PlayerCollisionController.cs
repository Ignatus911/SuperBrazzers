using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private PlayerStatusController playerStatusController;
    [SerializeField] private Transform PlayerGroundController;
    [SerializeField] private PlayerDirectionAspect direction;
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        var bonusComponent = other.gameObject.GetComponentInParent<IBonus>();
        if (bonusComponent != null)
        {
            bonusComponent.Use(gameObject);
            return;
        }
        if (playerStatusController.IsUntouchable)
            return;
        var enemyComponent = other.gameObject.GetComponentInParent<IEnemy>();
        if (enemyComponent != null)
        {
            if (!playerStatusController.IsSuper)
            {
                var deathPoint = other.gameObject.GetComponent<Transform>();
                float enemyDeathPoint = deathPoint.position.y;
                float playerFeetPosition = PlayerGroundController.position.y;
                if (playerFeetPosition >= enemyDeathPoint) { enemyComponent.Hit(gameObject, direction.IsLookRight); }
                else playerStatusController.Hit();
            }
            else enemyComponent.DieFromSuperPlayer(gameObject, direction.IsLookRight);
        }
    }
}
