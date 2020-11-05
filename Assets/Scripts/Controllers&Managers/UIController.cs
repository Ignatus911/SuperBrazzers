using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
    private int coins = 0;
    private int lives = 3;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text currentWorldText;
    [SerializeField] private Text currentWorldTextDeathScreen;
    [SerializeField] private Text livesText;
    [SerializeField] private Image marioSprite;
    [SerializeField] private Text timeText;



    [SerializeField] private GameObject loadingUI;

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
        ShowScore(0);
        IncreaseCoins(0);
        WriteWorld(1, 1);
        SceneController.OnSceneLoaded += OnSceneLoaded;
        OnSceneLoaded("");
    }

    private void OnSceneLoaded(string sceneName)
    {
        SetLoadingUIScreen(sceneName == SceneController.DEATH_SCENE_NAME);
    }

    public void ShowScore(int score)
    {
        scoreText.text = "MARIO \n" + score.ToString("D6");
    }

    public void WriteWorld(int world, int stage)
    {
        currentWorldText.text = string.Format("WORLD\n{0}-{1}", world, stage);
        currentWorldTextDeathScreen.text = string.Format("WORLD {0}-{1}", world, stage);
    }

    public void IncreaseLives()
    {
        lives++;
        WriteLifes();
    }

    public void DecreaseLives()
    {
        lives--;
        if (lives < 1)
        {
            // GameOver screan;
            //Destroy(gameObject);
        }
        WriteLifes();
    }

    private void WriteLifes()
    {
        livesText.text = string.Format(" X  {0}", lives.ToString());
    }

    public void IncreaseCoins(int value = 1) {
        coins += value;
        if (coins == 100)
        {
            coins = 0;
            IncreaseLives();
        }
        coinsText.text = string.Format(" X{0}", coins.ToString("D2"));
    }

    public void SetLoadingUIScreen(bool value)
    {
        loadingUI.SetActive(value);
    }

    public void WriteCurrentTime(int currentTime)
    {
        timeText.text = string.Format("time\n {0}", currentTime.ToString("D3"));
    }

    //private void Update()
    //{
    //    if (!SceneController.Instance.isDeathScreen())
    //    {
    //        timeText.text = "time\n" + time.ToString("D3");
    //        currentWorldTextDeathScreen.enabled = false;
    //        livesText.enabled = false;
    //        marioSprite.enabled = false;
    //    }
    //    else
    //    {
    //        timeText.text = "time\n";
    //        currentWorldTextDeathScreen.enabled = true;
    //        livesText.enabled = true;
    //        marioSprite.enabled = true;
    //    }
    //}
}
