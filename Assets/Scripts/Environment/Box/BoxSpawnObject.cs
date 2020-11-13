using UnityEngine;

public class BoxSpawnObject : MonoBehaviour, BoxState
{
    private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite deadBlockSprite;
    [SerializeField] private float lifeTime;
    [SerializeField] private bool enableLastHit;
    private BoxSpawnCoinState state = BoxSpawnCoinState.Default;

    [SerializeField] private BoxBonusType bonusType;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject mushromPrefab;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private Transform spawnTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void DoLogic()
    {
        switch (state)
        {
            case BoxSpawnCoinState.Default:
                state = BoxSpawnCoinState.UnderPressing;
                lifeTime = Time.time + lifeTime;
                SpawnElement();
                return;
            case BoxSpawnCoinState.UnderPressing:
                SpawnElement();
                return;
            case BoxSpawnCoinState.WaitingForLastHit:
                SpawnElement();
                enableLastHit = false;
                state = BoxSpawnCoinState.UnderPressing;
                return;
            case BoxSpawnCoinState.Dead:
                return;
        }
    }

    private void Update()
    {
        if (state != BoxSpawnCoinState.UnderPressing)
            return;
        if (lifeTime < Time.time)
        {
            if (!enableLastHit)
            {
                state = BoxSpawnCoinState.Dead;
                sprite.sprite = deadBlockSprite;
            }
            else state = BoxSpawnCoinState.WaitingForLastHit;
        }
    }

    private void SpawnElement()
    {
        animator.Play("BoxJump");

        GameObject spawnObject = null;
        switch (bonusType)
        {
            case BoxBonusType.Coin:
                spawnObject = coinPrefab;
                break;
            case BoxBonusType.Mushroom:
                spawnObject = mushromPrefab;
                break;
            case BoxBonusType.Star:
                spawnObject = starPrefab;
                break;
        }
        Instantiate(spawnObject, spawnTransform.position, new Quaternion(), spawnTransform);
    }
}
