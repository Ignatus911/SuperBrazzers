using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusController : MonoBehaviour
{

    private int mushroomLayer = 15; //number of mushroom layer
    private int enemiesLayer = 10;  //number of enemies layer
    [SerializeField] Transform UpHitCheckerPosition; 
    [SerializeField] PlayerAnimationAspect playerAnimationAspect;
    [SerializeField] float xBigPlayerCollider = 1.95f ;
    [SerializeField] float xSmallPlayerCollider = 0.95f;
    [SerializeField] float bigCheckerPosition = 1.1f;
    [SerializeField] float smallCheckerPosition = 0.055f;

    BoxCollider2D playerBoxCollider;
    private bool isBig = false;
    private bool becomeBig = false;

    private void Awake()
    {
        playerBoxCollider = GetComponent<BoxCollider2D>();
    }
    public void Update()
    {
        
        //if ("colapse with mushrom")
        //{

        //}
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == mushroomLayer)
        {
            isBig = true;
            Destroy(other.gameObject);
            playerAnimationAspect.BecomeBig();
            playerBoxCollider.offset = new Vector2(playerBoxCollider.offset.x, 0f);
            playerBoxCollider.size = new Vector2(playerBoxCollider.size.x,xBigPlayerCollider);
            UpHitCheckerPosition.transform.localPosition = new Vector2(UpHitCheckerPosition.localPosition.x, bigCheckerPosition);
        }
        else if (other.gameObject.layer == enemiesLayer)
        {
            if (isBig)
            {
                playerAnimationAspect.BecomeSmall();
                playerBoxCollider.offset = new Vector2(playerBoxCollider.offset.x, -0.5f);
                playerBoxCollider.size = new Vector2(playerBoxCollider.size.x, xSmallPlayerCollider);
                UpHitCheckerPosition.transform.localPosition = new Vector2(UpHitCheckerPosition.localPosition.x, smallCheckerPosition);
            }
        }
    }

    public bool IsBig
    {
        get { return isBig; }
    }
}
