using UnityEngine;

public class BoxSpawnObject : MonoBehaviour, BoxState
{
    private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Sprite deadBlockSprite;
    [SerializeField] private float lifeTime;
    private BoxSpawnCoinState state = BoxSpawnCoinState.Default;
    private float destroyTime;

    [SerializeField] private BoxBonusType bonusType;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject mushromPrefab;
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
            state = BoxSpawnCoinState.Dead;
            sprite.sprite = deadBlockSprite;
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
        }
        Instantiate(spawnObject, spawnTransform.position, new Quaternion(), spawnTransform);
    }
}
