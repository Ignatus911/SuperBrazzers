using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSizeAspet : MonoBehaviour
{

    private BoxCollider2D playerCollider;
    [SerializeField] private PlayerSizeConfig smallPlayerCollider;
    [SerializeField] private PlayerSizeConfig bigPlayerCollider;
    [SerializeField] private Transform UpHitCheckerPosition;
    private void Awake()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    public void setBigCollider()
    {
        playerCollider.offset = bigPlayerCollider.offset;
        playerCollider.size = bigPlayerCollider.size;
        UpHitCheckerPosition.localPosition = new Vector2(UpHitCheckerPosition.localPosition.x, bigPlayerCollider.UpHitCheckerPosition);
    }

    public void setSmallCollider()
    {
        playerCollider.offset = smallPlayerCollider.offset;
        playerCollider.size = smallPlayerCollider.size;
        UpHitCheckerPosition.position = new Vector2(UpHitCheckerPosition.localPosition.x, smallPlayerCollider.UpHitCheckerPosition);
    }
}
