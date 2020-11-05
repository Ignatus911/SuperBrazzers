using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

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
        ShowCoins(0);
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

    public void ShowCoins(int coins)
    {
        coinsText.text = "\nX" + coins.ToString("D2");
    }

    public void WriteLifes(int lives)
    {
        livesText.text = string.Format(" X  {0}", lives.ToString());
    }



    public void SetLoadingUIScreen(bool value)
    {
        loadingUI.SetActive(value);
    }

    public void WriteCurrentTime(int currentTime)
    {
        timeText.text = string.Format("time\n {0}", currentTime.ToString("D3"));
    }
}
