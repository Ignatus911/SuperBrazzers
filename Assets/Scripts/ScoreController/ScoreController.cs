using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController Instance { get; private set; }
    private int coins = 0;
    private int score = 0;
    private int lives = 2;
    private int time = 400;
    [SerializeField] private Text ScoreText;
    [SerializeField] private Text CoinsText;
    [SerializeField] private Text CurrentWorldText;
    [SerializeField] private Text TimeText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddScore(int value) { score += value; }

    public void IncreaseLives() { lives++; }
    public void DecreaseLives() {
        lives--;
        if(lives < 1)
        {
            // GameOver screan;
            Destroy(gameObject);
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
        ScoreText.text = "MARIO \n" + score.ToString("D6");
        CoinsText.text = "\n" + "ЖX" + coins.ToString("D2");
        CurrentWorldText.text = "WORLD\n" + "1-1";// привязать к названию ЛВЛа или создать список связзаный с текущим ЛВЛом
        TimeText.text = "time\n" + time.ToString("D3");
    }
}
