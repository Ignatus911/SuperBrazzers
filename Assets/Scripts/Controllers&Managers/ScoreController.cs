using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance { get; private set; }
    private int coins = 0;
    private int score = 0;
    private int lives = 3;
    private int time = 400;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text currentWorldText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text currentWorldTextDeathScreen;
    [SerializeField] private Text livesTextext;
    [SerializeField] private Image marioSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("new one");
        }
        else
        {
            Debug.Log("clone is dead");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int value) { score += value; }

    public void IncreaseLives() { lives++; }
    public void DecreaseLives() {
        lives--;
        if (lives < 1)
        {
            // GameOver screan;
            //Destroy(gameObject);
        }
    }

    public void IncreaseCoins() {
        coins++;
        if(coins == 100)
        {
            coins = 0;
            IncreaseLives();
        }
    }


    // Update is called once per frame
    void Update()
    {
        scoreText.text = "MARIO \n" + score.ToString("D6");
        coinsText.text = "\n" + "ЖX" + coins.ToString("D2");
        currentWorldText.text = "WORLD\n" + "1-1";// привязать к названию ЛВЛа или создать список связзаный с текущим ЛВЛом
        currentWorldTextDeathScreen.text = "WORLD 1-1";// привязать к названию ЛВЛа или создать список связзаный с текущим ЛВЛом
        livesTextext.text = " X " + lives.ToString("D2");

        if (!SceneController.Instance.isDeathScreen())
        {
            timeText.text = "time\n" + time.ToString("D3");
            currentWorldTextDeathScreen.enabled = false;
            livesTextext.enabled = false;
            marioSprite.enabled = false;
        }
        else
        {
            timeText.text = "time\n";
            currentWorldTextDeathScreen.enabled = true;
            livesTextext.enabled = true;
            marioSprite.enabled = true;
        }
    }
}
