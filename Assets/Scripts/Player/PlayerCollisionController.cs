using UnityEngine;
using System.Collections;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private PlayerStatusController playerStatusController;
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

    IEnumerator freazeCourutine()
    {
        freaze = true;
        yield return new WaitForFixedUpdate();
        freaze = false;
    }
}
