using System.Collections;
using UnityEngine;

public class BoxController : MonoBehaviour, IHeadHitting
{
    [SerializeField] private BoxType boxType;
    [SerializeField] private BoxDestroy boxDestroy;
    [SerializeField] private BoxJump boxJump;
    [SerializeField] private BoxSpawnObject _boxObject;
    private BoxState currentState;
    private bool freeze;

    public void Hit(GameObject hitter)
    {
        if (freeze)
            return;
        var playerStatus = hitter.GetComponent<PlayerStatusController>();
        if (playerStatus == null)
            return;
        switch (boxType)
        {
            case BoxType.SimpleBrick:
                switch (playerStatus.Status)
                {
                    case PlayerStatus.Small:
                        currentState = boxJump;
                        break;
                    case PlayerStatus.Super:
                    case PlayerStatus.Big:
                        currentState = boxDestroy;
                        break;
                }
                break;

            case BoxType.BoxWithBonus:
                currentState = _boxObject;
                break;
        }
        currentState.DoLogic();
        freeze = true;
        StartCoroutine(DontReact());
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator DontReact()
    {
        yield return new WaitForSeconds(0.1f);
        freeze = false;
    }
}
