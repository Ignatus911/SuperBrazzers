using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private PlayerStatusController playerStatusController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bonusComponent = other.gameObject.GetComponentInParent<IBonus>();
        if (bonusComponent != null)
        {
            bonusComponent.Use(gameObject);
            return;
        }

        var enemyComponent = other.gameObject.GetComponentInParent<IEnemy>();
        if (enemyComponent != null)
        {
            //Если мы выше
            enemyComponent.Hit();
            //Если мы на уровне или ниже
            playerStatusController.Hit();
        }
    }
}
