using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance { get; private set; }
    [SerializeField] private PointsSpawner pointsSpawner;
    private int score = 0;
    private int coins = 0;
    private int lives = 3;
    private float playerComboTimer = 0;
    private float turtleComboTimer = 0;
    private int killedByPlayerEnemies = 0;
    private int killedByTurtleEnemies = 0;

    private bool playerKillingCombo;
    private bool playerKillingByTurtleCombo;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
        public void AddScore(int value)
    {
        score += value;
        UIController.Instance.ShowScore(score);
    }

    public void AddScore(int value, bool killByPlayer, bool killByTurtle)
    {
        if (killByPlayer)
        {
            playerComboTimer = 1;
            playerKillingCombo = true;
            killedByPlayerEnemies++;
            value = value * (int)Mathf.Pow(2, killedByPlayerEnemies - 1);// при убийстве нескольких врагов подряд прогрессия 2 в степени убийств
        }
        if (killByTurtle)
        {
            turtleComboTimer = 2;
            playerKillingByTurtleCombo = true;
            killedByTurtleEnemies++;
            value = value + 300 * (killedByTurtleEnemies-1);// 500 за первого, на 300 больше за каждого последующего 
        }
        score += value;
        pointsSpawner.SpawnPoints(value);
        UIController.Instance.ShowScore(score);
    }

    public void IncreaseCoins(int value = 1)
    {
        coins += value;
        if (coins == 100)
        {
            coins = 0;
            IncreaseLives();
        }
        UIController.Instance.ShowCoins(coins);
    }

    public void IncreaseLives()
    {
        lives++;
        UIController.Instance.WriteLifes(lives);
    }

    public void DecreaseLives()
    {
        lives--;
        if (lives < 1)
        {
            // GameOver screan;
            //Destroy(gameObject);
        }
        UIController.Instance.WriteLifes(lives);
    }

    void Update()
    {
        if (playerKillingCombo)
        {
            playerComboTimer -= Time.deltaTime;
            if (playerComboTimer < 0)
            {
                playerKillingCombo = false;
                killedByPlayerEnemies = 0;
            }
        }
        if (playerKillingByTurtleCombo)
        {
            turtleComboTimer -= Time.deltaTime;
            if (turtleComboTimer < 0)
            {
                playerKillingByTurtleCombo = false;
                killedByTurtleEnemies = 0;
            }
        }
    }
}
