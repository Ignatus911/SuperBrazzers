using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var bonusComponent = other.gameObject.GetComponentInParent<IBonus>();
        if (bonusComponent != null)
            bonusComponent.Use(gameObject);
    }
}
